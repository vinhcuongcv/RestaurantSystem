    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace QuanLyNhaHang.DTO 
    {
        public class MenuDTO
        {
            private string foodName;
            private int count;
            private double totalPrice;
            private double price;

            public MenuDTO(string foodName, int count,double price, double totalPrice)
            { 
                this.foodName = foodName;
                this.count = count;
                this.price = price;
                this.totalPrice = totalPrice;
            }
            public MenuDTO(DataRow data)
            {
                this.foodName = data["name"].ToString();
                this.count = (int)data["count"];
                this.price = Convert.ToDouble(data["price"]);
                this.totalPrice = Convert.ToDouble(data["TotalPrice"]);
        }

            public string FoodName { get => foodName; set => foodName = value; }
            public int Count { get => count; set => count = value; }
            public double Total { get => totalPrice; set => totalPrice = value; }
            public double Price { get => price; set => price = value; }
        }
    }
