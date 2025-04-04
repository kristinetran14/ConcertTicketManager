using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

public class Event
{
    [Key]
    public int EventId { get; set; }

    [Required]
    [ForeignKey("Venue")]
    public int VenueId { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Event Name")]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000)]
    [Display(Name = "Event Description")]
    public string Description { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.DateTime)]
    [Display(Name = "Event Date")]
    public DateTime EventDate { get; set; }

    public Venue Venue { get; set; } = null!;
    public ICollection<TicketType> TicketTypes { get; set; } = new List<TicketType>();
}
