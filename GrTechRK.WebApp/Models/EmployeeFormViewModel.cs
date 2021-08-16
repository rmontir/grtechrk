using System.ComponentModel.DataAnnotations;

namespace GrTechRK.WebApp.Models
{
    public class EmployeeFormViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string LastName { get; set; }
        public int? CompanyId { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }
    }
}
