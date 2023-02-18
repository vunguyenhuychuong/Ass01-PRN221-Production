using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        void Add(Order order);
        void Update(Order order);
        void Delete(int orderId);
        Order GetById(int id);

        Order GetOrderByMemberId(int memberId);

        IEnumerable<Order> ViewOrderHistory(int memberId);

        List<Order> GetOrderByDate(DateTime dateTime1, DateTime dateTime2);
    }
}
