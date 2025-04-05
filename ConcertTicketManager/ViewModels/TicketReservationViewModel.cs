using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

public class TicketReservationViewModel
{
    [Required(ErrorMessage = "Customer name is required.")]
    public string CustomerName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }

    [Required]
    public int EventId { get; set; }

    [Required]
    public int TicketTypeId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
    public int Quantity { get; set; }

    [BindNever]
    public DateTime ExpirationTime { get; set; }

    public string? Message { get; set; }

    [BindNever]
    public List<SelectListItem> Events { get; set; } = new();

    [BindNever]
    public List<SelectListItem> TicketTypes { get; set; } = new();
}
