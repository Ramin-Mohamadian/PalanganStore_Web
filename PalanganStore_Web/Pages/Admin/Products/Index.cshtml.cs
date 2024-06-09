using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Palangan.Core.Dtos.Product;
using Palangan.Core.Security;
using Palangan.Core.Services.Interfaces.Product;
using System.Collections.Generic;

namespace PalanganStore_Web.Pages.Admin.Products
{
    [PermissionChecker(1)]
    [PermissionChecker(10)]


    public class IndexModel : PageModel
    {
        private IProductService _productService;
        public IndexModel(IProductService productService)
        {
                _productService = productService;
        }


        public List<ShowProductForAdminViewModel> ListProduct { get; set; }
        public void OnGet()
        {
            ListProduct=_productService.GetAllProductAdminList();
        }
    }
}
