using Palangan.DataLayer.Entities.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palangan.DataLayer.Entities.Permissions
{
    public  class Permission
    {
        [Key]
        public int PermissionId { get; set; }

        [Display(Name ="")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "تعداد کاراکتر های وارد شده بیش از حد مجاز است")]
        public string PermissionTitle { get; set; }


        public int? ParentID { get; set; }

        #region Relation
        [ForeignKey("ParentID")]
        public List<Permission> Permissions { get; set; }
        public List<RolePermission> RolePermissions { get; set; }
        #endregion
    }
}
