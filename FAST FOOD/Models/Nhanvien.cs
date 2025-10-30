using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FAST_FOOD.Models
{
    public class Nhanvien
    {
        [Key]
        public int MaNV { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public string ChucVu { get; set; }
        public decimal Luong { get; set; }

    }
}