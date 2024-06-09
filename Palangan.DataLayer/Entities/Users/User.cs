using Palangan.DataLayer.Entities.Comments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palangan.DataLayer.Entities.Users
{
    public class User
    {
        public User()
        {

        }
        [Key]
        public int UserId { get; set; }

        

        [Display(Name ="نام کاربری")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "تعداد کاراکتر های وارد شده بیش از حد مجاز است")]
        public string UserName { get; set; }



        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "تعداد کاراکتر های وارد شده بیش از حد مجاز است")]
        public string Email { get; set; }


        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "تعداد کاراکتر های وارد شده بیش از حد مجاز است")]
        public string Password { get; set; }



        [Display(Name = "وضعیت")]      
        public bool IsActive { get; set; }


        [Display(Name = "شماره مبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "تعداد کاراکتر های وارد شده بیش از حد مجاز است")]
        public string NumberPhone { get; set; }


        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(600, ErrorMessage = "تعداد کاراکتر های وارد شده بیش از حد مجاز است")]
        public string Address { get; set; }



        [Display(Name = "کد فعال سازی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "تعداد کاراکتر های وارد شده بیش از حد مجاز است")]
        public string ActiveCode { get; set; }


        [Display(Name = "تصویر کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]      
        public string UserAvatar { get; set; }


        [Display(Name = "تاریخ ثبت نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]       
        public DateTime RegisterDate { get; set; }


        [Display(Name = "حذف شده؟")]    
        public bool IsDelete { get; set; }

        #region Relation
        public virtual List<UserRole> UserRoles { get; set; }
        public virtual List<ProductComment> ProductComments { get; set; }
        #endregion

    }
}
