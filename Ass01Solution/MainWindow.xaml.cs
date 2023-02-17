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
        public RoleUser UserRoleInfo { get; set; }
        private bool _isAdmin;
        public MainWindow(bool isAdmin)
        {
            InitializeComponent();
            LoadMemberList();
            _isAdmin = isAdmin;
        }


        public void LoadMemberList()
        {
            lsvMem.ItemsSource = memberRepository.GetMembers();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            MemberManagementPopUp carManagementPopup = new MemberManagementPopUp(this);
            carManagementPopup.Show();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtSelectedId.Text))
            {
                MemberManagementPopUp carManagementPopUp = new MemberManagementPopUp(int.Parse(txtSelectedId.Text), this);
                carManagementPopUp.Show();
            }
            else
            {
                MessageBox.Show("Please select a member");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtSelectedId.Text))
            {
                MessageBox.Show("Please select a car you want to delete");
            }
            else
            {
                MessageBoxResult dialogResult = MessageBox.Show("This may cause delete carProducer ,Are you want to countinue?", "This action can'be reverse", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    memberRepository.DeleteMember(int.Parse(txtSelectedId.Text));
                    LoadMemberList();
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            LoginWindow loginWindow = new LoginWindow();    
            loginWindow.Show();
        }
    }
}
