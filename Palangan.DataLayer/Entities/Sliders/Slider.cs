using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palangan.DataLayer.Entities.Sliders
{
    public class Slider
    {
        [Key]
        public int SliderId { get; set; }


        [Display(Name ="عنوان")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string Title { get; set; }


        [Display(Name ="تصویر")]
        public string SliderImage { get; set; }
    }
}