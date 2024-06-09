using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palangan.Core.Dtos.Product
{
    public  class EditProductViewModel
    {
        [Key]
        public int ProductId { get; set; }


        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int GroupId { get; set; }



        public int? SubGroup { get; set; }



        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "تعداد کاراکتر های وارد شده بیش از حد مجاز است")]
        public string Title { get; set; }




        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string Description { get; set; }



        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }



        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ProductImage { get; set; }
               


        [Display(Name = "برچسب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(850, ErrorMessage = "تعداد کاراکتر های وارد شده بیش از حد مجاز است")]
        public string Tags { get; set; }




       
    }
}
