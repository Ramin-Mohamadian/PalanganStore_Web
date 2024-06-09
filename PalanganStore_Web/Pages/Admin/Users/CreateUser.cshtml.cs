using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Palangan.Core.Dtos.AdminUser;
using Palangan.Core.Security;
using Palangan.Core.Services.Interfaces;
using Palangan.Core.Services.Interfaces.Role;
using System.Collections.Generic;

namespace PalanganStore_Web.Pages.Admin.Users
{
    [PermissionChecker(1)]
    [PermissionChecker(2)]
    [PermissionChecker(3)]
    public class CreateUserModel : PageModel
    {
        private IAccountService _accountService;
        private IRoleService _roleService;
        public CreateUserModel(IAccountService accountService , IRoleService roleService)
        {
            _accountService = accountService;
            _roleService = roleService;
        }


        [BindProperty]
        public AdminAddUserViewModel  CreateUserVm { get; set; }

        public void OnGet()
        {
            ViewData["Roles"]=_roleService.GetAllRole();
        }


        public IActionResult OnPost(List<int> selectedRole)
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            int userid=_accountService.AddminAddUser(CreateUserVm);
            _roleService.AddUserRole(selectedRole,userid);

            return RedirectToPage("Index");
            
        }
    }
}
