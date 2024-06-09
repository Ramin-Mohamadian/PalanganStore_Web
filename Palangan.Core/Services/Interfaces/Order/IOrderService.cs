using Palangan.Core.Dtos.Order;
using Palangan.DataLayer.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palangan.Core.Services.Interfaces.Order
{
    public interface IOrderService
    {

        int AddOrder(string name, int productId);
        
        void UpdatePriceOrder(int orderId);

        List<Palangan.DataLayer.Entities.Orders.Order> GetAllOrders(int? orderid);

        string GetNameById(int id);

        List<OrderDetail> GetOrderDetailByOrderId(int id);
        string GetAdreesById(int id);

        Palangan.DataLayer.Entities.Orders.Order GetOrderById(int id);

        void UpdateOrder(Palangan.DataLayer.Entities.Orders.Order order);
    }
}
