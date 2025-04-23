using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance
        {
            get { if (instance == null) instance = new BillInfoDAO(); return BillInfoDAO.instance; }
            private set { BillInfoDAO.instance = value; }
        }

        private BillInfoDAO () {  }

        public List<BillInfo> GetListBillInfo(int id)
        {
            List<BillInfo> listBillInfo = new List<BillInfo>();
            DataTable data = DataProvider.Instance.ExcuteQuery("SELECT*FROM BillInfo WHERE idBill = " + id);
    
            foreach (DataRow item in data.Rows)
            {
                BillInfo bill = new BillInfo(item);
                listBillInfo.Add(bill);
            }

            return listBillInfo;
        }
        public void InsertBillInfo(int idBill , int idFood, int count)
        {
            DataProvider.Instance.ExcuteNonQuery("Exec USP_InsertBillInfo @idBill , @idFood , @count", new Object[] {idBill,idFood,count});
        }
        public void DeleteBillInfoByFoodID(int id)
        {
            DataProvider.Instance.ExcuteNonQuery("DELETE dbo.BillInfo WHERE idFood = " + id);
        }
    }
}
