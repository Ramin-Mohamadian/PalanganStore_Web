using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Palangan.Core.Security;
using Palangan.Core.Services.Interfaces;

namespace PalanganStore_Web.Pages.Admin.Users
{
    [PermissionChecker(1)]
    [PermissionChecker(2)]
    [PermissionChecker(5)]
    public class DeleteUserModel : PageModel
    {
        private IAccountService _accountService;

        public DeleteUserModel(IAccountService accountService)
        {
            _accountService = accountService;
        }


        

        public void OnGet(int id)
        {

        }


        
        public IActionResult OnPost(int id)
        {
            _accountService.DeleteUser(id);
            return RedirectToPage("Index");
        }


    }
}
