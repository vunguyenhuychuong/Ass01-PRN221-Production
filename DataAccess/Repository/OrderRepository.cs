using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public void Add(Order order)
        {
            OrderDAO.Instance.AddOrder(order);
        }

        public void Delete(int orderId)
        {
            OrderDAO.Instance.DeleteOrder(orderId);
        }

        public IEnumerable<Order> GetAll()
        {
            return OrderDAO.Instance.GetOrders();
        }

        public Order GetById(int id)
        {
            return OrderDAO.Instance.GetOrderById(id);
        }

        public void Update(Order order)
        {
            OrderDAO.Instance.UpdateOrder(order);
        }
    }
}
