
using Palangan.DataLayer.Entities.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palangan.Core.Services.Interfaces.Role
{
    public  interface IRoleService
    {
        List<Palangan.DataLayer.Entities.Roles.Role> GetAllRole();

        int  AddRole(Palangan.DataLayer.Entities.Roles.Role role);
        Palangan.DataLayer.Entities.Roles.Role GetRoleById(int roleId);



        #region Permission
        List<Permission> GetAllPermissions();
        void AddPermissionToRole(int roleId, List<int> permissions);
        List<int> GetPermissionById(int roleId);

        void UpdateRolePermission(int role,List<int> permissions);

        void  DeleteRole(int roleId);
        #endregion

        #region rolePermission
        void AddUserRole(List<int>roleIds,int userId);

        List<int> GetUserRole(int userId);

        void UpdateUserRole(int userId,List<int> selectedrole);
        #endregion
    }
}
