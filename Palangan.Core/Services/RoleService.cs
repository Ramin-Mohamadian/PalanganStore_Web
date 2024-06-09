using Microsoft.EntityFrameworkCore;
using Palangan.Core.Services.Interfaces.Role;
using Palangan.DataLayer.Context;
using Palangan.DataLayer.Entities.Permissions;
using Palangan.DataLayer.Entities.Roles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palangan.Core.Services
{
    public  class RoleService:IRoleService
    {

        private MyContext _context;

        public RoleService(MyContext Context)
        {
                _context = Context;
        }

     

        public List<Role> GetAllRole()
        {
            return _context.Roles.ToList();
        }

     

        public List<Permission> GetAllPermissions()
        {
             return _context.Permissions.ToList();
        }


        public int AddRole(Role role)
        {
            Role addrole = new Role()
            {
                RoleTitle=role.RoleTitle,
                IsDelete=false,
            };

            _context.Roles.Add(addrole);
            _context.SaveChanges();
            return addrole.RoleId;
        }


        public void AddPermissionToRole(int roleId, List<int> permissions)
        {
            foreach(var p in permissions)
            {
                _context.RolePermissions.Add(new RolePermission()
                {
                    RoleId = roleId,
                    PermissionId=p
                });
               

            }
            _context.SaveChanges();
        }

        public Role GetRoleById(int roleId)
        {
            return _context.Roles.Find(roleId);
        }

        public List<int> GetPermissionById(int roleId)
        {
            return _context.RolePermissions.Where(r=>r.RoleId==roleId).Select(r=>r.PermissionId).ToList();
        }

        public void UpdateRolePermission(int role, List<int> permissions)
        {
            _context.RolePermissions.Where(r => r.RoleId==role).ToList().ForEach(r => _context.Remove(r));

            AddPermissionToRole(role, permissions);
        }

        public void DeleteRole(int roleId)
        {
            var role=_context.Roles.Find(roleId);   
            role.IsDelete = true;
            _context.Update(role);
            _context.SaveChanges();
        }

        public void AddUserRole(List<int> roleIds, int userId)
        {
            foreach(int roleId in roleIds)
            {
                _context.UserRoles.Add(new DataLayer.Entities.Users.UserRole()
                {
                  RoleId= roleId,
                  UserId= userId,
                });
               
            }
            _context.SaveChanges();
        }

        public List<int> GetUserRole(int userId)
        {
            return _context.UserRoles.Where(u=>u.UserId==userId).Select(u=>u.RoleId).ToList();
        }

        public void UpdateUserRole(int userId, List<int> selectedrole)
        {       
            _context.UserRoles.Where(r => r.UserId==userId).ToList().ForEach(r => _context.UserRoles.Remove(r));
            _context.SaveChanges();
            AddUserRole(selectedrole, userId);
        }
    }
}
