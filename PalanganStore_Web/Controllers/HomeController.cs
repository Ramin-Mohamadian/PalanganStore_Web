using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Palangan.Core.Services.Interfaces.Product;

namespace PalanganStore_Web.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;
        public HomeController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetSubGroups(int id)
        {
            var subgroup=_productService.GetSubGroupForAddProduct(id);
            return Json(new SelectList(subgroup, "Value", "Text"));
        }




        
        public IActionResult Arshive(string filter = "", int group = 0, string getType = "all", int startPrice = 0, int endPrice = 0)
        {        

            
            




            return View(_productService.GetProductByFilter(filter,group,getType,startPrice,endPrice));

        }
    }
}
