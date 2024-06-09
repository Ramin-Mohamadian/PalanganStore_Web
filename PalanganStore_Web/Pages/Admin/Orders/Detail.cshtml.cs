using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Palangan.Core.Security;
using Palangan.Core.Services.Interfaces.Order;
using Palangan.DataLayer.Entities.Orders;
using System.Collections.Generic;

namespace PalanganStore_Web.Pages.Admin.Orders
{
    [PermissionChecker(1)]
    [PermissionChecker(19)]
    public class DetailModel : PageModel
    {
        private IOrderService _orderService;

        public DetailModel(IOrderService orderService)
        {
            _orderService = orderService;
        }


        public List<OrderDetail> detail { get; set; }
        public void OnGet(int id)
        {
            detail=_orderService.GetOrderDetailByOrderId(id);
        }


    }
}
