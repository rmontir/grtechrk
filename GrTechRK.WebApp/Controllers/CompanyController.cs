using GrTechRK.BSL.Interfaces;
using GrTechRK.Database.Models;
using GrTechRK.DTO;
using GrTechRK.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GrTechRK.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("company")]
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(
            ICompanyService companyService
        )
        {
            _companyService = companyService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> DetailAsync(int id)
        {
            CompanyDto company = await _companyService.GetAsync(id, CancellationToken.None).ConfigureAwait(false);
            return ViewComponent("CompanyDetail", company);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> FormAsync(int? id, CancellationToken cancellationToken)
        {
            if (id.HasValue)
            {
                CompanyDto company = await _companyService.GetAsync(id.Value, cancellationToken).ConfigureAwait(false);
                CompanyFormViewModel model = new CompanyFormViewModel()
                {
                    Id = company.Id,
                    Name = company.Name,
                    Email = company.Email,
                    Logo = null,
                    Website = company.Website
                };
                return PartialView("_CompanyFormPopupPartial", model);
            }
            else
            {
                return PartialView("_CompanyFormPopupPartial", null);
            }
        }

        [HttpGet("[action]")]
        public async Task<JsonResult> ListAsync(
            [FromQuery(Name = "draw")] int? draw,
            [FromQuery(Name = "start")] int? start,
            [FromQuery(Name = "length")] int? length,
            [FromQuery(Name = "search[value]")] string search,
            [FromQuery(Name = "order[0][dir]")] string sortDir,
            CancellationToken cancellationToken
        )
        {
            try
            {
                int pageSize = length ?? 10;
                int skip = start.HasValue ? start.Value / (length ?? 10) : 0;
                int recordsTotal = 0;
                string sortColumn = Request.Query["columns[" + Request.Query["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

                IEnumerable<CompanyDto> companies = await _companyService.GetAllCompaniesAsync(cancellationToken).ConfigureAwait(false);

                if (!string.IsNullOrWhiteSpace(search))
                {
                    companies = companies.Where(e => e.Name.Contains(search) || e.Email.Contains(search) ||
                        e.Website.Contains(search));
                }

                recordsTotal = companies.Count();
                if (!string.IsNullOrWhiteSpace(sortColumn) && !string.IsNullOrWhiteSpace(sortDir))
                {
                    string sort = $"{sortColumn} {sortDir}";
                    companies = sort switch
                    {
                        "name asc" => companies.OrderBy(e => e.Name).Skip(skip).Take(pageSize),
                        "name desc" => companies.OrderByDescending(e => e.Name).Skip(skip).Take(pageSize),
                        "email asc" => companies.OrderBy(e => e.Email).Skip(skip).Take(pageSize),
                        "email desc" => companies.OrderByDescending(e => e.Email).Skip(skip).Take(pageSize),
                        "website asc" => companies.OrderBy(e => e.Website).Skip(skip).Take(pageSize),
                        "website desc" => companies.OrderByDescending(e => e.Website).Skip(skip).Take(pageSize),
                        _ => companies.OrderBy(e => e.Name).Skip(skip).Take(pageSize),
                    };
                }

                return Json(new { data = companies });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("[action]")]
        public async Task<DtoResponse> AddAsync(CompanyFormViewModel model)
        {
            try
            {
                if (!Uri.IsWellFormedUriString(model.Website, UriKind.Absolute)) throw new ArgumentException("Invalid website link");

                using MemoryStream ms = new MemoryStream();
                if (model.Logo != null)
                {
                    model.Logo.CopyTo(ms);
                }

                Company company = new Company()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Email = model.Email,
                    Logo = model.Logo != null ? ms.ToArray() : null,
                    Website = !string.IsNullOrWhiteSpace(model.Website) ? new Uri(model.Website, UriKind.Absolute) : null
                };

                return Response_Ok(await _companyService.AddAsync(company).ConfigureAwait(false));
            }
            catch (Exception exc)
            {
                return Response_Exception(exc);
            }
        }

        [HttpPut]
        public async Task<DtoResponse> UpdateAsync(CompanyFormViewModel model)
        {
            try
            {
                if (!Uri.IsWellFormedUriString(model.Website, UriKind.Absolute)) throw new ArgumentException("Invalid website link");

                Company company = new Company()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Email = model.Email,
                    Logo = null,
                    Website = !string.IsNullOrWhiteSpace(model.Website) ? new Uri(model.Website, UriKind.Absolute) : null
                };

                return Response_Ok(await _companyService.UpdateAsync(company).ConfigureAwait(false));
            }
            catch (Exception exc)
            {
                return Response_Exception(exc);
            }
        }

        [HttpPut("[action]")]
        public async Task<DtoResponse> UploadAsync(CompanyFormViewModel model)
        {
            try
            {
                using MemoryStream ms = new MemoryStream();
                if (model.Logo != null)
                {
                    model.Logo.CopyTo(ms);
                }

                return Response_Ok(await _companyService.UpdateLogoAsync(model.Id.Value, ms.ToArray()).ConfigureAwait(false));
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
                await _companyService.DeleteAsync(id).ConfigureAwait(false);
                return Response_Ok();
            }
            catch (Exception exc)
            {
                return Response_Exception(exc);
            }
        }
    }
}
