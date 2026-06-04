using System.ComponentModel.DataAnnotations;

namespace BabySphere.Models
{
    public class ParentProfile
    {
        public int Id { get; set; }

        [Required]
        public string ParentName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string ChildName { get; set; }

        public int ChildAge { get; set; }

        public string CareNeeds { get; set; }
    }
}