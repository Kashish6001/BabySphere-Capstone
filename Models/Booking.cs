using System.ComponentModel.DataAnnotations;

namespace BabySphere.Models
{
    public class Booking
    {
        [Key] 
        public int BookingId { get; set; }

        [Required(ErrorMessage = "Parent name is required")]
        public string ParentName { get; set; }

        [Required(ErrorMessage = "Please select a valid date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please select a valid time")]
        public string Time { get; set; } 

        [Required]
        public string BabysitterName { get; set; }
    }
}