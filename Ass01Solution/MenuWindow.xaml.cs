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
        private IMemberRepository memberRepository = new MemberRepository();    
        private IProductRepository productRepository = new ProductRepository();
        private IOrderRepository orderRepository = new OrderRepository();
        public RoleUser UserRoleInfo { get; set; }
        private bool _isAdmin;
        public MenuWindow(bool isAdmin)
        {
            InitializeComponent();
            LoadProductList();
            LoadMemberList();
            LoadOrderList();
            _isAdmin = isAdmin;
        }

        private void LoadProductList()
        {
            lsvProduct.ItemsSource = productRepository.GetProducts();
        }

        private void LoadMemberList()
        {
            lsvMember.ItemsSource = memberRepository.GetMembers();
        }

        private void LoadOrderList() 
        {
            lsvOrder.ItemsSource = orderRepository.GetAll();
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
                MessageBox.Show("Select product to update");
            }
            return product;
        }

        /* Order */
        private Order GetOrderObject()
        {
            Order order = null;
            try
            {
                dynamic selectItem = lsvOrder.SelectedItem;
                order = new Order
                {
                    OrderId = selectItem.OrderId,
                    MemberId = selectItem.MemberId,
                    OrderDate = selectItem.OrderDate,
                    RequiredDate = selectItem.RequiredDate,
                    ShippedDate = selectItem.ShippedDate,
                    Freight = selectItem.Freight
                };
            }
            catch (RuntimeBinderException ex)
            {
                MessageBox.Show("Select order to update");
            }
            return order;
        }

        private Member GetMemberObject()
        {
            Member member = null;
            try
            {
                dynamic selectItem = lsvMember.SelectedItem;
                member = new Member
                {
                    MemberId = selectItem.MemberId,
                    Email = selectItem.Email,
                    CompanyName = selectItem.CompanyName,
                    City = selectItem.City,
                    Country = selectItem.Country,
                    Password = selectItem.Password,
                };
            }
            catch (RuntimeBinderException ex)
            {
                MessageBox.Show("Select order to update");
            }
            return member;
        }

       
        /* Member */
        private void btn_addMem_Click(object sender, RoutedEventArgs e)
        {
            MemberManagementPopUp memberManagermentPopUp = new MemberManagementPopUp
            {
                Title = "Add Product Popup",
                InsertOrUpdate = false,
                MemberRepository = memberRepository
            };
            if (memberManagermentPopUp.ShowDialog() == false)
            {
                LoadMemberList();
            }
        }

        private void btn_deleteMem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dynamic selectItem = lsvMember.SelectedItem;
                var memberId = selectItem.MemberId;

                memberRepository.DeleteMember(memberId);
                MessageBox.Show("Deleted Successfully!");
                LoadMemberList();

            }
            catch (RuntimeBinderException ex)
            {
                MessageBox.Show("Select Product to Delete");
            }
        }
        private void btnStatistic_Click(object sender, RoutedEventArgs e)
        {
            lsvOrder.ItemsSource = (System.Collections.IEnumerable)orderRepository.GetOrderByDate(txtPickupDate.DisplayDate, txtReturnDate.DisplayDate);
            MessageBox.Show($"The total amount of sales in the selected period is  currency", "Statistic Order ");
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            var products = productRepository.GetProducts();
            List<Product> list = new List<Product>();
            foreach (var product in products)
            {
                if (product.ProductName.ToUpper().Contains(txt_search.Text.ToUpper()))
                {
                    list.Add(product);
                }
                else if (int.TryParse(txt_search.Text, out int value))
                {
                    if (value == product.ProductId)
                    {
                        list.Add(product);
                    }
                    else if (value == product.UnitslnStock)
                    {
                        list.Add(product);
                    }
                }else if (decimal.TryParse(txt_search.Text, out decimal value2))
                {
                    if (value2 == product.UnitPrice) {
                        list.Add(product);
                    }
                }
            }
            if (list.Count > 0)
            {
                lsvProduct.ItemsSource = list;
            }
            else
            {
                MessageBox.Show("Product has not found");
                LoadProductList();
            }
        }

        private void btn_addOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderManagementPopUp orderManagermentPopUp = new OrderManagementPopUp
            {
                Title = "Add order Popup",
                InsertOrUpdate = false,
                OrderRepository = orderRepository,
            };
            if (orderManagermentPopUp.ShowDialog() == false)
            {
                LoadOrderList();
            }
        }

        private void btn_deleteOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dynamic selectItem = lsvOrder.SelectedItem;
                var orderId = selectItem.OrderId;

                orderRepository.Delete(orderId);
                MessageBox.Show("Deleted Successfully!");
                LoadOrderList();

            }
            catch (RuntimeBinderException ex)
            {
                MessageBox.Show("Select order to Delete");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Member member = GetMemberObject();
            if (member != null)
            {
                MemberManagementPopUp memberManagementPopUp = new MemberManagementPopUp
                {
                    Title = "Update Member",
                    InsertOrUpdate = true,
                    MemberRepository = memberRepository,
                    member = member
                };
                if (memberManagementPopUp.ShowDialog() == false)
                {
                    LoadMemberList();
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Order order = GetOrderObject();
            if (order != null)
            {
                OrderManagementPopUp orderManagementPopUp = new OrderManagementPopUp
                {
                    Title = "Update Order",
                    InsertOrUpdate = true,
                    OrderRepository = orderRepository,
                    order = order
                };
                if (orderManagementPopUp.ShowDialog() == false)
                {
                    LoadOrderList();
                }
            }
        }

        private void lsvOrder_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
 }

