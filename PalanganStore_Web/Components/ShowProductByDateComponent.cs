using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Palangan.Core.Services.Interfaces.Product;
using System.Threading.Tasks;

namespace PalanganStore_Web.Components
{
    public class ShowProductByDateComponent:ViewComponent
    {
        private IProductService _productService;
        public ShowProductByDateComponent(IProductService productService)
        {
                _productService = productService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("ShowProductByDate",_productService.GetAllProductByDate()));
        }

    }
}
