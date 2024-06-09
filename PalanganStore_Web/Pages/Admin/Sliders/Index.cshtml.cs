using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Palangan.Core.Services.Interfaces.Slider;
using Palangan.DataLayer.Entities.Sliders;
using System.Collections.Generic;

namespace PalanganStore_Web.Pages.Admin.Sliders
{
    public class IndexModel : PageModel
    {
        private ISliderService _slider;
        public IndexModel(ISliderService slider)
        {
                _slider = slider;
        }


        public List<Slider> Slide { get; set; }
        public void OnGet()
        {
            Slide=_slider.GetAllSlider();
        }
    }
}
