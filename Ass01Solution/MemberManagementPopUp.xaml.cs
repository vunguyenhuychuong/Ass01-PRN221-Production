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
        private int _memberId;
        MainWindow _memberRepository;
        IMemberRepository memberRepository = new MemberRepository();
        private bool _isAddOrUpdate;

        public bool checkVaLidateCarname()
        {
            String Email = txtEmail.Text;
            String Company = txtCompanyName.Text;
            String City = txtCity.Text;
            String Country = txtCountry.Text;
            if (Email == null && Company == null && City == null && Country == null)
            {
                MessageBox.Show("FIELD KHONG DC DE TRONG");
                return false;
            }
            else
            {
                return true;
            }
        }

        public MemberManagementPopUp(MainWindow memberRepository)
        {
            _isAddOrUpdate = true;
            _memberRepository = memberRepository;
            InitializeComponent();
            checkVaLidateCarname();
            lbAdd_Update.Content = "Add";
            btnAdd_Update.Content = "Add";
        }

        public MemberManagementPopUp(int memberId, MainWindow memberRepository)
        {
            _isAddOrUpdate = false;
            _memberRepository = memberRepository;
            InitializeComponent();
            checkVaLidateCarname();
            SetUpdateForm(memberId);
        }

        private void SetUpdateForm(int memberId)
        {
            try
            {
                lbAdd_Update.Content = "Update";
                btnAdd_Update.Content = "Update";
                var mem = memberRepository.GetMemberById(memberId);
                txtMemberId.Text = mem.MemberId.ToString();
                txtEmail.Text = mem.Email;
                txtCompanyName.Text = mem.CompanyName;
                txtCity.Text = mem.City;
                txtCountry.Text = mem.Country;
                txtPassword.Text = mem.Password;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }


        private void btnAdd_Update_Click(object sender, RoutedEventArgs e)
        {
            int memberId = int.Parse(txtMemberId.Text);
            if (_isAddOrUpdate)
            {
                memberRepository.InsertMember(GetFormInfo());
                _memberRepository.LoadMemberList();
                MessageBox.Show("Success");
                this.Close();
            }
            else
            {
                memberRepository.UpdateMember(GetFormInfo());
                _memberRepository.LoadMemberList();
                MessageBox.Show("Success");
                this.Close();
            }
        }

        private Member GetFormInfo()
        {

            Member member = null;
            try
            {
                member = new Member
                {
                    MemberId = int.Parse(txtMemberId.Text),
                    Email = txtEmail.Text,
                    CompanyName = txtCompanyName.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text,
                    Password = txtPassword.Text,
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "loi");
            }
            return member;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
