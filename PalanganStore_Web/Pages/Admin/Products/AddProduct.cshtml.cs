using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Palangan.Core.Security;
using Palangan.Core.Services.Interfaces.Product;
using Palangan.DataLayer.Entities.Products;
using System.Collections.Generic;
using System.Linq;

namespace PalanganStore_Web.Pages.Admin.Products
{
    [PermissionChecker(1)]
    [PermissionChecker(10)]
    [PermissionChecker(11)]
    public class AddProductModel : PageModel
    {
        private IProductService _productService;
        public AddProductModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Product product { get; set; }

        public void OnGet()
        {


            var group = _productService.GetGroupForAddProduct();
            ViewData["Group"]=new SelectList(group, "Value", "Text");

            List<SelectListItem> subgroup = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text="انتخاب کنید!!!",
                    Value="",
                }
            };

            subgroup.AddRange(_productService.GetSubGroupForAddProduct(int.Parse(group.First().Value)));
            ViewData["SubGroup"]=new SelectList(subgroup, "Value", "Text");

        }


        public IActionResult OnPost(IFormFile imgUp)
        {
            

             _productService.AddProduct(product, imgUp);

            return RedirectToPage("Index");
        }
    }
}
