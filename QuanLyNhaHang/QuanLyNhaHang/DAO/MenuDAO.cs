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
    public class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Instance
        {
            get { if (instance == null) instance = new MenuDAO(); return MenuDAO.instance; }
            private set { MenuDAO.instance = value; }
        }

        private MenuDAO()
        {

        }
        public List<MenuDTO> GetListMenuByTable(int id)
        {
            List<MenuDTO> listMenu = new List<MenuDTO>();
            DataTable data = DataProvider.Instance.ExcuteQuery("SELECT Food.name , BillInfo.count , Food.price,Food.price*BillInfo.count AS TotalPrice FROM dbo.Food , dbo.Bill , dbo.BillInfo\r\nWHERE dbo.BillInfo.idBill = dbo.Bill.id AND\r\n\t  dbo.BillInfo.idFood = dbo.Food.id AND dbo.Bill.status = 0 AND Bill.idTable ="+ id);
            foreach (DataRow item in data.Rows)
            {
                MenuDTO menu = new MenuDTO(item);
                listMenu.Add(menu);
            }


            return listMenu;
        }
    }
}
