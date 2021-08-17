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
    [Authorize(Roles = "Admin")]
    [Route("employee")]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(
            IEmployeeService employeeService
        )
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> FormAsync(int? id, CancellationToken cancellationToken)
        {
            if (id.HasValue)
            {
                EmployeeDto employee = await _employeeService.GetAsync(id.Value, cancellationToken).ConfigureAwait(false);
                EmployeeFormViewModel model = new EmployeeFormViewModel()
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    CompanyId = employee.CompanyId,
                    Email = employee.Email,
                    Phone = employee.Phone
                };
                return PartialView("_EmployeeFormPopupPartial", model);
            }
            else
            {
                return PartialView("_EmployeeFormPopupPartial", null);
            }
        }

        [HttpGet("[action]")]
        public async Task<JsonResult> ListAsync(
            [FromQuery(Name = "draw")] int? draw,
            [FromQuery(Name = "start")] int? start,
            [FromQuery(Name = "length")] int? length,
            [FromQuery(Name = "search[value]")] string search,
            [FromQuery(Name = "order[0][dir]")] string sortDir,
            [FromQuery(Name = "s")] string startDate,
            [FromQuery(Name = "e")] string endDate,
            [FromQuery(Name = "fn")] string firstName,
            [FromQuery(Name = "ln")] string lastName,
            [FromQuery(Name = "m")] string email,
            [FromQuery(Name = "c")] string companyId,
            CancellationToken cancellationToken
        )
        {
            try
            {
                int pageSize = length ?? 10;
                int skip = start ?? length ?? 10;
                int recordsTotal = 0;
                int recordsFiltered = 0;
                string sortColumn = Request.Query["columns[" + Request.Query["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

                IEnumerable<EmployeeDto> employees = await _employeeService.GetAllEmployeesAsync(cancellationToken).ConfigureAwait(false);

                recordsTotal = employees.Count();
                if (!string.IsNullOrWhiteSpace(search))
                {
                    employees = employees.Where(e => e.FirstName.Contains(search) || e.LastName.Contains(search) ||
                        e.CompanyName.Contains(search) || e.Email.Contains(search) || e.Phone.Contains(search));
                }

                if (!string.IsNullOrWhiteSpace(startDate))
                {
                    DateTime sd = DateTime.Parse(startDate);
                    employees = employees.Where(e => e.Created >= sd);
                }
                if (!string.IsNullOrWhiteSpace(endDate))
                {
                    DateTime ed = DateTime.Parse(endDate);
                    employees = employees.Where(e => e.Created <= ed);
                }
                if (!string.IsNullOrWhiteSpace(firstName))
                {
                    employees = employees.Where(e => e.FirstName.Contains(firstName));
                }
                if (!string.IsNullOrWhiteSpace(lastName))
                {
                    employees = employees.Where(e => e.LastName.Contains(lastName));
                }
                if (!string.IsNullOrWhiteSpace(email))
                {
                    employees = employees.Where(e => e.Email.Contains(email));
                }
                if (!string.IsNullOrWhiteSpace(companyId))
                {
                    int.TryParse(companyId, out int cid);
                    employees = employees.Where(e => e.CompanyId == cid || cid == 0);
                }

                recordsFiltered = employees.Count();
                if (!string.IsNullOrWhiteSpace(sortColumn) && !string.IsNullOrWhiteSpace(sortDir))
                {
                    string sort = $"{sortColumn} {sortDir}";
                    employees = sort switch
                    {
                        "fullName asc" => employees.OrderBy(e => e.FullName).Skip(skip).Take(pageSize),
                        "fullName desc" => employees.OrderByDescending(e => e.FullName).Skip(skip).Take(pageSize),
                        "companyName asc" => employees.OrderBy(e => e.CompanyName).Skip(skip).Take(pageSize),
                        "companyName desc" => employees.OrderByDescending(e => e.CompanyName).Skip(skip).Take(pageSize),
                        "email asc" => employees.OrderBy(e => e.Email).Skip(skip).Take(pageSize),
                        "email desc" => employees.OrderByDescending(e => e.Email).Skip(skip).Take(pageSize),
                        "phone asc" => employees.OrderBy(e => e.Phone).Skip(skip).Take(pageSize),
                        "phone desc" => employees.OrderByDescending(e => e.Phone).Skip(skip).Take(pageSize),
                        "created asc" => employees.OrderBy(e => e.Created).Skip(skip).Take(pageSize),
                        "created desc" => employees.OrderByDescending(e => e.Created).Skip(skip).Take(pageSize),
                        _ => employees.OrderBy(e => e.FullName).Skip(skip).Take(pageSize),
                    };
                }

                return Json(new { draw, recordsTotal, recordsFiltered, data = employees });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("[action]")]
        public async Task<DtoResponse> AddAsync(EmployeeFormViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Employee employee = new Employee()
                    {
                        Id = model.Id,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        CompanyId = model.CompanyId,
                        Email = model.Email,
                        Phone = model.Phone
                    };

                    return Response_Ok(await _employeeService.AddAsync(employee).ConfigureAwait(false));
                }
                else
                {
                    return Response_Exception(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                }
            }
            catch (Exception exc)
            {
                return Response_Exception(exc);
            }
        }

        [HttpPut]
        public async Task<DtoResponse> UpdateAsync(EmployeeFormViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Employee employee = new Employee()
                    {
                        Id = model.Id,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        CompanyId = model.CompanyId,
                        Email = model.Email,
                        Phone = model.Phone
                    };

                    return Response_Ok(await _employeeService.UpdateAsync(employee).ConfigureAwait(false));
                }
                else
                {
                    return Response_Exception(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                }
            }
            catch (Exception exc)
            {
                return Response_Exception(exc);
            }
        }

        [HttpDelete]
        public async Task<DtoResponse> DeleteAsync(int id)
        {
            try
            {
                await _employeeService.DeleteAsync(id).ConfigureAwait(false);
                return Response_Ok();
            }
            catch (Exception exc)
            {
                return Response_Exception(exc);
            }
        }
    }
}
