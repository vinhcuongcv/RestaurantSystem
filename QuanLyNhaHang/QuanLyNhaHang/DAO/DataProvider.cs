using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaHang.DAO
{
    class DataProvider
    {
        string connectionSTR = "Data Source=LAPTOP-OLVVUIGV\\TESTDB;Initial Catalog=QuanLyNhaHang;Integrated Security=True";
        private static DataProvider instance;

        internal static DataProvider Instance 
        {
            get { if (instance == null) instance = new DataProvider(); return instance; }
            private set { instance = value; }
        }


        public DataTable ExcuteQuery(string query, object[] paramater = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                try
                {
                    connection.Open(); // Mở kết nối
                    DataTable data = new DataTable();

                    SqlCommand command = new SqlCommand(query, connection);

                    if (paramater != null)
                    {
                        string[] listPara = query.Split(' ');
                        int i = 0;
                        foreach (string item in listPara)
                        {
                            if (item.Contains("@"))
                            {
                                command.Parameters.AddWithValue(item, paramater[i]);
                                i++;
                            }
                        }
                    }
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    sqlDataAdapter.Fill(data);
                    connection.Close(); // Đóng kết nối
                    return data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kết nối SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new DataTable();
                }
            }
        }
        public int ExcuteNonQuery(string query , object[] paramater = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                try
                {
                    int data = 0;
                    connection.Open(); // Mở kết nối

                    SqlCommand command = new SqlCommand(query, connection);

                    if(paramater != null)
                    {
                        string[] listPara = query.Split(' ');
                        int i = 0;
                        foreach (string item in listPara)
                        {
                            if(item.Contains("@"))
                            {
                                command.Parameters.AddWithValue(item, paramater[i]);
                                i++;    
                            }    
                        }
                    }    
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    data = command.ExecuteNonQuery();
                    connection.Close(); // Đóng kết nối
                    return data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kết nối SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
            }
        }

        public object ExcuteScalar(string query, object[] paramater = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                try
                {
                    object data = 0;
                    connection.Open(); // Mở kết nối

                    SqlCommand command = new SqlCommand(query, connection);

                    if (paramater != null)
                    {
                        string[] listPara = query.Split(' ');
                        int i = 0;
                        foreach (string item in listPara)
                        {
                            if (item.Contains("@"))
                            {
                                command.Parameters.AddWithValue(item, paramater[i]);
                                i++;
                            }
                        }
                    }
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    data = command.ExecuteScalar();
                    connection.Close(); // Đóng kết nối
                    return data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kết nối SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
            }
        }
    }
}
