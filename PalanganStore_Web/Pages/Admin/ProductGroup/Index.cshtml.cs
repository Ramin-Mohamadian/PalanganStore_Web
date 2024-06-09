using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Palangan.Core.Security;
using Palangan.Core.Services.Interfaces.ProductGroup;
using Palangan.DataLayer.Entities.Groups;
using System.Collections.Generic;

namespace PalanganStore_Web.Pages.Admin.ProductGroup
{
    [PermissionChecker(1)]
    [PermissionChecker(14)]
  
    public class IndexModel : PageModel
    {
        private IGroupService _groupService;
        public IndexModel(IGroupService groupService)
        {
            _groupService=groupService;    
        }



        
        public List<Palangan.DataLayer.Entities.Groups.ProductGroup> allproduct { get; set; }
        public void OnGet()
        {
            allproduct=_groupService.GetAllGroup();
        }

    }
}
