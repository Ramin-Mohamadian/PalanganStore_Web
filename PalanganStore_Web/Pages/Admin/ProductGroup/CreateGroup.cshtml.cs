using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Palangan.Core.Security;
using Palangan.Core.Services.Interfaces.ProductGroup;
using Palangan.DataLayer.Entities.Groups;

namespace PalanganStore_Web.Pages.Admin.ProductGroup
{
    [PermissionChecker(1)]
    [PermissionChecker(14)]
    [PermissionChecker(15)]
    
    public class CreateGroupModel : PageModel
    {


        private IGroupService _groupService;
        public CreateGroupModel(IGroupService groupService)
        {
                _groupService = groupService;
        }



        [BindProperty]
        public Palangan.DataLayer.Entities.Groups.ProductGroup ProductGroups { get; set; }

        public void OnGet(int? id)
        {
            ProductGroups=new Palangan.DataLayer.Entities.Groups.ProductGroup()
            {
                ParentId=id
            };
        }


        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }


            _groupService.AddGroup(ProductGroups);

            return RedirectToPage("index");
        }
    }
}
