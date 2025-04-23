using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }
        public bool Login(string username, string password)
        {
            string query = "select *  from dbo.Account where UserName =  N'" + username + "' AND PassWord = N'" + password + "'";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            return data.Rows.Count > 0;
        }
        public bool UpdateAccount(string username, string displayName, string password ,string newpassword)
        {
            int result = DataProvider.Instance.ExcuteNonQuery("EXEC USP_UpdateAccount @userName , @displayName , @password , @newPassword", new object[] { username, displayName, password, newpassword });
            return result > 0;
        }
        public Account GetAcountByUserName(string username)
        {
            DataTable data = DataProvider.Instance.ExcuteQuery("select * from dbo.Account where UserName = N'" + username + "'");

            foreach (DataRow item in data.Rows)
            {   
                return new Account(item);
            }
            return null;
        }

    }
}
