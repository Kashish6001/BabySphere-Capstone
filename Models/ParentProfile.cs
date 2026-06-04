using System.ComponentModel.DataAnnotations;

namespace BabySphere.Models
{
    public class ParentProfile
    {
        public int Id { get; set; }

        [Required]
        public string ParentName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string ChildName { get; set; } = string.Empty;

        [Range(0, 12)]
        public int ChildAge { get; set; }

        [Required]
        public string SupportCategory { get; set; } = string.Empty;

        public string CareNeeds { get; set; } = string.Empty;

        public string Recommendation { get; set; } = string.Empty;

        public string TicketNumber { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending";
    }
}