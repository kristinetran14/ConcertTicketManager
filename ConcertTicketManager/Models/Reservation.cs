using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Reservation
{
    [Key]
    public int ReservationId { get; set; }

    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }

    [Required]
    [ForeignKey("TicketType")]
    public int TicketTypeId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    [Display(Name = "Reserved Quantity")]
    public int Quantity { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    [Display(Name = "Reservation Time")]
    public DateTime ReservedAt { get; set; } = DateTime.UtcNow;

    [Required]
    [DataType(DataType.DateTime)]
    [Display(Name = "Expiration Time")]
    public DateTime ExpiresAt { get; set; }

    [Required]
    [Display(Name = "Cancelled?")]
    public bool IsCancelled { get; set; } = false;

    public User User { get; set; } = null!;
    public TicketType TicketType { get; set; } = null!;
}
