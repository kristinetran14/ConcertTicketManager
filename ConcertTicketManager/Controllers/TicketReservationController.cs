﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class TicketReservationController : Controller
{
    private readonly ConcertDbContext _context;
    private readonly TimeSpan _reservationDuration = TimeSpan.FromMinutes(15);

    public TicketReservationController(ConcertDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Reserve()
    {
        var model = new TicketReservationViewModel
        {
            Events = await _context.Events
                .OrderBy(e => e.Name)
                .Select(e => new SelectListItem
                {
                    Value = e.EventId.ToString(),
                    Text = e.Name
                }).ToListAsync()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Reserve(TicketReservationViewModel model)
    {
        model.Events = await _context.Events
            .OrderBy(e => e.Name)
            .Select(e => new SelectListItem
            {
                Value = e.EventId.ToString(),
                Text = e.Name
            }).ToListAsync();

        model.TicketTypes = await _context.TicketTypes
            .Where(tt => tt.EventId == model.EventId)
            .OrderBy(tt => tt.Type)
            .Select(tt => new SelectListItem
            {
                Value = tt.TicketTypeId.ToString(),
                Text = $"{tt.Type} - ${tt.Price:F2}"
            }).ToListAsync();

        if (!ModelState.IsValid)
            return View(model);

        // Find or create customer
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == model.Email);
        if (customer == null)
        {
            customer = new Customer
            {
                Name = model.CustomerName,
                Email = model.Email,
                CreatedAt = DateTime.UtcNow
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        var ticketType = await _context.TicketTypes
            .Include(tt => tt.Reservations)
            .FirstOrDefaultAsync(tt => tt.TicketTypeId == model.TicketTypeId);

        if (ticketType == null)
        {
            ModelState.AddModelError("", "Selected ticket type is invalid.");
            return View(model);
        }

        var reservedCount = ticketType.Reservations
            .Where(r => !r.IsPurchased && r.ExpirationTime > DateTime.UtcNow)
            .Sum(r => r.Quantity);

        if (reservedCount + model.Quantity > ticketType.Capacity)
        {
            ModelState.AddModelError("", "Not enough tickets available for reservation.");
            return View(model);
        }

        var reservation = new TicketReservation
        {
            CustomerId = customer.CustomerId,
            TicketTypeId = model.TicketTypeId,
            Quantity = model.Quantity,
            ExpirationTime = DateTime.UtcNow.Add(_reservationDuration),
            IsPurchased = false
        };

        _context.TicketReservations.Add(reservation);
        await _context.SaveChangesAsync();

        model.ExpirationTime = reservation.ExpirationTime;
        model.Message = $"Reserved {model.Quantity} ticket(s) until {model.ExpirationTime:u}";

        return View("Confirmation", model);
    }

    [HttpGet]
    public async Task<JsonResult> GetTicketTypesByEvent(int eventId)
    {
        var ticketTypes = await _context.TicketTypes
            .Where(t => t.EventId == eventId)
            .Select(t => new
            {
                id = t.TicketTypeId,
                name = $"{t.Type} - ${t.Price:F2}"
            }).ToListAsync();

        return Json(ticketTypes);
    }

    [HttpGet]
    public IActionResult Cancel()
    {
        return View(new CancelReservationViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Cancel(CancelReservationViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var customer = await _context.Customers
            .Include(c => c.Reservations)
                .ThenInclude(r => r.TicketType)
                    .ThenInclude(tt => tt.Event)
            .FirstOrDefaultAsync(c => c.Email == model.Email);

        if (customer == null)
        {
            model.Message = "No customer found with this email.";
            return View(model);
        }

        var activeReservations = customer.Reservations
            .Where(r => !r.IsPurchased && r.ExpirationTime > DateTime.UtcNow)
            .ToList();

        if (!activeReservations.Any())
        {
            model.Message = "No active reservations found.";
            return View(model);
        }

        // If no reservations were selected yet, just display them to choose
        if (!model.SelectedReservationIds.Any())
        {
            model.Reservations = activeReservations.Select(r => new ReservationSummary
            {
                ReservationId = r.ReservationId,
                EventName = r.TicketType.Event.Name,
                TicketType = r.TicketType.Type,
                Quantity = r.Quantity,
                ExpirationTime = r.ExpirationTime
            }).ToList();

            model.Message = "Select reservations to cancel.";
            return View(model);
        }

        // Cancel selected reservations
        var toCancel = activeReservations
            .Where(r => model.SelectedReservationIds.Contains(r.ReservationId))
            .ToList();

        if (!toCancel.Any())
        {
            model.Message = "No valid reservations selected.";
            return View(model);
        }

        _context.TicketReservations.RemoveRange(toCancel);
        await _context.SaveChangesAsync();

        model.Message = $"Canceled {toCancel.Count} reservation(s) for {customer.Email}.";
        model.Reservations = new List<ReservationSummary>(); // Clear after cancellation
        return View(model);
    }

}
