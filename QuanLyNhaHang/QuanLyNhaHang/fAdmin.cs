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
        BindingSource FoodList = new BindingSource();   
        public fAdmin()
        {
            InitializeComponent();
            LoadAll();
        }
        void LoadAll()
        {
            dtgvFood.DataSource = FoodList; 

            LoadDateTimePicker();
            LoadListByDate(dtpFromDate.Value, dtpToDate.Value);
            LoadListFood();
            addFoodBinding();
            LoadCategoryInfoCombobox(cbCategory);
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
        void LoadListByDate(DateTime checkIn , DateTime checkOut)
        {
            dtgvThongKe.DataSource =  BillDAO.Instance.GetListBillByDate(checkIn, checkOut);
        }

        void LoadListFood()
        {
            FoodList.DataSource = FoodDAO.Instance.GetListFood();
        }

        void addFoodBinding()
        {
            txbFood.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name",true,DataSourceUpdateMode.Never));
            txbID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "id", true, DataSourceUpdateMode.Never));
            nmTotalPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));

        }

        void LoadCategoryInfoCombobox(ComboBox cb)
        {
           cb.DataSource = CategoryDAO.Instance.GetListCategory();
           cb.DisplayMember = "Name";
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

            if(FoodDAO.Instance.InsertFood(name, idCategory, nmPrice))
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

            if (FoodDAO.Instance.EditFood(id,name, idCategory, nmPrice))
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
                if(deleteFood!= null)
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
        #endregion Events

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FoodList.DataSource = SearchFoodByName(txbSearch.Text);
        }
    }
    }
