using System.ComponentModel.DataAnnotations;

namespace BabySphere.Models
{
    public class Babysitter
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Experience { get; set; }

        public decimal HourlyRate { get; set; }

        public double Rating { get; set; }

        public string Skills { get; set; }
    }
}