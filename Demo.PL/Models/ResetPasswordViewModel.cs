using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        [MinLength(6, ErrorMessage = "Minimum Length is 6 chars")]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password mismatch")]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
