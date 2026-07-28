using System.ComponentModel.DataAnnotations;

namespace BabySphere.Models
{
    public class Babysitter
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Range(0, 60)]
        public int Experience { get; set; }

        [Range(0, 500)]
        public decimal HourlyRate { get; set; }

        [Range(0, 5)]
        public double Rating { get; set; }

        public string Skills { get; set; } = string.Empty;
    }
}