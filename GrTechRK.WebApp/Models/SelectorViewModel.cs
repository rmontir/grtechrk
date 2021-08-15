using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Immutable;

namespace GrTechRK.WebApp.Models
{
    public class SelectorViewModel
    {
        public string SelectedValue { get; set; }
        public ImmutableHashSet<SelectListItem> Data { get; set; }

        public SelectorViewModel(
            string selectedValue,
            ImmutableHashSet<SelectListItem> data
        )
        {
            SelectedValue = selectedValue;
            Data = data;
        }
    }
}
