using System;
using System.ComponentModel.DataAnnotations;

namespace TheWorld.ViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage ="Please enter a name")]
        [StringLength(255,MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a valid email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(1024, MinimumLength = 5)]
        public string Message { get; set; }
        
    }
}