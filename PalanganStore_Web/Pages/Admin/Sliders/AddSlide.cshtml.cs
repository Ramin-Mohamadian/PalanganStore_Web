using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Palangan.Core.Services.Interfaces.Slider;
using Palangan.DataLayer.Entities.Sliders;
using System.Xml.Linq;

namespace PalanganStore_Web.Pages.Admin.Sliders
{
    public class AddSlideModel : PageModel
    {
        private ISliderService _slider;
        public AddSlideModel(ISliderService slider)
        {
            _slider = slider;
        }
        [BindProperty]
        public Slider  slide { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost(IFormFile imgUp)
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
           
               _slider.AddSlide(slide,imgUp);
          
            return RedirectToPage("Index");
        }
    }
}
