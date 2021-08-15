using Microsoft.AspNetCore.Http;
using System;

namespace GrTechRK.WebApp.Models
{
    public class CompanyFormViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IFormFile Logo { get; set; }
        public string Website { get; set; }
    }
}
