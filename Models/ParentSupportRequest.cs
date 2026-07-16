using System;
using System.ComponentModel.DataAnnotations;

namespace BabySphere.Models
{
    public class ParentSupportRequest
    {
        public int Id { get; set; }

        [Required]
        public string ParentName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string SupportType { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}