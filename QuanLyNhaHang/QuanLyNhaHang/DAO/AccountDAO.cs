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
        public DataTable GetListAccount()
        {
            DataTable data = DataProvider.Instance.ExcuteQuery("select UserName, DisplayName, Type from dbo.Account");
            return data;
        }

        public bool InsertAccount(string username , string displayname , int type)
        {
            string query = string.Format("INSERT dbo.Account  (UserName , DisplayName , Type) Values ( N'{0}' , N'{1}'  , {2} )", username, displayname, type);
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }
        public bool InsertAccountRegister(string username , string displayname , string password)
        {
            string query = string.Format("EXEC USP_InsertAccount @username = N'{0}' , @displayname = N'{1}' , @password = N'{2}' , @type = {3}", username, displayname, password , 0);
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }
        public bool EditAccount(string username, string displayname, int type)
        {
            string query = string.Format("UPDATE dbo.Account SET DisplayName = N'{1}' , Type = {2} WHERE UserName = N'{0}'", username, displayname, type);
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteAccount(string username)
        {
            string query = string.Format("Delete dbo.Account Where UserName = N'{0}'",username);
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }

        public bool ResetPassword(string username)
        {
            string query = string.Format("UPDATE dbo.Account SET PassWord = N'0' WHERE UserName = N'{0}'", username);
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }
    }
}
