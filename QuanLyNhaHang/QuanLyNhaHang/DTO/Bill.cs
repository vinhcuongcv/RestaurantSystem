using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DTO
{
    public class Bill
    {
        private int id;
        private DateTime? dateCheckIn;
        private DateTime? dateCheckOut;
        private int status;

        public Bill(int id , DateTime? dateCheckIn , DateTime? dateCheckOut , int status)
        {
            this.id = id;
            this.dateCheckIn = dateCheckIn;
            this.dateCheckOut = dateCheckOut;
            this.Status = status;
        }

        public Bill(DataRow dataRow)
        {
            this.id = (int)dataRow["id"];
            this.dateCheckIn = (DateTime?)dataRow["dateCheckIn"];
            var dateCheckOutTemp = dataRow["dateCheckOut"];
            if(dateCheckOutTemp.ToString() != "")
            {
                this.dateCheckOut = (DateTime?)dataRow["dateCheckOut"];
            }
            this.Status = (int)dataRow["status"];   

        }
        public int Id { get => id; set => id = value; }
        public DateTime? DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public int Status { get => status; set => status = value; }
    }
}
