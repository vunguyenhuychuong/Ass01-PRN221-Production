using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Ass01Solution
{
    /// <summary>
    /// Interaction logic for OrderManagementPopUp.xaml
    /// </summary>
    public partial class OrderManagementPopUp : Window
    {

        public IOrderRepository OrderRepository { get; set; }

        public bool InsertOrUpdate { get; set; }
        public Order order { get; set; }
        public OrderManagementPopUp()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtOrderId.IsEnabled = !InsertOrUpdate;
            if (InsertOrUpdate)
            {
                txtOrderId.Text = order.OrderId.ToString();
                txtMemberId.Text = order.MemberId.ToString();
                txtOrderDate.Text = order.OrderDate.ToString();
                txtRequiredDate.Text = order.RequiredDate.ToString();
                txtShippedDate.Text = order.ShippedDate.ToString();
                txtFreight.Text = order.Freight.ToString();
            }

        }

        private void btn_Order_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var order = new Order
                {
                    OrderId = int.Parse(txtOrderId.Text),
                    MemberId = int.Parse(txtMemberId.Text),
                    OrderDate = DateTime.Parse(txtOrderDate.Text),
                    RequiredDate = DateTime.Parse(txtRequiredDate.Text),
                    ShippedDate = DateTime.Parse(txtShippedDate.Text),
                    Freight = Decimal.Parse(txtFreight.Text),
                };
                if (InsertOrUpdate == false)
                {
                    OrderRepository.Add(order);
                    MessageBox.Show("Added Order Successfully!");
                    this.Close();
                }
                else
                {
                    OrderRepository.Update(order);
                    MessageBox.Show("Update Order Successfully!");
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, InsertOrUpdate == false ? "add new" : "update");
            }
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}

