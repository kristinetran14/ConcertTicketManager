using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Logging;

public class Venue
{
    [Key]
    public int VenueId { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Venue Name")]
    public string Name { get; set; } = string.Empty;

    [StringLength(255)]
    [Display(Name = "Location")]
    public string Location { get; set; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be at least 1.")]
    [Display(Name = "Capacity")]
    public int Capacity { get; set; }

    public ICollection<Event> Events { get; set; } = new List<Event>();
}
