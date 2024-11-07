using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CODE_WebQao.Models.ModelsView
{
    public class CartItem
    {
        public int MaCTDH { get; set; }       // Mã chi tiết đơn hàng
        public int MaSP { get; set; }         // Mã sản phẩm
        public string TenSP { get; set; }     // Tên sản phẩm
        public decimal Gia { get; set; }      // Giá
        public int So_luong { get; set; }     // Số lượng trong giỏ hàng
        public decimal Total => Gia * So_luong; // Tổng giá
    }
}