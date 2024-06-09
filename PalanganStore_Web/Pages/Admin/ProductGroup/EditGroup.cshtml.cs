using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Palangan.Core.Security;
using Palangan.Core.Services.Interfaces.ProductGroup;
using Palangan.DataLayer.Entities.Groups;

namespace PalanganStore_Web.Pages.Admin.ProductGroup
{
    [PermissionChecker(1)]
    [PermissionChecker(14)]
    [PermissionChecker(16)]
    public class EditGroupModel : PageModel
    {
        private IGroupService _groupService;
        public EditGroupModel(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [BindProperty]
        public Palangan.DataLayer.Entities.Groups.ProductGroup ProductGroups { get; set; }
        public void OnGet(int id)
        {
            ProductGroups = new Palangan.DataLayer.Entities.Groups.ProductGroup()
            {
                ParentId=id,
            };
            ProductGroups=_groupService.GetGroupById(id);
        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            _groupService.UpdateGroup(ProductGroups);
            return RedirectToPage("Index");
        }
    }
}
