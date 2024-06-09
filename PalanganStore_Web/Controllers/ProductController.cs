using Microsoft.AspNetCore.Mvc;
using Palangan.Core.Dtos.Product;
using Palangan.Core.Services.Interfaces.Product;
using Palangan.DataLayer.Entities.Comments;
using System;

namespace PalanganStore_Web.Controllers
{
    public class ProductController : Controller
    {

        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        public IActionResult ShowProduct(int id)
        { 
            var product=_productService.GetProductlById(id);
            product.See +=1;
          _productService.UpdateProduct(product);
            return View(product);
        }


        [HttpPost]
        public IActionResult AddComment(ProductComment comment) 
        {
            comment.CreateDate=DateTime.Now;
            comment.IsAdminRead=false;
            comment.IsDelete=false;
            comment.UserId=_productService.GetUserIdByName(User.Identity.Name);
            _productService.AddComment(comment);
            return View("ShowComment", _productService.GetAllCommentsById(comment.ProductId));
        }


        public IActionResult ShowComment(int id)
        {
            return PartialView(_productService.GetAllCommentsById(id));
        }
    }
}
