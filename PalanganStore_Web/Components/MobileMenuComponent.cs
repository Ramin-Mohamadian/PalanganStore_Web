using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Palangan.Core.Services.Interfaces.ProductGroup;
using System.Threading.Tasks;

namespace PalanganStore_Web.Components
{
    public class MobileMenuComponent:ViewComponent
    {
        private IGroupService _groupService;
        public MobileMenuComponent(IGroupService groupService)
        {
            _groupService = groupService;
        }

        public async Task<IViewComponentResult>InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("MobileMenu", _groupService.GetAllGroup()));
        }
    }
}
