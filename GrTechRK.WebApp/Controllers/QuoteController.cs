using GrTechRK.BSL.Interfaces;
using GrTechRK.Database.Models;
using GrTechRK.DTO;
using GrTechRK.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GrTechRK.WebApp.Controllers
{
    [AllowAnonymous]
    [Route("daily-quotes")]
    public class QuoteController : BaseController
    {
        //private readonly IEmployeeService _employeeService;

        public QuoteController(
            //IEmployeeService employeeService
        )
        {
            //_employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
