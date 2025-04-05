using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

public class EventViewModel
{
    public int EventId { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    [Display(Name = "Event Date")]
    public DateTime Date { get; set; }

    [Required]
    [StringLength(500)]
    public string Description { get; set; }

    [Required]
    [Display(Name = "Venue")]
    public int? VenueId { get; set; }

    public List<SelectListItem> Venues { get; set; } = new();
    public List<TicketTypeViewModel> TicketTypes { get; set; } = new();

    public bool TicketSalesStarted { get; set; }  // For disabling update logic
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

public class EventSummaryViewModel
{
    public int EventId { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public string VenueName { get; set; }
    public List<TicketTypeSummaryViewModel> TicketTypes { get; set; }
}

public class TicketTypeSummaryViewModel
{
    public string Type { get; set; }
    public decimal Price { get; set; }
    public int Capacity { get; set; }
    public int ReservedCount { get; set; }
    public int PurchasedCount { get; set; }

    public int RemainingCapacity => Capacity - ReservedCount - PurchasedCount;
}

public class EventDeleteViewModel
{
    public int EventId { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string VenueName { get; set; }
    public bool TicketSalesStarted { get; set; }
}
