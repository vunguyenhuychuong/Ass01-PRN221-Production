using BusinessObject;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private IMemberRepository memberRepository;
       

        public LoginWindow()
        {
            InitializeComponent();
        }

        private RoleUser LoginByAdminAccount(String email, String password)
        {
            String _email, _password;
            RoleUser roleUser = null;

            using (StreamReader r = new StreamReader("appsettings.json"))
            {
                string json = r.ReadToEnd();
                IConfiguration config = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", true, true)
                                        .Build();
                _email = config["account:defaultAccount:email"];
                _password = config["account:defaultAccount:password"];
            }

            if (email.Equals(_email) && password.Equals(_password))
            {
                roleUser = new RoleUser
                {
                    Email = email,
                    Password = password,
                };
                return roleUser;
            }
            else
            {
                return null;
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            memberRepository = new MemberRepository();
            MainWindow mainWindow;
            String email = txtUsername.Text;
            String password = txtPasword.Password;
            if (LoginByAdminAccount(email, password) != null)
            {
                mainWindow = new MainWindow(true);
                mainWindow.UserRoleInfo = LoginByAdminAccount(email, password);
                mainWindow.Show();
                this.Hide();
            }
            /*else if (memberRepository.LoginByStaffAccount(email, password) != null)
            {
                var staff = staffRepository.GetStaffByEmail(txtUsername.Text);
                MenuStaffManagement staffInformationView = new MenuStaffManagement(staff.StaffId);
                staffInformationView.Show();
                this.Hide();
            }*/
        }
    }
}
