using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return BillDAO.instance; }
            private set { BillDAO.instance = value; }
        }
    
        private BillDAO() { }

        public int GetUnCheckedBillIDByTableID(int idTable)
        {
            DataTable data = DataProvider.Instance.ExcuteQuery("SELECT*FROM dbo.Bill WHERE  idTable =" +  idTable +" AND status = 0"); 
            if(data.Rows.Count>0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.Id;
            }
            return -1;
        }
        public void InsertBill(int id)  
        {
            DataProvider.Instance.ExcuteNonQuery("Exec USP_InsertBill @idTable", new Object[] { id });
        }

        public void CheckOut(int id,float discount, float totalPrice)
        {
            string query = "UPDATE dbo.Bill set dateCheckOut = GetDate() , status = 1, discount = "+discount+",totalPrice = " + totalPrice +  "where id = "+ id;
            DataProvider.Instance.ExcuteNonQuery(query);    
        }

        public DataTable GetListBillByDate(DateTime checkIn,DateTime checkOut)
        {
            return DataProvider.Instance.ExcuteQuery("EXEC USP_GetListBillByDate @dateCheckIn , @dateCheckOut", new object[] { checkIn, checkOut });
        }

        public int GetMaxIDBill()
        {
            try
            {
                return (int)DataProvider.Instance.ExcuteScalar("SELECT MAX(id) from dbo.Bill");
            }
            catch
            {
                return 1;
            }
        }
    }
}

