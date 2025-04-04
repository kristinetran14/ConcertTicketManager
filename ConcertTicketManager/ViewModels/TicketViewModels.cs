public class TicketAvailabilityViewModel
{
    public int TicketTypeId { get; set; }
    public string Type { get; set; }
    public decimal Price { get; set; }
    public int AvailableQuantity { get; set; }
    public int Capacity { get; set; }
    public bool IsSoldOut => AvailableQuantity <= 0;
}

public class TicketReservationViewModel
{
    public int EventId { get; set; }
    public int TicketTypeId { get; set; }
    public int Quantity { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerName { get; set; }
    public string Message { get; set; }
}

public class TicketPurchaseViewModel
{
    public int ReservationId { get; set; }
    public string Message { get; set; }
}