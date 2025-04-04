using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class TicketController : Controller
{
    private readonly ConcertDbContext _context;

    public TicketController(ConcertDbContext context)
    {
        _context = context;
    }

    // 5. Reserve Tickets
    [HttpGet]
    public IActionResult Reserve()
    {
        ViewBag.TicketTypes = _context.TicketTypes.ToList();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Reserve(TicketReservationViewModel model)
    {
        var ticketType = await _context.TicketTypes.Include(t => t.Reservations).FirstOrDefaultAsync(t => t.TicketTypeId == model.TicketTypeId);
        if (ticketType == null)
        {
            ModelState.AddModelError("TicketTypeId", "Invalid ticket type.");
            return View(model);
        }

        var reservedCount = ticketType.Reservations.Where(r => !r.IsPurchased && r.ExpirationTime > DateTime.UtcNow).Sum(r => r.Quantity);
        if (ticketType.Capacity - reservedCount < model.Quantity)
        {
            ModelState.AddModelError("Quantity", "Not enough tickets available.");
            return View(model);
        }

        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == model.CustomerEmail);
        if (customer == null)
        {
            customer = new Customer { Name = model.CustomerName, Email = model.CustomerEmail };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        var reservation = new TicketReservation
        {
            CustomerId = customer.CustomerId,
            TicketTypeId = model.TicketTypeId,
            Quantity = model.Quantity,
            ExpirationTime = DateTime.UtcNow.AddMinutes(10),
            IsPurchased = false
        };

        _context.TicketReservations.Add(reservation);
        await _context.SaveChangesAsync();

        model.Message = "Tickets reserved successfully for 10 minutes.";
        return View("ReservationConfirmation", model);
    }

    // 6. Purchase Tickets
    [HttpGet]
    public IActionResult Purchase() => View();

    [HttpPost]
    public async Task<IActionResult> Purchase(TicketPurchaseViewModel model)
    {
        var reservation = await _context.TicketReservations.Include(r => r.TicketType).ThenInclude(t => t.Event).Include(r => r.Customer).FirstOrDefaultAsync(r => r.ReservationId == model.ReservationId);

        if (reservation == null || reservation.IsPurchased || reservation.ExpirationTime < DateTime.UtcNow)
        {
            ModelState.AddModelError("ReservationId", "Invalid or expired reservation.");
            return View(model);
        }

        reservation.IsPurchased = true;
        var purchase = new TicketPurchase
        {
            ReservationId = reservation.ReservationId,
            PaymentMethod = "Simulated",
            PurchaseDate = DateTime.UtcNow
        };

        _context.TicketPurchases.Add(purchase);
        await _context.SaveChangesAsync();

        model.Message = "Purchase successful.";
        return View("PurchaseConfirmation", model);
    }

    // 7. Cancel Reservation
    [HttpPost]
    public async Task<IActionResult> Cancel(int reservationId)
    {
        var reservation = await _context.TicketReservations.Include(r => r.Customer).FirstOrDefaultAsync(r => r.ReservationId == reservationId);
        if (reservation == null || reservation.IsPurchased)
            return BadRequest("Cannot cancel this reservation.");

        _context.TicketReservations.Remove(reservation);
        await _context.SaveChangesAsync();

        return RedirectToAction("CancelConfirmation");
    }

    public IActionResult CancelConfirmation()
    {
        return View();
    }

    // 8. View Availability
    public async Task<IActionResult> Availability(int eventId)
    {
        var ticketTypes = await _context.TicketTypes
            .Where(t => t.EventId == eventId)
            .Include(t => t.Reservations)
            .Select(t => new TicketAvailabilityViewModel
            {
                TicketTypeId = t.TicketTypeId,
                Type = t.Type,
                Price = t.Price,
                Capacity = t.Capacity,
                AvailableQuantity = t.Capacity - t.Reservations.Where(r => !r.IsPurchased && r.ExpirationTime > DateTime.UtcNow).Sum(r => r.Quantity)
            }).ToListAsync();

        return View(ticketTypes);
    }
}