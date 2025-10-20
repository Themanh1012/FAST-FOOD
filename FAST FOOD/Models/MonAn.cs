using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FAST_FOOD.Models
{
    public class MonAn
    {
        [Key]
        public int MonAnId { get; set; }

        [Required, StringLength(150)]
        public string TenMon { get; set; }

        public decimal Gia { get; set; }
        public string HinhAnh { get; set; }

        //khoa ngoai
        
        public int DanhMucId {  get; set; }
        [ForeignKey("DanhMucId")]
        public virtual Danhmuc DanhMuc { get; set; }

        //N:N
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}