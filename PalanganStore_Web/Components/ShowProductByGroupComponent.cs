using Microsoft.AspNetCore.Mvc;
using Palangan.Core.Services.Interfaces.Product;
using System.Threading.Tasks;

namespace PalanganStore_Web.Components
{
    public class ShowProductByGroupComponent:ViewComponent
    {
        private IProductService _productService;
        public ShowProductByGroupComponent(IProductService productService)
        {
                _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("ShowProductByGroup",_productService.GetAllProductByGroup()));
        }
    }
}
