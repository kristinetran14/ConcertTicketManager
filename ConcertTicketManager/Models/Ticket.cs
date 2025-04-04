using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Ticket
{
    [Key]
    public int TicketId { get; set; }

    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }

    [Required]
    [ForeignKey("TicketType")]
    public int TicketTypeId { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    [Display(Name = "Purchase Date")]
    public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;

    [ForeignKey("Reservation")]
    public int? ReservationId { get; set; }

    public User User { get; set; } = null!;
    public TicketType TicketType { get; set; } = null!;
    public Reservation? Reservation { get; set; }
}
