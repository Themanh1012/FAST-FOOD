using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAST_FOOD.Models
{
    public class UserOrderViewModel
    {
        public int UserId { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalMoney { get; set; }
        public DateTime? LastOrder { get; set; }
    }
}