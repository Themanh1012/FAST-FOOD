using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAST_FOOD.Models
{
    public class DanhMucViewModel
    {
        public Danhmuc DanhMuc { get; set; }
        public List<MonAn> MonAns { get; set; }
        public List<Danhmuc> Danhmucs { get; set; }
    }


}