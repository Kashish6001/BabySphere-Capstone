using System;
using System.ComponentModel.DataAnnotations;

namespace BabySphere.Models
{
    public class ParentSupportRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your name.")]
        public string ParentName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a support type.")]
        public string SupportType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please describe how we can help.")]
        public string Message { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}