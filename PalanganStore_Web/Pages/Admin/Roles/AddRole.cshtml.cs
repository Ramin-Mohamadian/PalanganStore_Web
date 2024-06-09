using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Palangan.Core.Security;
using Palangan.Core.Services.Interfaces.Role;
using Palangan.DataLayer.Entities.Roles;
using System.Collections.Generic;

namespace PalanganStore_Web.Pages.Admin.Roles
{
    [PermissionChecker(1)]
    [PermissionChecker(6)]
    [PermissionChecker(7)]
    public class AddRoleModel : PageModel
    {
        private IRoleService _roleService;
        public AddRoleModel(IRoleService roleService)
        {
                _roleService = roleService;
        }

        [BindProperty]
        public Role role { get; set; }
        public void OnGet()
        {
            ViewData["permission"]=_roleService.GetAllPermissions();
        }

        public IActionResult OnPost(List<int> SelectedPermission)
        {

            if(!ModelState.IsValid)
            {
                return Page();
            }
       
           var roleid= _roleService.AddRole(role);

            _roleService.AddPermissionToRole(roleid, SelectedPermission);
            
            return RedirectToPage("Index");
        }
    }
}
