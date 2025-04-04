using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

public class TicketType
{
    [Key]
    public int TicketTypeId { get; set; }

    [Required]
    [ForeignKey("Event")]
    public int EventId { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "Ticket Type Name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
    [DataType(DataType.Currency)]
    [Display(Name = "Price")]
    public decimal Price { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    [Display(Name = "Available Tickets")]
    public int Quantity { get; set; }

    public Event Event { get; set; } = null!;
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
