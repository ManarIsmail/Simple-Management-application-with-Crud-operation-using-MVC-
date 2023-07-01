using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models
{
    public class RegisterViewModel
    {

        [Required]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Minimum Length is 6 chars")]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password mismatch")]
        public string ConfirmPassword { get; set; }
        [Required]
        public bool IsAgree { get; set; }
    }
}
