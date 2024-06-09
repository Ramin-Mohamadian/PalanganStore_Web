using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Palangan.Core.Security;
using Palangan.Core.Services.Interfaces.Order;
using Palangan.DataLayer.Entities.Orders;

namespace PalanganStore_Web.Pages.Admin.Orders
{
    [PermissionChecker(1)]
    [PermissionChecker(20)]
    public class SubmitDeliveredModel : PageModel
    {
        private IOrderService _orderService;
        public SubmitDeliveredModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [BindProperty]
        public Order  order { get; set; }
        public void OnGet(int id)
        {
            order=_orderService.GetOrderById(id);
        }


        public IActionResult OnPost()
        {
            var subdel=_orderService.GetOrderById(order.OrderId);
            subdel.IsDelivered= true;
            _orderService.UpdateOrder(subdel);


            return RedirectToPage("Index");
        }
    }
}
