using Microsoft.AspNetCore.Mvc;
using Palangan.Core.Services.Interfaces.Product;
using System.Threading.Tasks;

namespace PalanganStore_Web.Components
{
    public class ShowProductByVisitedComponent : ViewComponent
    {
        private IProductService _productService;
        public ShowProductByVisitedComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("ShowProductByVisit",_productService.GetAllProductByVisit()));
        }

    }
}
