using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

public class EventViewModel
{
    [Required(ErrorMessage = "Event name is required.")]
    [StringLength(200, ErrorMessage = "Event name cannot exceed 200 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Event date is required.")]
    [DataType(DataType.DateTime)]
    [Display(Name = "Event Date")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Event description is required.")]
    [StringLength(500, ErrorMessage = "Event description cannot exceed 500 characters.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Venue is required.")]
    [Display(Name = "Venue")]
    public int? VenueId { get; set; }

    public List<SelectListItem> Venues { get; set; } = new();

    public List<TicketTypeViewModel> TicketTypes { get; set; } = new();
}

public class TicketTypeViewModel
{
    [Required(ErrorMessage = "Ticket type is required.")]
    [StringLength(100, ErrorMessage = "Ticket type cannot exceed 100 characters.")]
    public string Type { get; set; }

    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Capacity is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be at least 1.")]
    public int Capacity { get; set; }
}
