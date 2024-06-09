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
    [PermissionChecker(4)]
    public class EditeUserModel : PageModel
    {
        private IAccountService _accountService;
        private IRoleService _roleService;

        public EditeUserModel(IAccountService accountService,IRoleService roleService)
        {
            _accountService = accountService;
            _roleService = roleService;
        }



        [BindProperty]
        public AdminEditUserViewModel editUserViewModel { get; set; }
        public void OnGet(int id)
        {
            editUserViewModel=_accountService.GetUserForEdit(id);
            ViewData["Roles"]=_roleService.GetAllRole();
           // ViewData["geteditrole"]=_roleService.GetUserRole(id);           
        }


        public IActionResult OnPost(List<int> selectedRole)
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }


            
            _accountService.EditeUser(editUserViewModel);
            _roleService.UpdateUserRole(editUserViewModel.userId, selectedRole);           

            return RedirectToPage("Index");
        }
    }
}
