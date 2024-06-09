using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Palangan.Core.Dtos.Order;
using Palangan.Core.Services.Interfaces.Order;
using Palangan.DataLayer.Context;
using Palangan.DataLayer.Entities.Orders;
using System.Collections.Generic;
using System.Linq;
using Zarinpal;

namespace PalanganStore_Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private IOrderService _orderService;
        private MyContext _context;

        public OrderController(IOrderService orderService, MyContext context)
        {
            _orderService = orderService;
            _context=context;
        }
        public IActionResult AddToCart(int id)
        {
            _orderService.AddOrder(User.Identity.Name, id);
            return Redirect("/Product/ShowProduct/"+id);
        }



        public IActionResult ShowOrder()
        {
            string name = User.Identity.Name;
            int userId = _context.Users.Where(u => u.UserName==name).SingleOrDefault().UserId;

            var order = _context.Orders.SingleOrDefault(o => o.UserId == userId&&!o.IsFinaly);

            List<ShowOrderDetailViewModel>_list= new List<ShowOrderDetailViewModel>();

            if (order!=null)
            {
                var details = _context.OrderDetails.Where(od => od.OrderId==order.OrderId).ToList();
                foreach (var item in details)
                {
                    var product = _context.Products.Find(item.ProductId);
                    _list.Add(new ShowOrderDetailViewModel()
                    {
                        OrderDetailId= item.OrderDetailId,
                        Count=item.Count,
                        ImageName=product.ProductImage,
                        Price=item.Price,
                        SumOrder=item.Count*item.Price,
                        Title=product.Title,
                    });
                }
            }
            return View(_list);
        }


        public IActionResult DeleteOrder(int id)
        {
            
            var orderDetail=_context.OrderDetails.Find(id);            
            _context.Remove(orderDetail);
           
            _context.SaveChanges();
            var order = _context.Orders.First(o => o.OrderId==orderDetail.OrderId);
            _orderService.UpdatePriceOrder(order.OrderId);
            return RedirectToAction("ShowOrder");
        }

        public IActionResult AddCount(int id)
        {
            var orderDetail = _context.OrderDetails.Find(id);           
            orderDetail.Count+=1;
            _context.OrderDetails.Update(orderDetail);           
            _context.SaveChanges();


            var order = _context.Orders.FirstOrDefault(o => o.OrderId==orderDetail.OrderId);
            _orderService.UpdatePriceOrder(order.OrderId);
            return RedirectToAction("ShowOrder");
        }

        public IActionResult RemoveCount(int id)
        {
            var orderDetail = _context.OrderDetails.Find(id);
            if (orderDetail.Count>1)
            {
                orderDetail.Count-=1;
                _context.Update(orderDetail);
               
               
                _context.SaveChanges();
            }
            var order = _context.Orders.SingleOrDefault(o => o.OrderId==orderDetail.OrderId);
            _orderService.UpdatePriceOrder(order.OrderId);
            return RedirectToAction("ShowOrder");
        }




        public IActionResult Payment()
        {
            int userid = _context.Users.SingleOrDefault(u => u.UserName==User.Identity.Name).UserId;
            var order = _context.Orders.SingleOrDefault(o =>o.UserId==userid && !o.IsFinaly);
            if (order == null)
            { 
                return NotFound();
            }

            var payment = new Payment("5d799b8a-0b78-45fc-9e67-c5b7e911f6c3", order.Sum);
            var res = payment.PaymentRequest($"پرداخت فاکتور شماره {order.OrderId}", "http://palanganstore.ir/Order/OnlinePayment/"+order.OrderId, "raminmohamadian2020@gmail.com", "09186671161");
            if(res.Result.Status==100)
            {
                return Redirect("https://www.zarinpal.com/pg/StartPay/"+res.Result.Authority);
            }
            else
            {
                return BadRequest();
            }
            return null;
        }



        public IActionResult OnlinePayment(int id)
        {

            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                && HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];

                var order = _context.Orders.Find(id);

                var payment = new Payment("5d799b8a-0b78-45fc-9e67-c5b7e911f6c3", order.Sum);
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    ViewBag.code = res.RefId;
                    ViewBag.IsSuccess = true;
                    order.IsFinaly = true;
                    _context.Orders.Update(order);
                    _context.SaveChanges();
                }
              
            }
            return View("~/Views/Order/OnlinePayment.cshtml");
        }
    }
}
