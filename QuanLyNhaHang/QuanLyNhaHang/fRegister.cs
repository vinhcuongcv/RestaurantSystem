using QuanLyNhaHang.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaHang
{
    public partial class fRegister: Form
    {
        public fRegister()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string displayname = txbDisplayName.Text;
            string password = txbPassWord.Text;
            string confirmPassword = txbConfirmPassword.Text;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(displayname) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin");
            }
            else
            {
                if (password != confirmPassword)
                {
                    MessageBox.Show("Mật khẩu không khớp");
                }
                else
                {
                    if (AccountDAO.Instance.InsertAccountRegister(username, displayname, password))
                    {
                        MessageBox.Show("Đăng ký thành công");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Tên tài khoản đã tồn tại");
                    }
                }

            }
        }
    }
}