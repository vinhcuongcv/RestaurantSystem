using Microsoft.SqlServer.Server;
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
using System.Text.RegularExpressions;

namespace QuanLyNhaHang
{
    public partial class fTableManeger : Form
    {
        private Account loginAccount;

        public Account LoginAccount {
            get => loginAccount;
            set {
                loginAccount = value;
                ChangeAccount(loginAccount.Type);
            } 
        }
        public fTableManeger(Account acc)
        {
            InitializeComponent();
            this.LoginAccount = acc;
            loadTable();
            LoadCategory();
            loadComboboxTable(cbSwitchTable);

        }
        #region Method
        void ChangeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += " (" + LoginAccount.DisplayName + ")";
        }
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
            flpTable.Controls.Clear();
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
            List<DTO.MenuDTO> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);
            foreach (DTO.MenuDTO item in listBillInfo)
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

        void loadComboboxTable(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.loadTableList();
            cb.DisplayMember = "Name";
        }

        #endregion Method

        private void DangXuatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ThongTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAcccoutProfile f = new fAcccoutProfile(LoginAccount);
            f.ShowDialog();
        }


        
        private void lvBill_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.InsertTable += f_InsertTable;
            f.DeleteTable += f_TableDeleted;  // Đăng ký sự kiện xóa bàn

            f.loginAccount = LoginAccount;  
            f.InsertFood += f_InsertFood;
            f.DeleteFood += f_DeleteFood;
            f.UpdateFood += f_UpdateFood;
            f.ShowDialog();
        }

        private void f_UpdateFood(object sender, EventArgs e)
        {
            LoadListFoodByCategoryID((cbCategory.SelectedItem as Category).Id);
            if (lvBill.Tag != null)
                ShowBill((lvBill.Tag as Table).ID);
            loadTable();
        }

        private void f_DeleteFood(object sender, EventArgs e)
        {
            LoadListFoodByCategoryID((cbCategory.SelectedItem as Category).Id);
            if(lvBill.Tag != null)
                ShowBill((lvBill.Tag as Table).ID);
            loadTable();
        }

        private void f_InsertFood(object sender, EventArgs e)
        {
            LoadListFoodByCategoryID((cbCategory.SelectedItem as Category).Id);
            if (lvBill.Tag != null)
                ShowBill((lvBill.Tag as Table).ID);
            loadTable();
            nmFoodCount.Value = 0;
        }

        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAcccoutProfile f = new fAcccoutProfile(LoginAccount);
            f.UpdateAccountEvent += f_UpdateAccountEvent;   
            f.ShowDialog();
        }

        private void  f_UpdateAccountEvent(object sender,  AccountEvent e)
        {
            thôngTinTàiKhoảnToolStripMenuItem.Text = "Thông tin tài khoản (" + e.Acc.DisplayName + ")";
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
            if(nmFoodCount.Value != 0)
            {
                Table table = lvBill.Tag as Table;
                if (table == null)
                {
                    MessageBox.Show("Hãy chọn bàn");
                    return;
                }
                int idBill = BillDAO.Instance.GetUnCheckedBillIDByTableID(table.ID);
                int idFood = (cbFood.SelectedItem as Food).Id;
                int count = (int)nmFoodCount.Value;

                if (idBill == -1)
                {
                    BillDAO.Instance.InsertBill(table.ID);
                    BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), idFood, count);
                }
                else
                {
                    BillInfoDAO.Instance.InsertBillInfo(idBill, idFood, count);
                }
                ShowBill(table.ID);

                loadTable();
                nmFoodCount.Value = 0;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn số lượng thức ăn muốn thêm !", "Thông báo");
            }    

        }
        //buntton Thanh toán
        private void button4_Click(object sender, EventArgs e)
        {
            if(lvBill.Items.Count != 0)
            {
                Table table = lvBill.Tag as Table;
                int idBill = BillDAO.Instance.GetUnCheckedBillIDByTableID(table.ID);
                float discount = (float)nmDiscount.Value;

                // Xử lý chuỗi số tiền
                string raw = new string(txbTotalPrice.Text.Where(char.IsDigit).ToArray());
                raw = raw.Replace(".", "");  // Xóa dấu phân cách hàng nghìn (nếu có)

                // Chuyển đổi chuỗi thành số tiền
                float totalPrice = float.Parse(raw);
                float finalTotalPrice = totalPrice - (totalPrice / 100 * discount);

                // Kiểm tra hóa đơn chưa thanh toán
                if (idBill != -1)
                {
                    // Hiển thị thông báo thanh toán
                    if (MessageBox.Show(string.Format("Bạn có chắc thanh toán hóa đơn cho {0}\nTổng tiền - (Tổng tiền/100) x Giảm giá = {1} - ({1}/100 x {2}) = {3}",
                                                        table.Name, totalPrice, discount, finalTotalPrice),
                                        "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        // Cập nhật thông tin thanh toán
                        BillDAO.Instance.CheckOut(idBill, discount, finalTotalPrice);
                        ShowBill(table.ID);
                        loadTable();
                    }
                }
                nmDiscount.Value = 0;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn bàn và món ăn để thanh toán !", "Thông báo");
            }    
        }

        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            int idTable1 = (lvBill.Tag as Table).ID;
            int idTable2 = (cbSwitchTable.SelectedItem as Table).ID;
            if (MessageBox.Show(String.Format("Bạn có thật sự muốn chuyển bàn {0} sang bàn {1}", (lvBill.Tag as Table).Name, (cbSwitchTable.SelectedItem as Table).Name),"Thông báo",MessageBoxButtons.OKCancel)== System.Windows.Forms.DialogResult.OK)
            {
                TableDAO.Instance.SwitchTable(idTable1, idTable2);
                loadTable();
            }    

        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thanhToánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button4_Click(this , new EventArgs()); // Gọi thằng event click nút thanh toán
        }

        private void thêmMónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddFood_Click(this, new EventArgs()); // Gọi thằng event click nút thêm món
        }

        private void f_InsertTable(object sender, EventArgs e)
        {
            // Cập nhật danh sách bàn sau khi thêm bàn mới
            loadTable();
        }
        private void f_TableDeleted(object sender, EventArgs e)
        {
            loadTable();  // Cập nhật danh sách bàn ăn trong fTableManager
        }

        private void btnBanking_Click(object sender, EventArgs e)
        {
            float tongTien = float.Parse(new string(txbTotalPrice.Text.Where(char.IsDigit).ToArray()));
            if (tongTien != null && tongTien != 0)
            {
                fVnPayQrCode f = new fVnPayQrCode(tongTien);
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bạn cần có hóa đơn thanh toán");
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}   


