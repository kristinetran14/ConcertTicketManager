using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

public class TicketType
{
    [Key]
    public int TicketTypeId { get; set; }

    [Required(ErrorMessage = "EventId is required.")]
    public int EventId { get; set; }

    [ForeignKey("EventId")]
    public Event Event { get; set; }

    [Required(ErrorMessage = "Ticket type is required.")]
    [StringLength(100, ErrorMessage = "Ticket type cannot exceed 100 characters.")]
    public string Type { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than zero.")]
    public int Capacity { get; set; }

    public ICollection<TicketReservation> Reservations { get; set; }
}
