using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Palangan.Core.Security;
using Palangan.Core.Services.Interfaces.Role;
using Palangan.DataLayer.Entities.Roles;

namespace PalanganStore_Web.Pages.Admin.Roles
{
    [PermissionChecker(1)]
    [PermissionChecker(6)]
    [PermissionChecker(9)]
    public class DeleteRoleModel : PageModel
    {
        private IRoleService _roleService;
        public DeleteRoleModel(IRoleService roleService)
        {
                _roleService = roleService;
        }

        public Role Role { get; set; }
        public void OnGet(int id)
        {
            Role=_roleService.GetRoleById(id);
            
        }

        public IActionResult OnPost(int id)
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            _roleService.DeleteRole(id);

            return RedirectToPage("Index");
        }
    }
}
