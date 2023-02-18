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
    /// Interaction logic for ProductManagermentPopUp.xaml
    /// </summary>
    public partial class ProductManagermentPopUp : Window
    {

        public IProductRepository ProductRepository { get; set; }
       
        public bool InsertOrUpdate {get; set; }
        public Product product { get; set; }
        

        public ProductManagermentPopUp()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtProductId.IsEnabled = !InsertOrUpdate;
            if (InsertOrUpdate)
            {
                txtProductId.Text = product.ProductId.ToString();
                txtCategoryId.Text = product.CategoryId.ToString();
                txtProductName.Text = product.ProductName;
                txtWeight.Text = product.Weight;
                txtUnitPrice.Text = product.UnitPrice.ToString();
                txtUnitslnStock.Text = product.UnitslnStock.ToString();
            }

        }

        private void btn_Product_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var product = new Product
                {
                    ProductId = Int32.Parse(txtProductId.Text),
                    CategoryId = Int32.Parse(txtCategoryId.Text),
                    ProductName = txtProductName.Text,
                    Weight = txtWeight.Text,
                    UnitPrice = Decimal.Parse(txtUnitPrice.Text),
                    UnitslnStock = Int32.Parse(txtUnitslnStock.Text),
                };
                if (InsertOrUpdate == false)
                {
                    ProductRepository.InsertProduct(product);
                    MessageBox.Show("Added Product Successfully!");
                    this.Close();
                }
                else
                {
                    ProductRepository.UpdateProduct(product);
                    MessageBox.Show("Update Product Successfully!");
                    this.Close();
                }

            }catch(Exception ex)
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
