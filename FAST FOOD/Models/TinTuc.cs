using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAST_FOOD.Models
{
    public class TinTuc
    {
        public int Id { get; set; }
        public string TieuDe { get; set; }
        public string TomTat { get; set; }
        public string NoiDungChiTiet { get; set; }
        public DateTime NgayDang { get; set; }
        public string HinhAnh { get; set; }
        public string Slug { get; set; } // Dùng cho URL thân thiện
    }
}