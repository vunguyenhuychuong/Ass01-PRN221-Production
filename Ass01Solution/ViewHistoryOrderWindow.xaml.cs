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
    /// Interaction logic for ViewHistoryOrderWindow.xaml
    /// </summary>
    public partial class ViewHistoryOrderWindow : Window
    {
        private int _memberId;
        IOrderRepository _orderRepository = new OrderRepository();

        public ViewHistoryOrderWindow(int memberId)
        {   
            _memberId = memberId;
            InitializeComponent();
            LoadOrderHistory();
        }

        public void LoadOrderHistory()
        {
            lsvViewRentingHistory.ItemsSource = _orderRepository.ViewOrderHistory(_memberId).ToList();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lsvView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
