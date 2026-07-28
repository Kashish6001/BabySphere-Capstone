using System;
using System.ComponentModel.DataAnnotations;

namespace BabySphere.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Parent name is required")]
        public string ParentName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a valid date")]
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }

        [Required]
        public string BabysitterName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Start time is required")]
        public TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "End time is required")]
        public TimeSpan EndTime { get; set; }

        public string Notes { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending";
    }
}