using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TicketPurchase
{
    [Key]
    public int PurchaseId { get; set; }

    [Required(ErrorMessage = "ReservationId is required.")]
    public int ReservationId { get; set; }

    [ForeignKey("ReservationId")]
    public TicketReservation Reservation { get; set; }

    [Required]
    public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "Payment method is required.")]
    [StringLength(50, ErrorMessage = "Payment method cannot exceed 50 characters.")]
    public string PaymentMethod { get; set; }
}
