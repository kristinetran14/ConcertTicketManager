using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

public class EventController : Controller
{
    private readonly ConcertDbContext _context;

    public EventController(ConcertDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var events = await _context.Events
            .Include(e => e.Venue)
            .Include(e => e.TicketTypes)
            .ToListAsync();

        return View(events);
    }

    public async Task<IActionResult> Create()
    {
        var model = new EventViewModel
        {
            Date = DateTime.Now,
            Venues = await GetVenueSelectListAsync(),
            TicketTypes = new List<TicketTypeViewModel> { new TicketTypeViewModel() }
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EventViewModel model)
    {
        model.Venues = await GetVenueSelectListAsync();

        if (!ModelState.IsValid)
            return View(model);

        if (model.Date <= DateTime.UtcNow)
        {
            ModelState.AddModelError("Date", "Event date must be in the future.");
            return View(model);
        }

        if (await _context.Events.AnyAsync(e => e.Name == model.Name))
        {
            ModelState.AddModelError("Name", "An event with this name already exists.");
            return View(model);
        }

        var venue = await _context.Venues.FindAsync(model.VenueId);
        if (venue == null)
        {
            ModelState.AddModelError("VenueId", "Invalid venue selected.");
            return View(model);
        }

        var totalTicketCapacity = model.TicketTypes.Sum(t => t.Capacity);
        if (totalTicketCapacity > venue.Capacity)
        {
            ModelState.AddModelError("", "Total ticket capacity exceeds the venue limit.");
            return View(model);
        }

        var newEvent = new Event
        {
            Name = model.Name,
            Date = model.Date,
            Description = model.Description,
            VenueId = model.VenueId.Value,
            TicketTypes = model.TicketTypes.Select(t => new TicketType
            {
                Type = t.Type,
                Price = t.Price,
                Capacity = t.Capacity
            }).ToList()
        };


        _context.Events.Add(newEvent);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    private async Task<List<SelectListItem>> GetVenueSelectListAsync()
    {
        var venues = await _context.Venues.ToListAsync();
        return venues.Select(v => new SelectListItem
        {
            Value = v.VenueId.ToString(),
            Text = v.Name
        }).Prepend(new SelectListItem
        {
            Value = "",
            Text = "-- Select Venue --"
        }).ToList();
    }
}
