using QuanLyNhaHang.DAO;
using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaHang
{
    public partial class fAdmin : Form
    {
        BindingSource foodList = new BindingSource();
        BindingSource accountList = new BindingSource();

        BindingSource categoryList = new BindingSource();
        BindingSource tableList = new BindingSource();

        public Account loginAccount;
        public fAdmin()
        {
            InitializeComponent();
            LoadAll();
        }
        void LoadAll()
        {
            dtgvFood.DataSource = foodList;
            dtgvAccount.DataSource = accountList;
            dtgvCategory.DataSource = categoryList;
            dtgvTable.DataSource = tableList;

            LoadDateTimePicker();
            LoadListByDate(dtpFromDate.Value, dtpToDate.Value);
            LoadListFood();
            LoadAccount();
            addFoodBinding();
            addAccountBinding();
            LoadCategoryInfoCombobox(cbCategory);
            LoadListCategory();
            addCategoryBinding();
            LoadListTable();
            addTableBinding();
        }
        private void fAdmin_Load(object sender, EventArgs e)
        {

        }

        private SqlConnection SqlConnection(string v)
        {
            throw new NotImplementedException();
        }
        #region Methods
        List<Food> SearchFoodByName(string name)
        {
            List<Food> listFood = FoodDAO.Instance.SearchFoodByName(name);
            return listFood;
        }
        void LoadDateTimePicker()
        {
            DateTime toDay = DateTime.Now;
            dtpFromDate.Value = new DateTime(toDay.Year, toDay.Month, 1);
            dtpToDate.Value.AddMonths(1).AddDays(-1);
        }
        void LoadListByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvThongKe.DataSource = BillDAO.Instance.GetListBillByDate(checkIn, checkOut);
        }

        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }

        void addFoodBinding()
        {
            txbFood.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "id", true, DataSourceUpdateMode.Never));
            nmTotalPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));

        }
        void LoadListCategory()
        {
            categoryList.DataSource = CategoryDAO.Instance.GetListCategory();
        }
        void LoadListTable()
        {
            tableList.DataSource = TableDAO.Instance.loadTableList();
        }
        void addTableBinding()
        {
            txbIdTable.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "id", true, DataSourceUpdateMode.Never));
            txbNameTable.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "name", true, DataSourceUpdateMode.Never));
            txbStatusTable.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "status", true, DataSourceUpdateMode.Never));
        }
        void addCategoryBinding()
        {
            txbCategoryName.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "name", true, DataSourceUpdateMode.Never));
            txbIDCategory.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "id", true, DataSourceUpdateMode.Never));
        }
        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }
        void addAccountBinding()
        {
            txbUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txbDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            nmAccountType.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }
        void LoadCategoryInfoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }
        void AddAccount(string username, string displayname, int type)
        {
            if (AccountDAO.Instance.InsertAccount(username, displayname, type))
            {
                MessageBox.Show("Thêm tài khoản thành công");
                LoadAccount();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm tài khoản");
            }
            LoadAccount();

        }
        void EditAccount(string username, string displayname, int type)
        {
            if (AccountDAO.Instance.EditAccount(username, displayname, type))
            {
                MessageBox.Show("Cập nhật tài khoản thành công");
                LoadAccount();
            }
            else
            {
                MessageBox.Show("Có lỗi khi cập nhật tài khoản");
            }
            LoadAccount();
        }
        void DeleteAccount(string username)
        {
            if (loginAccount.Username.Equals(username))
            {
                MessageBox.Show("Không thể xóa tài khoản đang đăng nhập");
                return;
            }
            if (AccountDAO.Instance.DeleteAccount(username))
            {
                MessageBox.Show("Xóa tài khoản thành công");
                LoadAccount();
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa tài khoản");
            }
            LoadAccount();
        }

        void ResetAccount(string username)
        {
            if (AccountDAO.Instance.ResetPassword(username))
            {
                MessageBox.Show("Reset khoản thành công");
                LoadAccount();
            }
            else
            {
                MessageBox.Show("Có lỗi khi reset tài khoản");
            }
            LoadAccount();
        }
        #endregion Methods

        #region Events
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            LoadListByDate(dtpFromDate.Value, dtpToDate.Value);
        }


        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void txbID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvFood.SelectedCells.Count > 0)
                {
                    object value = dtgvFood.SelectedCells[0].OwningRow.Cells["IdCategory"].Value;

                    if (value != null && int.TryParse(value.ToString(), out int id))
                    {
                        Category category = CategoryDAO.Instance.GetCategoryByID(id);

                        if (category != null)
                        {
                            cbCategory.SelectedItem = category;

                            int index = -1;
                            int i = 0;
                            foreach (Category item in cbCategory.Items)
                            {
                                if (item.Id == category.Id)
                                {
                                    index = i;
                                    break;
                                }
                                i++;
                            }

                            cbCategory.SelectedIndex = index;
                        }
                    }
                }
            }
            catch
            {
                // Có thể log lỗi nếu cần nhưng để trống cũng OK để không làm gián đoạn
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string name = txbFood.Text;
            int idCategory = (cbCategory.SelectedItem as Category).Id;
            float nmPrice = (float)nmTotalPrice.Value;

            if (FoodDAO.Instance.InsertFood(name, idCategory, nmPrice))
            {
                MessageBox.Show("Thêm món thành công");
                LoadListFood();
                if (insertFood != null)
                {
                    insertFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm món");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string name = txbFood.Text;
            int idCategory = (cbCategory.SelectedItem as Category).Id;
            float nmPrice = (float)nmTotalPrice.Value;
            int id = Convert.ToInt32(txbID.Text);

            if (FoodDAO.Instance.EditFood(id, name, idCategory, nmPrice))
            {
                MessageBox.Show("Cập nhật món ăn thành công");
                LoadListFood();
                if (updateFood != null)
                {
                    updateFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi cập nhật món ăn");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string name = txbFood.Text;
            int idCategory = (cbCategory.SelectedItem as Category).Id;
            float nmPrice = (float)nmTotalPrice.Value;
            int id = Convert.ToInt32(txbID.Text);

            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Xóa món ăn thành công");
                LoadListFood();
                if (deleteFood != null)
                {
                    deleteFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa món ăn");
            }
        }

        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }

        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }

        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            foodList.DataSource = SearchFoodByName(txbSearch.Text);
        }

        private void bntShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }

        private void btnThemAcc_Click(object sender, EventArgs e)
        {
            string username = txbUserName.Text;
            string displayname = txbDisplayName.Text;
            int type = (int)nmAccountType.Value;

            AddAccount(username, displayname, type);
        }

        private void btnSuaAcc_Click(object sender, EventArgs e)
        {
            string username = txbUserName.Text;
            string displayname = txbDisplayName.Text;
            int type = (int)nmAccountType.Value;

            EditAccount(username, displayname, type);
        }

        private void btnXoaAcc_Click(object sender, EventArgs e)
        {
            string username = txbUserName.Text;

            DeleteAccount(username);
        }

        private void btnResetAcc_Click(object sender, EventArgs e)
        {
            string username = txbUserName.Text;
            ResetAccount(username);
        }

        private void btnFirstBill_Click(object sender, EventArgs e)
        {
            txbPageCount.Text = "1";
        }

        private void btnLastBill_Click(object sender, EventArgs e)
        {
            int sumRecord = BillDAO.Instance.GetNumBillByDate(dtpFromDate.Value, dtpToDate.Value);
            int lastPage = sumRecord / 13;

            if (sumRecord % 13 != 0)
            {
                lastPage++;
            }
            txbPageCount.Text = lastPage.ToString();
        }

        private void txbPageCount_TextChanged(object sender, EventArgs e)
        {
            dtgvThongKe.DataSource = BillDAO.Instance.GetListBillByDateAndPage(dtpFromDate.Value, dtpToDate.Value, Convert.ToInt32(txbPageCount.Text));
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int pageCount = Convert.ToInt32(txbPageCount.Text);
            int totalRecords = BillDAO.Instance.GetNumBillByDate(dtpFromDate.Value, dtpToDate.Value);
            int maxPage = (int)Math.Ceiling((double)totalRecords / 13); // mỗi trang 10 dòng

            if (pageCount < maxPage)
            {
                pageCount++;
                txbPageCount.Text = pageCount.ToString();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            int pageCount = Convert.ToInt32(txbPageCount.Text);
            if (pageCount > 1)
            {
                pageCount--;
                txbPageCount.Text = pageCount.ToString();
            }
        }

        private void btnShowDanhMuc_Click(object sender, EventArgs e)
        {
            LoadListCategory();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;

            if (CategoryDAO.Instance.InsertCategory(name))
            {
                MessageBox.Show("Thêm danh mục thành công");
                LoadListCategory();
                LoadCategoryInfoCombobox(cbCategory);
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm danh mục");
            }
        }

        private void btnChangeCategory_Click(object sender, EventArgs e)
        {
            string id = txbIDCategory.Text;
            string name = txbCategoryName.Text.Trim();
            if (CategoryDAO.Instance.UpdateCategory(name, id))
            {
                MessageBox.Show("Cập nhật danh mục thành công");
                LoadListCategory();
                LoadCategoryInfoCombobox(cbCategory);
            }
            else
            {
                MessageBox.Show("Có lỗi khi cập nhật danh mục");
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            string id = txbIDCategory.Text;
            if (CategoryDAO.Instance.DeleteCategory(id))
            {
                MessageBox.Show("Xóa danh mục thành công");
                LoadListCategory();
                LoadCategoryInfoCombobox(cbCategory);
            }
            else
            {
                MessageBox.Show("Xóa danh mục thất bại");
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadListTable();
        }
        public event EventHandler InsertTable;

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = txbNameTable.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Tên bàn không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (TableDAO.Instance.CheckTableNameExists(name))
            {
                MessageBox.Show("Tên bàn đã tồn tại, vui lòng nhập tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (TableDAO.Instance.InsertTable(name))
            {
                MessageBox.Show("Thêm bàn mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListTable(); // reload lại giao diện các bàn
                InsertTable?.Invoke(this, EventArgs.Empty);  // Kích hoạt sự kiện khi bàn được thêm
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm bàn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public event EventHandler DeleteTable;

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            string id = txbIdTable.Text.Trim();
            if (CategoryDAO.Instance.DeleteCategory(id))
            {
                MessageBox.Show("Xóa bàn thành công");
                LoadListTable();
                DeleteTable?.Invoke(this, EventArgs.Empty);  // Kích hoạt sự kiện thông báo đã xóa bàn

            }
            else
            {
                MessageBox.Show("Xóa b thất bại");
            }
        }

        private void dtgvThongKe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure the clicked cell is within a valid row  
            {
                DataGridViewRow row = dtgvThongKe.Rows[e.RowIndex]; 
                int id = Convert.ToInt32(row.Cells["Mã hóa đơn"].Value);
                string tableName = row.Cells["Tên bàn"].Value.ToString();
                fBillInfor billDetailForm = new fBillInfor(id, tableName);
                billDetailForm.ShowDialog();
            }
        }

        private void dtgvAccount_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgvTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    #endregion Events

}
