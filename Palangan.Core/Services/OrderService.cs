using Microsoft.EntityFrameworkCore;
using Palangan.Core.Dtos.Order;
using Palangan.Core.Services.Interfaces.Order;
using Palangan.DataLayer.Context;
using Palangan.DataLayer.Entities.Orders;
using Palangan.DataLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palangan.Core.Services
{
    public class OrderService : IOrderService
    {
        private MyContext _myContext;

        public Order Order { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public OrderService(MyContext myContext)
        {
            _myContext = myContext;
        }

        public int AddOrder(string name, int productId)
        {
            int userid=_myContext.Users.Where(u=>u.UserName == name).FirstOrDefault().UserId;
            var product=_myContext.Products.Find(productId);
            Order order = _myContext.Orders.FirstOrDefault(o=>o.UserId == userid&&!o.IsFinaly);

            if (order == null)
            {
                order = new Order()
                {
                    UserId = userid,
                    IsFinaly = false,
                    IsDelivered= false,
                    CreateDate = DateTime.Now,
                    Sum = product.Price,
                    OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail()
                        {
                            ProductId = productId,
                            Count = 1,
                            Price = product.Price
                        }
                    }
                };
                _myContext.Orders.Add(order);
                _myContext.SaveChanges();
            }
            else
            {
                OrderDetail detail = _myContext.OrderDetails
                    .FirstOrDefault(d => d.OrderId == order.OrderId && d.ProductId == productId);
                if (detail == null)
                {

                    detail=new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        Count = 1,
                        ProductId = productId,
                        Price = product.Price,
                    };
                    _myContext.OrderDetails.Add(detail);


                }
                else
                {

                    detail.Count += 1;
                    _myContext.OrderDetails.Update(detail);
                }

                _myContext.SaveChanges();
                UpdatePriceOrder(order.OrderId);
            }


            return order.OrderId;    
        }

        public List<Order> GetAllOrders(int? orderid )
        {
            return _myContext.Orders.Where(o => o.OrderId.Equals(orderid)||o.IsFinaly==true).OrderByDescending(c=>c.CreateDate).ToList();
        }       

        public void UpdatePriceOrder(int orderId)
        {
            var order = _myContext.Orders.Find(orderId);
            order.Sum=_myContext.OrderDetails.Where(o=>o.OrderId==orderId).Select(d=>d.Count*d.Price).Sum();
            _myContext.Orders.Update(order);
            _myContext.SaveChanges();
        }

        public string GetNameById(int id)
        {
            return _myContext.Users.Find(id).UserName;
        }

        public List<OrderDetail> GetOrderDetailByOrderId(int id)
        {
            return _myContext.OrderDetails.Where(od=>od.OrderId==id).ToList();
        }

        public string GetAdreesById(int id)
        {
            return _myContext.Users.Find(id).Address;
        }

        public Order GetOrderById(int id)
        {
            return _myContext.Orders.Find(id);
        }

        public void UpdateOrder(Order order)
        {
            _myContext.Orders.Update(order);
            _myContext.SaveChanges();
        }
    }
}
