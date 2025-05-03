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
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }
        private void fLogin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtPassWord.Text;
            if(string.IsNullOrEmpty(username)|| string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo");
            }    
            else
            {
                if (Login(username, password))
                {
                    Account account = AccountDAO.Instance.GetAcountByUserName(username);
                    fTableManeger f = new fTableManeger(account);
                    this.Hide();
                    f.ShowDialog();
                    this.Show();

                    txtUserName.Clear();
                    txtPassWord.Clear();
                    txtUserName.Focus();
                }
                else
                {
                    MessageBox.Show("Bạn nhập sai tài khoản hoặc mật khẩu", "Thông Báo");
                }
            }    
        }
        bool Login(string username , string password)
        {
            return AccountDAO.Instance.Login(username, password);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát chương trình?",
                                "Thông báo",
                                MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            fRegister f = new fRegister();
            f.ShowDialog();
            this.Show();
        }
    }
}
