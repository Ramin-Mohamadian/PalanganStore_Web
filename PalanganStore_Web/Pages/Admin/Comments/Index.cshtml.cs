using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Palangan.Core.Security;
using Palangan.Core.Services.Interfaces.Product;
using Palangan.DataLayer.Entities.Comments;
using System.Collections.Generic;

namespace PalanganStore_Web.Pages.Admin
{
    [PermissionChecker(1)]
    [PermissionChecker(21)]
    public class CommentModel : PageModel
    {
        private IProductService _productService;
        public CommentModel(IProductService productService)
        {
            _productService = productService;
        }


        public List<ProductComment> comment { get; set; }
        public void OnGet()
        {
            comment=_productService.GetAllComment();
        }
    }
}
