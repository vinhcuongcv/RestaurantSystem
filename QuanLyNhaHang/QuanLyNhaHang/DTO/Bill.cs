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
        private float discount;

        public Bill(int id , DateTime? dateCheckIn , DateTime? dateCheckOut , int status, float discount = 0)
        {
            this.id = id;
            this.dateCheckIn = dateCheckIn;
            this.dateCheckOut = dateCheckOut;
            this.status = status;
            this.discount = discount;
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
            this.status = (int)dataRow["status"];

            if (dataRow["discount"].ToString() != "")
                this.discount = (float)(double)dataRow["discount"];
        }
        public int Id { get => id; set => id = value; }
        public DateTime? DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public int Status { get => status; set => status = value; }
        public float Discount { get => discount; set => discount = value; }

    }
}
