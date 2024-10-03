using Demo.API.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo.MVC.ViewModels
{
    public class VillaNumberUpdateVM
    {
        public VillaNumberUpdateVM()
        {
            VillaNumber = new VillaNumberUpdateDTO();
        }
        public VillaNumberUpdateDTO VillaNumber { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}
