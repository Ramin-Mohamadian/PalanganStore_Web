using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Palangan.Core.Security;
using Palangan.Core.Services.Interfaces.ProductGroup;

namespace PalanganStore_Web.Pages.Admin.ProductGroup
{
    [PermissionChecker(1)]
    [PermissionChecker(14)]
    [PermissionChecker(17)]
    public class DeletGroupModel : PageModel
    {
        private IGroupService _groupService;
        public DeletGroupModel(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [BindProperty]
        public Palangan.DataLayer.Entities.Groups.ProductGroup group { get; set; }
        public void OnGet(int id)
        {
            group =_groupService.GetGroupById(id);
        }

        public IActionResult OnPost()
        {
            

            _groupService.DeleteGroup(group.GroupId);

            return RedirectToPage("Index");
        }
    }
}
