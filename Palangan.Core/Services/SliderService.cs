using Microsoft.AspNetCore.Http;
using Palangan.Core.Generator;
using Palangan.Core.Services.Interfaces.Slider;
using Palangan.DataLayer.Context;
using Palangan.DataLayer.Entities.Products;
using Palangan.DataLayer.Entities.Sliders;
using Palangan.DataLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palangan.Core.Services
{
    public class SliderService : ISliderService
    {
        private MyContext _context;
        public SliderService(MyContext context)
        {
            _context = context;
        }
        public List<Slider> GetAllSlider()
        {
            return _context.Sliders.ToList();
        }

        public void AddSlide(Slider slide, IFormFile imgUp)
        {
            if (imgUp != null)
            {
                slide.SliderImage=Guid.NewGuid().ToString()+Path.GetExtension(imgUp.FileName);
                var imgpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/Slider", slide.SliderImage);
                using (var stream = new FileStream(imgpath, FileMode.Create))
                {
                    imgUp.CopyTo(stream);
                }

            }
            else
            {
                slide.SliderImage="Default.jpg";
            }
            _context.Sliders.Add(slide);
            _context.SaveChanges();
        }

        public void EditeSlide(Slider slide, IFormFile file)
        {


            if (file != null)
            {
                string imagepath = "";
                if (slide.SliderImage!="DefaultProfile.png")
                {
                    imagepath=Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/Slider", slide.SliderImage);
                    if (File.Exists(imagepath))
                    {
                        File.Delete(imagepath);
                    }
                }

                slide.SliderImage=NameGenerator.GenerateUniqeName()+Path.GetExtension(file.FileName);

                imagepath= Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/Slider", slide.SliderImage);
                using (var stream = new FileStream(imagepath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            _context.Sliders.Update(slide);
            _context.SaveChanges();
        }



        public void RemoveSlide(Slider slide)
        {

            string imagepath = "";
            if (slide.SliderImage!="")
            {
                imagepath=Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/Slider", slide.SliderImage);
                if (File.Exists(imagepath))
                {
                    File.Delete(imagepath);
                }
            }

            _context.Sliders.Remove(slide);
            _context.SaveChanges();
        }

        public Slider GetSlideById(int slideId)
        {
            return _context.Sliders.Find(slideId);
        }
    }
}
