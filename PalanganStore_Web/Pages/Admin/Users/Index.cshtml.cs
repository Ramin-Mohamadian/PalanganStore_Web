using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Palangan.Core.Dtos.AdminUser;
using Palangan.Core.Security;
using Palangan.Core.Services.Interfaces;
using System.Collections.Generic;

namespace PalanganStore_Web.Pages.Admin.Users
{
    [PermissionChecker(2)]
    public class IndexModel : PageModel
    {


        private IAccountService _accountService;
        public IndexModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        
        public List<AdminUserListViewModel> ListUser { get; set; }
        public void OnGet(string name)
        {
            ListUser=_accountService.GetAllUserForAdminList(name);
            ViewData["userCount"]=_accountService.UserCont();
        }
    }
}
