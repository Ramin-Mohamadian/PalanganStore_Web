using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Palangan.Core.Security;
using Palangan.Core.Services.Interfaces.Product;
using Palangan.DataLayer.Entities.Comments;

namespace PalanganStore_Web.Pages.Admin.Comments
{
    [PermissionChecker(1)]
    [PermissionChecker(23)]
    public class ReadCommentModel : PageModel
    {
        private IProductService _productService;
        public ReadCommentModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public ProductComment comment { get; set; }
        public void OnGet(int id)
        {
            comment=_productService.GetCommentById(id);
        }

        public IActionResult OnPost()
        {

            var com = _productService.GetCommentById(comment.CommentId);
            com.IsAdminRead= true;
            _productService.UpdateComment(com);


            return RedirectToPage("Index");
        }
    }
}
