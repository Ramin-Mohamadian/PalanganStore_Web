using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Palangan.Core.Services.Interfaces.Slider;
using Palangan.DataLayer.Entities.Sliders;

namespace PalanganStore_Web.Pages.Admin.Sliders
{
    public class RemoveSliderModel : PageModel
    {
        private ISliderService _slider;
        public RemoveSliderModel(ISliderService slider)
        {
            _slider = slider;
        }

        [BindProperty]
        public Slider slide { get; set; }
        public void OnGet(int id)
        {
            slide=_slider.GetSlideById(id);
            
        }

        public IActionResult OnPost()
        {
            _slider.RemoveSlide(slide);

            return RedirectToPage("Index");
        }
    }
}
