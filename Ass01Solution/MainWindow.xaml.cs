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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ass01Solution
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IMemberRepository memberRepository = new MemberRepository();
        private IOrderRepository orderRepository = new OrderRepository();
        public RoleUser UserRoleInfo { get; set; }
        private bool _isAddOrUpdate;
        private int _memberId;

        public MainWindow(int memberId)
        {
            _isAddOrUpdate = false;
            InitializeComponent();
            _memberId = memberId;
            CheckAddOrUpdate();
        }


        private Member GetMemberInfor()
        {
            Member info = new Member
            {

                MemberId = int.Parse(txtMemberId.Text),
                Email = txtEmail.Text,
                CompanyName = txtCompanyName.Text,
                City = txtCity.Text,
                Country = txtCountry.Text,
                Password = txtPassword.Text
            };
            return info;
        }

        //Chi update thong tin nen cho false th add de ko bao gio nhay vao th add.
        private void CheckAddOrUpdate()
        {
            txtMemberId.IsEnabled = _isAddOrUpdate;
            if (_isAddOrUpdate)
            {
                lbAdd_Update.Content = "Add";
            }
            else
            {
                var info = memberRepository.GetMemberById(_memberId);
                lbAdd_Update.Content = "Update";
                txtMemberId.IsEnabled = false;
                txtMemberId.Text = info.MemberId.ToString();
                txtEmail.Text = info.Email;
                txtCompanyName.Text = info.CompanyName;
                txtCity.Text = info.City;
                txtPassword.Text = info.Password;
                txtCountry.Text = info.Country;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            LoginWindow loginWindow = new LoginWindow();    
            loginWindow.Show();
        }

        private void btn_Member_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_isAddOrUpdate)
                {
                    memberRepository.InsertMember(GetMemberInfor());
                    MessageBox.Show("Add success");
                }
                else
                {
                    memberRepository.UpdateMember(GetMemberInfor());
                    MessageBox.Show("Update success");
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_View_History(object sender, RoutedEventArgs e)
        {
            ViewHistoryOrderWindow viewHistoryOrderWindow = new ViewHistoryOrderWindow(_memberId);
            viewHistoryOrderWindow.Show();
        }
    }
}
