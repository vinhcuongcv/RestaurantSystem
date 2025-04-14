using QuanLyNhaHang.DAO;
using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaHang
{
    public partial class fTableManeger : Form
    {
        public fTableManeger()
        {
            InitializeComponent();
            loadTable();
            LoadCategory();
            //LoadFood();
        }
        #region Method

        void LoadCategory()
        {
            List<Category> list = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = list;
            cbCategory.DisplayMember = "Name";
        }

        void LoadListFoodByCategoryID(int iD)
        {
            List<Food> list = FoodDAO.Instance.GetListFoodByCategoryID(iD);
            cbFood.DataSource = list;
            cbFood.DisplayMember = "Name";
        }

        void loadTable()
        {
            List<Table> table = TableDAO.Instance.loadTableList();


            foreach (Table item in table)
            {
                Button btn = new Button() { Width = TableDAO.widthTable, Height = TableDAO.heightTable };
                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += Btn_Click;
                btn.Tag = item;
                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = ColorTranslator.FromHtml("#54FF9F");
                        break;
                    default:
                        btn.BackColor = ColorTranslator.FromHtml("#FF6A6A");
                        break;
                }
                flpTable.Controls.Add(btn);
            }
        }

        void ShowBill(int id)
        {
            lvBill.Items.Clear();
            double totalPrice = 0;
            List<MenuDTO> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);
            foreach (MenuDTO item in listBillInfo)
            {
                ListViewItem list = new ListViewItem(item.FoodName.ToString());
                list.SubItems.Add(item.Count.ToString());
                list.SubItems.Add(item.Price.ToString());
                list.SubItems.Add(item.Total.ToString());
                lvBill.Items.Add(list);
                totalPrice += item.Total;
            }
            CultureInfo cul = new CultureInfo("vi-VN");
            txbTotalPrice.ForeColor = ColorTranslator.FromHtml("#FF3300");
            txbTotalPrice.Text = totalPrice.ToString("#,##0 ₫", cul);
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;
            lvBill.Tag = (sender as Button).Tag;
            ShowBill(tableID);
        }

        #endregion

        private void DangXuatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ThongTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAcccoutProfile f = new fAcccoutProfile();
            f.ShowDialog();
        }


        
        private void lvBill_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.ShowDialog();
        }

        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAcccoutProfile f = new fAcccoutProfile();
            f.ShowDialog();
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;

            if(cb.SelectedItem == null) 
            {
                return;
            }    
            Category selected = cb.SelectedItem as Category;
            id = selected.Id;

            LoadListFoodByCategoryID(id);
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lvBill.Tag as Table;
            int idBill = BillDAO.Instance.GetUnCheckedBillIDByTableID(table.ID);
            int idFood = (cbFood.SelectedItem as Food).Id;
            int count = (int)nmFoodCount.Value;

            if(idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), idFood, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, idFood, count);
            }
            ShowBill(table.ID);
        }
    }
}


