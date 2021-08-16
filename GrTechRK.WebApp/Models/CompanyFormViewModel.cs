using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GrTechRK.WebApp.Models
{
    public class CompanyFormViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        public IFormFile Logo { get; set; }
        [Url(ErrorMessage = "Invalid website link")]
        public string Website { get; set; }
    }
}
