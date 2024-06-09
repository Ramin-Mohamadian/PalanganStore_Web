using Palangan.DataLayer.Entities.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palangan.DataLayer.Entities.Users
{
    public  class UserRole
    {
        public UserRole()
        {
            
        }
        [Key]
        public int UR_Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        #region Relations
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
        #endregion
    }
}
