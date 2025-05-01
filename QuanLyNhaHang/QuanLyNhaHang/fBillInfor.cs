using QuanLyNhaHang.DAO;
using QuanLyNhaHang.DTO;
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
    public partial class fBillInfor: Form
    {
        private int id;

        public fBillInfor(int id , string tableName)
        {
            InitializeComponent();
            this.id = id;
            lblTableBill.Text = "Hóa Đơn "+tableName;
            LoadBillInfo();
        }
        void LoadBillInfo()
        {
            string query = "EXEC USP_GetBillInfoByBillID @billId";

            // Chạy truy vấn và lấy dữ liệu
            DataTable data = DataProvider.Instance.ExcuteQuery(query, new object[] { id });

            // Đưa dữ liệu vào DataGridView
            dtgvBill.DataSource = data;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
