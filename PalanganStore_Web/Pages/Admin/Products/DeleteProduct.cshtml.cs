using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Palangan.Core.Security;
using Palangan.Core.Services.Interfaces.Product;
using Palangan.DataLayer.Entities.Products;

namespace PalanganStore_Web.Pages.Admin.Products
{
    [PermissionChecker(1)]
    [PermissionChecker(10)]
    [PermissionChecker(13)]
    public class DeleteProductModel : PageModel
    {
        private IProductService _productService;
        public DeleteProductModel(IProductService productService)
        {
            _productService=productService;   
        }
        [BindProperty]
        public Product  product { get; set; }
        public void OnGet(int id)
        {
            product=_productService.GetProductlById(id);
        }

        public IActionResult OnPost()
        {
            _productService.DeleteProduct(product.ProductId);

            return RedirectToPage("Index");
        }
    }
}
