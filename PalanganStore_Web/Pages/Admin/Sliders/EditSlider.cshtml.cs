using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Palangan.Core.Services.Interfaces.Slider;
using Palangan.DataLayer.Entities.Sliders;

namespace PalanganStore_Web.Pages.Admin.Sliders
{
    public class EditSliderModel : PageModel
    {
        private ISliderService _slider;
        public EditSliderModel(ISliderService slider)
        {
            _slider = slider;
        }


        [BindProperty]
        public Slider slide { get; set; }
        public void OnGet(int id)
        {
            slide=_slider.GetSlideById(id);
        }

        public IActionResult OnPost(IFormFile imgUp)
        {
            _slider.EditeSlide(slide, imgUp);
            return RedirectToPage("Index");
        }


    }
}
