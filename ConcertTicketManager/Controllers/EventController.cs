using ConcertTicketManager.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class EventController : Controller
{
    private readonly ConcertDbContext _context;

    public EventController(ConcertDbContext context)
    {
        _context = context;
    }

    // GET: Event/Create
    public IActionResult Create()
    {
        // Populate the list of venues for the dropdown in the Create view
        ViewBag.Venues = _context.Venues.ToList();
        return View();
    }

    // POST: Event/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EventViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Check if the event name is unique
            var existingEvent = await _context.Events
                .Where(e => e.Name == model.Name)
                .FirstOrDefaultAsync();

            if (existingEvent != null)
            {
                ModelState.AddModelError("Name", "Event name must be unique.");
                ViewBag.Venues = await _context.Venues.ToListAsync();
                return View(model);
            }

            // Ensure the event date is in the future
            if (model.Date <= DateTime.Now)
            {
                ModelState.AddModelError("Date", "Event date must be a future date.");
                ViewBag.Venues = await _context.Venues.ToListAsync();
                return View(model);
            }

            // Ensure ticket type capacities don't exceed venue capacity
            var venueCapacity = await _context.Venues
                .Where(v => v.VenueId == model.VenueId)
                .Select(v => v.Capacity)
                .FirstOrDefaultAsync();

            if (model.TicketTypes.Any(tt => tt.Capacity > venueCapacity))
            {
                ModelState.AddModelError("TicketTypes", "Ticket type capacity cannot exceed venue capacity.");
                ViewBag.Venues = await _context.Venues.ToListAsync();
                return View(model);
            }

            // Create the event
            var newEvent = new Event
            {
                Name = model.Name,
                Date = model.Date,
                Description = model.Description,
                VenueId = model.VenueId
            };

            _context.Add(newEvent);
            await _context.SaveChangesAsync();

            // Create the ticket types for the event
            foreach (var ticketType in model.TicketTypes)
            {
                var newTicketType = new TicketType
                {
                    EventId = newEvent.EventId,
                    Type = ticketType.Type,
                    Price = ticketType.Price,
                    Capacity = ticketType.Capacity
                };
                _context.Add(newTicketType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Create"); // Redirect back to the Create page after successful creation
        }

        // If we got this far, something failed, so we return the view with the errors
        ViewBag.Venues = await _context.Venues.ToListAsync();
        return View(model);
    }
}
