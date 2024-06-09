
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palangan.Core.Services.Interfaces.Slider
{
    public  interface ISliderService
    {
        List<Palangan.DataLayer.Entities.Sliders.Slider> GetAllSlider();

        void AddSlide(Palangan.DataLayer.Entities.Sliders.Slider slide,IFormFile imgUp);
        Palangan.DataLayer.Entities.Sliders.Slider GetSlideById(int slideId);

        void EditeSlide(Palangan.DataLayer.Entities.Sliders.Slider slide,IFormFile imgUp);
        void RemoveSlide(Palangan.DataLayer.Entities.Sliders.Slider slide);
    }
}
