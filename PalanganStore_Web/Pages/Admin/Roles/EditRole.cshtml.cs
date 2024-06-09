using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Palangan.Core.Security;
using Palangan.Core.Services.Interfaces.Role;
using Palangan.DataLayer.Entities.Permissions;
using Palangan.DataLayer.Entities.Roles;
using System.Collections.Generic;

namespace PalanganStore_Web.Pages.Admin.Roles
{
    [PermissionChecker(1)]
    [PermissionChecker(6)]
    [PermissionChecker(8)]
    public class EditRoleModel : PageModel
    {
        

        private IRoleService _roleService;
        public EditRoleModel(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [BindProperty]
        public Role role { get; set; }



        public void OnGet(int id)
        {
            
            role=_roleService.GetRoleById(id);
            ViewData["permission"]=_roleService.GetAllPermissions();
            ViewData["getpermission"]=_roleService.GetPermissionById(id);
        }



        public IActionResult OnPost(List<int> SelectedPermission)
        {

            if(!ModelState.IsValid)
            {
                return Page();
            }
            _roleService.UpdateRolePermission(role.RoleId,SelectedPermission);
            return RedirectToPage("Index");
        }
    }
}
