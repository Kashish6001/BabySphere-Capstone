using System.ComponentModel.DataAnnotations;

namespace BabySphere.Models
{
    public class Booking
    {
        [Required]
        public string ParentName { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Time { get; set; }

        public int BabysitterId { get; set; }
    }
}