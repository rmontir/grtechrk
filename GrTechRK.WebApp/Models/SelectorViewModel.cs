using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Immutable;

namespace GrTechRK.WebApp.Models
{
    public class SelectorViewModel
    {
        public string Id { get; set; }
        public string SelectedValue { get; set; }
        public ImmutableHashSet<SelectListItem> Data { get; set; }

        public SelectorViewModel(
            string id,
            string selectedValue,
            ImmutableHashSet<SelectListItem> data
        )
        {
            Id = id;
            SelectedValue = selectedValue;
            Data = data;
        }
    }
}
