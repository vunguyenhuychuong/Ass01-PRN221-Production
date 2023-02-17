using BusinessObject;
using DataAccess.Repository;
using Microsoft.CSharp.RuntimeBinder;
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
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {

        private IProductRepository productRepository = new ProductRepository();
        public RoleUser UserRoleInfo { get; set; }
        private bool _isAdmin;
        public MenuWindow(bool isAdmin)
        {
            InitializeComponent();
            LoadProductList();
            _isAdmin = isAdmin;
        }

        private void LoadProductList()
        {
            lsvProduct.ItemsSource = productRepository.GetProducts();
        }

        private void btn_addPro_Click(object sender, RoutedEventArgs e)
        {
            ProductManagermentPopUp productManagermentPopUp = new ProductManagermentPopUp
            {
                Title = "Add Product Popup",
                InsertOrUpdate = false,
                ProductRepository= productRepository,
            };
            if (productManagermentPopUp.ShowDialog() == false )
            {
                LoadProductList();
            }
        }

        private void btn_deletePro_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dynamic selectItem = lsvProduct.SelectedItem;
                var productId = selectItem.ProductId;
                
                    productRepository.DeleteProduct(productId);
                    MessageBox.Show("Deleted Successfully!");
                    LoadProductList();
                
            }
            catch(RuntimeBinderException ex)
            {
                MessageBox.Show("Select Product to Delete");
            }
            
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {        
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private Product GetProductObject()
        {
            Product product = null;
            try
            {
                dynamic selectItem = lsvProduct.SelectedItem;
                product = new Product
                {
                    ProductId = selectItem.ProductId,
                    CategoryId= selectItem.CategoryId,
                    ProductName= selectItem.ProductName,
                    Weight= selectItem.Weight,
                    UnitPrice= selectItem.UnitPrice,
                    UnitslnStock= selectItem.UnitslnStock
                };
            }catch(RuntimeBinderException ex)
            {
                MessageBox.Show("double click product to update");
            }
            return product;
        }

        private void lsvProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Product product = GetProductObject();
            if (product != null)
            {
                ProductManagermentPopUp productManagermentPopUp = new ProductManagermentPopUp
                {
                    Title = "UpdateProduct PopUp",
                    InsertOrUpdate = true,
                    ProductRepository = productRepository,
                    product = product
                };

                if (productManagermentPopUp.ShowDialog() == false)
                {
                    LoadProductList();
                }
            }         
        }
    }
}
