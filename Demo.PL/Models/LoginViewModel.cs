using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Minimum Length is 6 chars")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
