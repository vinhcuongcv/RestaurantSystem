using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaHang.DAO
{
     public class TableDAO      
    {
        private static TableDAO instance;
        
        public static int widthTable = 95;
        public static int heightTable = 95;

        private TableDAO() { }      
        public static TableDAO Instance
        {   
            get { if (instance == null) instance = new TableDAO(); return TableDAO.instance; }
            private set { TableDAO.instance = value; }
        }
        public void SwitchTable(int idTable1, int idTable2)
        {
            DataProvider.Instance.ExcuteQuery("EXEC USP_SwitchTable @idTable1 , @idTable2", new object[] { idTable1, idTable2 });
        }
        public List<Table> loadTableList()
        {
            List<Table> tableList = new List<Table>();
            DataTable data = DataProvider.Instance.ExcuteQuery("USP_GetTableList");
            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }
            return tableList;
        }
        public bool CheckTableNameExists(string name)
        {
            string query = "SELECT COUNT(*) FROM TableFood WHERE name = @name";
            object result = DataProvider.Instance.ExcuteScalar(query, new object[] { name });
            int count = Convert.ToInt32(result);

            return count > 0;
        }

        // Thêm bàn mới 
        public bool InsertTable(string name)
        {
            if (CheckTableNameExists(name))
            {
                MessageBox.Show("Tên bàn đã tồn tại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string query =string.Format("INSERT INTO TableFood (name, status) VALUES(N'{0}', N'Trống')", name);
            int result = DataProvider.Instance.ExcuteNonQuery(query);

            return result > 0;
        }
        public bool DeleteTable(string id)
        {
            string query = "DELETE FROM dbo.TableFood WHERE id = " + id;
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }

    }
}
