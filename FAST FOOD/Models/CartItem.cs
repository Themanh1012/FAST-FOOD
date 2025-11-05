using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAST_FOOD.Models
{
    public class CartItem
    {
        public int MonAnId { get; set; }
        public string TenMon { get; set; }
        public decimal Gia { get; set; }
        public string HinhAnh { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien => Gia * SoLuong;
    }
}