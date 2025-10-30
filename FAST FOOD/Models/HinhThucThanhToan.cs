using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FAST_FOOD.Models
{
    public class HinhThucThanhToan
    {
        [Key]
        public int MaHTTT { get; set; }
        public string TenHinhThuc { get; set; }
    }
}