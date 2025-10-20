using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FAST_FOOD.Models
{
    public class Danhmuc
    {
        [Key]
        public int DanhMucId {  get; set; }
        [Required, StringLength(100)]
        public string TenDanhMuc { get; set; }

        //1:N
        public virtual ICollection<MonAn> MonAns { get; set; }
    }
}