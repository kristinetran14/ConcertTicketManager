using System.ComponentModel.DataAnnotations;

public class CancelReservationViewModel
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }

    public List<ReservationSummary> Reservations { get; set; } = new();

    public List<int> SelectedReservationIds { get; set; } = new();

    public string? Message { get; set; }
}

public class ReservationSummary
{
    public int ReservationId { get; set; }
    public string EventName { get; set; }
    public string TicketType { get; set; }
    public int Quantity { get; set; }
    public DateTime ExpirationTime { get; set; }
}
