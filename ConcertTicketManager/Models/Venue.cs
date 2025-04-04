using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

public class Venue
{
    [Key]
    public int VenueId { get; set; }

    [Required(ErrorMessage = "Venue name is required.")]
    [StringLength(200, ErrorMessage = "Venue name cannot exceed 200 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Venue location is required.")]
    [StringLength(300, ErrorMessage = "Venue location cannot exceed 300 characters.")]
    public string Location { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than zero.")]
    public int Capacity { get; set; }

    public ICollection<Event> Events { get; set; }
}
