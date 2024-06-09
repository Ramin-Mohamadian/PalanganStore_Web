using Microsoft.IdentityModel.Tokens;
using Palangan.Core.Services.Interfaces;
using Palangan.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palangan.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private MyContext _context;
        public PermissionService(MyContext context)
        {
            _context = context;
        }

        public bool CheckPermission(int permissionId, string userName)
        {
            int userId = _context.Users.Single(u => u.UserName == userName).UserId;

            List<int> UserRoles = _context.UserRoles
                .Where(r => r.UserId == userId).Select(r => r.RoleId).ToList();

            if (!UserRoles.Any())
                return false;

            List<int> RolesPermission = _context.RolePermissions
                .Where(p => p.PermissionId == permissionId)
                .Select(p => p.RoleId).ToList();

            return RolesPermission.Any(p => UserRoles.Contains(p));


        }
       
    }
}
