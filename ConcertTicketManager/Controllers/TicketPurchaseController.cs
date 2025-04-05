using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class TicketPurchaseController : Controller
{
    private readonly ConcertDbContext _context;

    public TicketPurchaseController(ConcertDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Purchase()
    {
        return View(new TicketPurchaseViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Purchase(TicketPurchaseViewModel model)
    {
        var customer = await _context.Customers
            .Include(c => c.Reservations)
                .ThenInclude(r => r.TicketType)
                    .ThenInclude(tt => tt.Event)
            .FirstOrDefaultAsync(c => c.Email == model.Email);

        if (customer == null)
        {
            ModelState.AddModelError("", "No customer found with this email.");
            return View(model);
        }

        var activeReservations = customer.Reservations
            .Where(r => !r.IsPurchased && r.ExpirationTime > DateTime.UtcNow)
            .ToList();

        model.AvailableReservations = activeReservations.Select(r => new PurchaseReservationSummary
        {
            ReservationId = r.ReservationId,
            EventName = r.TicketType.Event.Name,
            TicketType = r.TicketType.Type,
            Quantity = r.Quantity,
            ExpirationTime = r.ExpirationTime,
            TotalPrice = r.Quantity * r.TicketType.Price
        }).ToList();

        if (!ModelState.IsValid || model.SelectedReservationId == null)
        {
            if (!model.AvailableReservations.Any())
                model.Message = "You don't have any active reservations to purchase.";

            return View(model);
        }

        var selected = activeReservations
            .FirstOrDefault(r => r.ReservationId == model.SelectedReservationId.Value);

        if (selected == null)
        {
            model.Message = "Selected reservation is invalid or expired.";
            return View(model);
        }

        // TODO: Integrate payment provider here.

        selected.IsPurchased = true;
        var purchase = new TicketPurchase
        {
            ReservationId = selected.ReservationId,
            PaymentMethod = model.PaymentMethod,
            PurchaseDate = DateTime.UtcNow
        };

        _context.TicketPurchases.Add(purchase);
        await _context.SaveChangesAsync();

        // Redirect to confirmation page
        return RedirectToAction("Confirmation", new
        {
            eventName = selected.TicketType.Event.Name,
            ticketType = selected.TicketType.Type,
            quantity = selected.Quantity,
            total = (selected.Quantity * selected.TicketType.Price).ToString("F2"),
            email = customer.Email
        });
    }

    [HttpGet]
    public IActionResult Confirmation(string eventName, string ticketType, int quantity, string total, string email)
    {
        ViewBag.EventName = eventName;
        ViewBag.TicketType = ticketType;
        ViewBag.Quantity = quantity;
        ViewBag.Total = total;
        ViewBag.Email = email;
        return View();
    }
}
