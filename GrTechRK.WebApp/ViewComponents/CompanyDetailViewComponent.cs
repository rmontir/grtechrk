using GrTechRK.BSL.Interfaces;
using GrTechRK.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace GrTechRK.WebApp.ViewComponents
{
    [ViewComponent(Name = "CompanyDetail")]
    public class CompanyDetailViewComponent : ViewComponent
    {
		private readonly ICompanyService _companyService;

		public CompanyDetailViewComponent(
			ICompanyService companyService
		)
		{
			_companyService = companyService;
		}

		public async Task<IViewComponentResult> InvokeAsync(int id)
		{
			CompanyDto model = await GetItemAsync(id).ConfigureAwait(false);
			return View(model);
		}

		private async Task<CompanyDto> GetItemAsync(int id)
		{
			return await _companyService.GetAsync(id, CancellationToken.None).ConfigureAwait(false);
		}
	}
}
