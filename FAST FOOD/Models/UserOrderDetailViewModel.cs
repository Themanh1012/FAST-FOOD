using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAST_FOOD.Models
{
    public class UserOrderDetailViewModel
    {
        public string HoTen { get; set; }
        public string Email { get; set; }

        public List<DonHang> DonHangs { get; set; }
    }
}