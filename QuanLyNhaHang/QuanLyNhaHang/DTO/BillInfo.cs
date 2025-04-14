using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DTO
{
    public class BillInfo
    {
        private int id;
        private int idBill;
        private int idFood;
        private int count;

        public BillInfo(int id, int idBill, int idFood, int count)
        {
            this.id = id;
            this.idBill = idBill;
            this.idFood = idFood;
            this.count = count;
        }

        public BillInfo(DataRow data)
        {
            this.id = (int)data["id"];
            this.idBill = (int)data["idBill"];
            this.idFood = (int)data["idFood"];
            this.count = (int)data["count"];
        }
        public int Id { get => id; set => id = value; }
        public int IdBill { get => idBill; set => idBill = value; }
        public int IdFood { get => idFood; set => idFood = value; }
        public int Count { get => count; set => count = value; }
    }
}
