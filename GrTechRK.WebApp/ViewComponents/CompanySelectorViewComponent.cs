using GrTechRK.BSL.Interfaces;
using GrTechRK.DTO;
using GrTechRK.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GrTechRK.WebApp.ViewComponents
{
    [ViewComponent(Name = "CompanySelector")]
    public class CompanySelectorViewComponent : ViewComponent
    {
		private readonly ICompanyService _companyService;

		public CompanySelectorViewComponent(
			ICompanyService companyService
		)
		{
            _companyService = companyService;
        }

		public async Task<IViewComponentResult> InvokeAsync(string id, string selected)
		{
			ImmutableHashSet<SelectListItem> items = await GetItemsAsync().ConfigureAwait(false);
			SelectorViewModel model = new SelectorViewModel(
				id: id,
				selectedValue: selected,
				data: items
			);
			return View(model);
		}

		private async Task<ImmutableHashSet<SelectListItem>> GetItemsAsync()
		{
			ImmutableHashSet<CompanyDto> results = await _companyService.GetAllCompaniesAsync(CancellationToken.None).ConfigureAwait(false);
			return results.Select(r => new SelectListItem(r.Name, r.Id.ToString())).ToImmutableHashSet();
		}
	}
}
