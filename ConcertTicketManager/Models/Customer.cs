using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [Required(ErrorMessage = "Customer name is required.")]
    [StringLength(150, ErrorMessage = "Customer name cannot exceed 150 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
    public string Email { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<TicketReservation> Reservations { get; set; }
}
