using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DTO
{
    public class Food
    {
        private int id;
        private string name;
        private int idCategory;
        private float price;
        
        public Food(int id , string name , int idCategory , float price)
        {
            this.id = id;
            this.name = name;
            this.idCategory = idCategory;
            this.price = price;
        }

        public Food(DataRow data)
        {
            this.id = (int)data["id"];
            this.name = (string)data["name"].ToString();
            this.idCategory = (int)data["idCategory"];
            this.price = (float)Convert.ToDouble(data["price"].ToString());
        }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int IdCategory { get => idCategory; set => idCategory = value; }
        public float Price { get => price; set => price = value; }

    }
}
