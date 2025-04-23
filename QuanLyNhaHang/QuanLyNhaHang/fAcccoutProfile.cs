using QuanLyNhaHang.DAO;
using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaHang
{
    public partial class fAcccoutProfile : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get => loginAccount;
            set
            {
                loginAccount = value;
                ChangeAccount(loginAccount);
            }
        }
        public fAcccoutProfile(Account acc)
        {
            InitializeComponent();

            LoginAccount = acc;
        }
        void ChangeAccount(Account acc)
        {
            txbUserName.Text = LoginAccount.Username;
            txbDisplayName.Text = LoginAccount.DisplayName;
        }
        void UpdateAccount()
        {
            string userName = txbUserName.Text;
            string disPlayName = txbDisplayName.Text;
            string passWord = txbPassWord.Text;
            string newPassword = txbNewPassWord.Text;
            string confirmPassWord = txbConfirmPassWord.Text;

            if (!newPassword.Equals(confirmPassWord))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu đúng với mật khẩu mới");
            }
            else
            {
                if (AccountDAO.Instance.UpdateAccount(userName, disPlayName, passWord, newPassword))
                {
                    MessageBox.Show("Cập nhật thành công");
                    if(updateAccount != null)
                    {
                        updateAccount(this, new AccountEvent((AccountDAO.Instance.GetAcountByUserName(userName))));
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đúng mật khẩu");
                }
            }
        }
        private event EventHandler<AccountEvent> updateAccount;
        public event EventHandler<AccountEvent> UpdateAccountEvent
        {
            add { updateAccount += value; }
            remove { updateAccount -= value; }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccount();
        }
    }
    public class AccountEvent : EventArgs
    {
        private Account acc;
        public Account Acc
        {
            get { return acc; }
            set { acc = value; }
        }
        public AccountEvent(Account acc)
        {
            this.acc = acc;
        }
    }
}
