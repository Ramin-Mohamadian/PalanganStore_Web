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
    public class IndexModel : PageModel
    {
        private IRoleService _roleService;
        public IndexModel(IRoleService roleService)
        {
            _roleService=roleService;  
        }


        public List<Role> Roles { get; set; }
        public void OnGet()
        {

            Roles = _roleService.GetAllRole();
        }
    }
}
