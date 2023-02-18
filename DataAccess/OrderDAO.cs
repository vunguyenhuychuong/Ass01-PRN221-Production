using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDAO
    {
        FStoreDBAssignmentContext dBContext = new FStoreDBAssignmentContext();
        List<Order> orders = new List<Order>();

        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();

        private OrderDAO() { }

        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }

        public Order GetOrderById(int orderId)
        {
            Order order = null;
            try
            {
                order = dBContext.Orders.SingleOrDefault(p => p.OrderId == orderId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }

        public IEnumerable<Order> GetOrders()
        {
            var orders = new List<Order>();
            try
            {
                orders = dBContext.Orders.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orders;
        }

        public void AddOrder(Order newOrder)
        {
            try
            {
                Order order = GetOrderById(newOrder.OrderId);
                if (order == null)
                {
                    dBContext.Orders.Add(newOrder);
                    dBContext.SaveChanges();
                }
                else
                {
                    throw new Exception("The order is allready exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteOrder(int orderId)
        {
            try
            {
                Order order = GetOrderById(orderId);
                if (order != null)
                {
                    dBContext.Orders.Remove(order);
                    dBContext.SaveChanges();
                }
                else
                {
                    throw new Exception("the order is not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateOrder(Order order)
        {
            try
            {
                Order orderToUpdate = GetOrderById(order.OrderId);
                if (orderToUpdate != null)
                {
                    bool check = true;
                    if (order.OrderDate == null)
                    {
                        check = false;
                        throw new Exception("The order date is not null.");
                    }
                    if (check)
                    {
                        dBContext.Orders.Update(order);
                        dBContext.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("The order is not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Order GetOrderByMemberId(int memberId)
        {
            FStoreDBAssignmentContext dBContext = new FStoreDBAssignmentContext();
            Order? order = dBContext.Orders.Where(order => order.MemberId == memberId).FirstOrDefault();
            return order;
        }

        public IEnumerable<Order> ViewOrderHistory(int memberId)
        {
            List<Order> listOrder;
            try
            {
                var context = new FStoreDBAssignmentContext();
                listOrder = context.Orders.Where(c => c.MemberId == memberId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listOrder;
        }

        public List<Order> Filter(DateTime a, DateTime b)
        {
            var order = new List<Order>();
            var fil = new List<Order>();
            try
            {
                using var context = new FStoreDBAssignmentContext();
                order = context.Orders.ToList();
                for (int i = 0; i < order.Count(); i++)
                {
                    if ((order[i].RequiredDate >= a && order[i].ShippedDate <= a))
                    {
                        fil.Add(order[i]);
                    }
                }
                fil = fil.OrderByDescending(x => x.RequiredDate).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return fil;
        }
    }
}
