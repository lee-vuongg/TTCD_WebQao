using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Ban_Quan_Ao.Models.ModelsView
{
    public class Carts
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Qty { get; set; }
        public float Price { get; set; }

        // Total được tính dựa trên Qty và Price
        public decimal Total => (decimal)(Qty * Price);
    }
}