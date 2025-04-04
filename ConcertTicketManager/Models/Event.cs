using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

public class Event
{
    [Key]
    public int EventId { get; set; }

    [Required(ErrorMessage = "Event name is required.")]
    [StringLength(200, ErrorMessage = "Event name cannot exceed 200 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Event date is required.")]
    [DataType(DataType.DateTime)]
    [Range(typeof(DateTime), "2025-04-03", "9999-12-31", ErrorMessage = "Event date must be a valid future date.")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Event description is required.")]
    [StringLength(500, ErrorMessage = "Event description cannot exceed 500 characters.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "VenueId is required.")]
    public int VenueId { get; set; }

    [ForeignKey("VenueId")]
    public Venue Venue { get; set; }

    public ICollection<TicketType> TicketTypes { get; set; }
}
