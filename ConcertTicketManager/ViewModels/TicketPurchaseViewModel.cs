using System.ComponentModel.DataAnnotations;

public class TicketPurchaseViewModel
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }

    public List<PurchaseReservationSummary> AvailableReservations { get; set; } = new();

    [Required(ErrorMessage = "Please select a reservation to purchase.")]
    public int? SelectedReservationId { get; set; }

    [Required(ErrorMessage = "Payment method is required.")]
    public string PaymentMethod { get; set; }

    public string? Message { get; set; }
}

public class PurchaseReservationSummary
{
    public int ReservationId { get; set; }
    public string EventName { get; set; }
    public string TicketType { get; set; }
    public int Quantity { get; set; }
    public DateTime ExpirationTime { get; set; }
    public decimal TotalPrice { get; set; }
}
