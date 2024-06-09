using Microsoft.AspNetCore.Mvc;
using Palangan.Core.Dtos.Order;
using Palangan.Core.Services.Interfaces.Order;
using Palangan.Core.Services.Interfaces.Product;
using Palangan.DataLayer.Context;
using Palangan.DataLayer.Entities.Orders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalanganStore_Web.Components
{
    public class ShowCartComponent : ViewComponent
    {
        private MyContext _context;
       
        public ShowCartComponent(MyContext context)
        {
            _context = context;
           
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            List<ShowOrderViewModel> list = new List<ShowOrderViewModel>();
            if (User.Identity.IsAuthenticated)
            {
                string name=User.Identity.Name;
                int userId = _context.Users.Where(u=>u.UserName==name).SingleOrDefault().UserId;

                var order = _context.Orders.SingleOrDefault(o => o.UserId == userId&&!o.IsFinaly);
                if (order!=null)
                {
                   var details=_context.OrderDetails.Where(od=>od.OrderId==order.OrderId).ToList();
                    foreach (var item in details)
                    {
                        var product = _context.Products.Find(item.ProductId);
                        list.Add(new ShowOrderViewModel()
                        {
                            OrderDetailId=item.OrderDetailId,
                            Count=item.Count,
                            ImageName=product.ProductImage,
                            Price=item.Price,
                            Title=product.Title,
                            SumOrder=order.Sum
                        });
                    }
                }
            }

            return await Task.FromResult((IViewComponentResult)View("ShowCard",list));
        }
    }
}
