using Palangan.DataLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palangan.DataLayer.Entities.Roles
{
    public class Role
    {
        public Role()
        {
            
        }
        [Key]
        public int RoleId { get; set; }

        [Display(Name ="عنوان نقش")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "تعداد کاراکتر های وارد شده بیش از حد مجاز است")]
        public string RoleTitle { get; set; }


        [Display(Name ="حذف شده")]
        public bool IsDelete { get; set; }




        #region Relation
        public virtual List<UserRole>  UserRoles { get; set; }
        public virtual List<RolePermission> RolePermissions { get; set; }
        #endregion
    }
}
