using Microsoft.AspNetCore.Mvc;
using Palangan.Core.Services.Interfaces.Slider;
using System.Threading.Tasks;

namespace PalanganStore_Web.Components
{
    public class SliderComponent:ViewComponent
    {
        private ISliderService _sliderService;
        public SliderComponent(ISliderService slider)
        {
            _sliderService = slider;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("SliderMain", _sliderService.GetAllSlider()));
        }
    }
}
