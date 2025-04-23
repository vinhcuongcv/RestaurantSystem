using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QuanLyNhaHang.DTO
{
    public class Account
    {
        public Account(string username, string displayName, int type, string password = null)
        {
            this.Username = username;
            this.DisplayName = displayName;
            this.Password = password;
            this.Type = type;
        }
        public Account(DataRow row)
        {
            this.Username = row["username"].ToString();
            this.DisplayName = row["displayName"].ToString();
            this.Password = row["password"].ToString();
            this.Type = (int)row["type"];
        }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public int Type { get; set; }
    }
}
