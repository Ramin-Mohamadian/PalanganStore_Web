using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Palangan.Core.Security;
using Palangan.Core.Services.Interfaces.Order;
using Palangan.DataLayer.Entities.Orders;
using System.Collections.Generic;

namespace PalanganStore_Web.Pages.Admin.Orders
{
    [PermissionChecker(1)]
    [PermissionChecker(18)]
    
    public class IndexModel : PageModel
    {
        private IOrderService _orderService;
        public IndexModel(IOrderService orderService)
        {
            _orderService=orderService; 
        }



        [BindProperty]
        public List<Order> Orders { get; set; }
        public void OnGet(int? orderid)
        {
            Orders=_orderService.GetAllOrders(orderid);
        }
    }
}
