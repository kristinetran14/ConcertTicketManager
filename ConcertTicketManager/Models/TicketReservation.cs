using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TicketReservation
{
    [Key]
    public int ReservationId { get; set; }

    [Required(ErrorMessage = "CustomerId is required.")]
    public int CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    public Customer Customer { get; set; }

    [Required(ErrorMessage = "TicketTypeId is required.")]
    public int TicketTypeId { get; set; }

    [ForeignKey("TicketTypeId")]
    public TicketType TicketType { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Expiration time is required.")]
    public DateTime ExpirationTime { get; set; }

    public bool IsPurchased { get; set; } = false;

    // Navigation Property
    public TicketPurchase TicketPurchase { get; set; }
}
