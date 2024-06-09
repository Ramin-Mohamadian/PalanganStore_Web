using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palangan.DataLayer.Entities.Roles
{
    public class RolePermission
    {
        [Key]
        public int RP_ID { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        #region Relation
        public Role Role { get; set; }
        public Permissions.Permission Permission { get; set; }

        #endregion
    }
}
