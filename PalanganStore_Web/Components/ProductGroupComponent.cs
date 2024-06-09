using Microsoft.AspNetCore.Mvc;
using Palangan.Core.Services.Interfaces.ProductGroup;
using System.Threading.Tasks;

namespace PalanganStore_Web.Components
{
    public class ProductGroupComponent:ViewComponent
    {
        
        private IGroupService _groupService;
        public ProductGroupComponent(IGroupService groupService)
        {
            _groupService=groupService;       
        }

        public async Task<IViewComponentResult>InvokeAsync()
        {

           // var group=_groupService.GetAllGroup();
 
            return await Task.FromResult((IViewComponentResult)View("ProductGroup", _groupService.GetAllGroup()));
        }
    }
}
