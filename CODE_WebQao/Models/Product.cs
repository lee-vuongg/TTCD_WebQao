using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CODE_WebQao.Models.ModelsView
{
    public class Product
    {
        public int MaSP { get; set; }      // Mã sản phẩm
        public string TenSP { get; set; }   // Tên sản phẩm
        public decimal Gia { get; set; }    // Giá
        public int So_luong { get; set; }   // Số lượng tồn kho
        public string Mo_ta { get; set; }   // Mô tả
        public string Trang_thai { get; set; } // Trạng thái
    }
}