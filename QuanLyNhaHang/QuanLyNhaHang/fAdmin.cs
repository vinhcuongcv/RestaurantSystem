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
            txbFood.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name"));
            txbID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "id"));
            nmTotalPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price"));

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

        #endregion Events

        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void txbID_TextChanged(object sender, EventArgs e)
        {
            if (dtgvFood.SelectedCells.Count > 0)
            {
                int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["IdCategory"].Value;

                Category category = CategoryDAO.Instance.GetCategoryByID(id);

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
