using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Quiz.Models.ManageViewModels
{
    public class ConfigureTwoFactorViewModel
    {
        public ICollection<SelectListItem> Providers { get; set; }
        public string SelectedProvider { get; set; }
    }
}