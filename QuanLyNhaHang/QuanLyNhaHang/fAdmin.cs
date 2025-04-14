using QuanLyNhaHang.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaHang
{
    public partial class fAdmin : Form
    {
        public fAdmin()
        {
            InitializeComponent();
            //loadAccountList();
            dtgAccount.DataSource = DataProvider.Instance.ExcuteQuery("select * from dbo.Account where UserName =  N'Vĩnh Cường' AND PassWord = N'' or 1=1");
        }

        private void fAdmin_Load(object sender, EventArgs e)
        {

        }
        //void loadAccountList()  
        //{
        //    string query = "Execute dbo.USP_GetAccoutByUserName @username";
        //    dtgAccount.DataSource = DataProvider.Instance.ExcuteQuery(query,new object[]{"Vĩnh Cường"});
        //} 

        private SqlConnection SqlConnection(string v)
        {
            throw new NotImplementedException();
        }
    }
}
