using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Palangan.Core.Dtos.AdminUser
{
    public class AdminEditUserViewModel
    {

        public int userId { get; set; }

        public string UserName { get; set; }


        [Display(Name = "ایمیل")]
        [MaxLength(350, ErrorMessage = "تعداد کاراکتر های وارد شده بیش از حد مجاز است")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.EmailAddress, ErrorMessage = "ایمیل وارد شده معتبر نمیباشد.")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [MaxLength(350, ErrorMessage = "تعداد کاراکتر های وارد شده بیش از حد مجاز است")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; }

        [Display(Name = "شماره مبایل")]
        [MaxLength(350, ErrorMessage = "تعداد کاراکتر های وارد شده بیش از حد مجاز است")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string NumberPhone { get; set; }


        [Display(Name = "ادرس")]
        [MaxLength(350, ErrorMessage = "تعداد کاراکتر های وارد شده بیش از حد مجاز است")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Address { get; set; }

        [Display(Name = "تصویر")]
        public IFormFile UserAvatar { get; set; }

        public List<int> UserRoles { get; set; }

        public string AvatarName { get; set; }


    }
}
