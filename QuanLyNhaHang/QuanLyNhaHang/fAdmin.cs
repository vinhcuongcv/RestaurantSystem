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
            LoadDateTimePicker();
            LoadListByDate(dtpFromDate.Value, dtpToDate.Value);
        }

        private void fAdmin_Load(object sender, EventArgs e)
        {

        }

        private SqlConnection SqlConnection(string v)
        {
            throw new NotImplementedException();
        }
        #region Methods
        void LoadDateTimePicker()
        {
            DateTime toDay = DateTime.Now;
            dtpFromDate.Value = new DateTime(toDay.Year, toDay.Month, 1);
            dtpToDate.Value.AddMonths(1).AddDays(-1);
        }
        void LoadListByDate(DateTime checkIn , DateTime checkOut)
        {
            dtgvThongKe.DataSource =  BillDAO.Instance.GetListBillByDate(checkIn, checkOut);
        }
        #endregion Methods

        #region Events
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            LoadListByDate(dtpFromDate.Value, dtpToDate.Value);
        }
        #endregion Events
    }
}
