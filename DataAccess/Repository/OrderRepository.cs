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

        public List<Order> GetOrderByDate(DateTime dateTime1, DateTime dateTime2)
        {
            return OrderDAO.Instance.Filter(dateTime1, dateTime2);
        }

        public Order GetOrderByMemberId(int memberId)
        {
            return OrderDAO.Instance.GetOrderByMemberId(memberId);
        }

        

        public void Update(Order order)
        {
            OrderDAO.Instance.UpdateOrder(order);
        }

        public IEnumerable<Order> ViewOrderHistory(int memberId)
        {
            return OrderDAO.Instance.ViewOrderHistory(memberId);
        }
    }
}
