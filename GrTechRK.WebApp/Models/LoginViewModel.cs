using System.ComponentModel.DataAnnotations;

namespace GrTechRK.WebApp.Models
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email format")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
