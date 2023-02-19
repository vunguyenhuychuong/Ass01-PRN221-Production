using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
    /// Interaction logic for MemberManagementPopUp.xaml
    /// </summary>
    public partial class MemberManagementPopUp : Window
    {
        public IMemberRepository MemberRepository { get; set; }

        public bool InsertOrUpdate { get; set; }
        public Member member { get; set; }


        public MemberManagementPopUp()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtMemberId.IsEnabled = !InsertOrUpdate;
            if (InsertOrUpdate)
            {
                txtMemberId.Text = member.MemberId.ToString();
                txtEmail.Text = member.Email;
                txtCompanyName.Text = member.CompanyName;
                txtCity.Text = member.City;
                txtCountry.Text = member.Country;
                txtPassword.Text = member.Password;
            }

        }

        private void btn_Member_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var member = new Member
                {
                    MemberId = int.Parse(txtMemberId.Text),
                    Email = txtEmail.Text,
                    CompanyName = txtCompanyName.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text,
                    Password = txtPassword.Text,
                };
                if (InsertOrUpdate == false)
                {
                    MemberRepository.InsertMember(member);
                    MessageBox.Show("Added Member Successfully!");
                    this.Close();
                }
                else
                {
                    MemberRepository.UpdateMember(member);
                    MessageBox.Show("Update Member Successfully!");
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
