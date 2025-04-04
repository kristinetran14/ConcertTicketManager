using System.ComponentModel.DataAnnotations;

namespace ConcertTicketManager.ViewModels
{
    public class TicketTypeViewModel
    {
        [Required(ErrorMessage = "Ticket type is required.")]
        [StringLength(100, ErrorMessage = "Ticket type name cannot exceed 100 characters.")]
        public string Type { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than zero.")]
        public int Capacity { get; set; }
    }
}
