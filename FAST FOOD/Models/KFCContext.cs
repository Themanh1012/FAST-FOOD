using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Collections;
namespace FAST_FOOD.Models
{
    public class KFCContext: DbContext
    {
        public KFCContext(): base("KFCContext"){
                this.Configuration.LazyLoadingEnabled = false;   
            }
        public DbSet<Danhmuc> Danhmucs { get; set; }
       
        public DbSet<MonAn> MonAns { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

      protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //1:N DanhMUc - MONAN
            modelBuilder.Entity<MonAn>()
                .HasRequired(m => m.DanhMuc)
                .WithMany(dm => dm.MonAns)
                .HasForeignKey(m => m.DanhMucId)
                .WillCascadeOnDelete(false);

            //1:N DONHANG -CHITIETDONHANG
            modelBuilder.Entity<ChiTietDonHang>()
                .HasRequired(ct => ct.DonHang)
                .WithMany(d => d.ChiTietDonHangs)
                .HasForeignKey(n => n.DonHangId);

            //1:N MONAN - CHITIETDONHANG

            modelBuilder.Entity<ChiTietDonHang>()
                .HasRequired(ct => ct.MonAn)
                .WithMany(d => d.ChiTietDonHangs)
                .HasForeignKey(n => n.MonAnId);
            base.OnModelCreating(modelBuilder);
        }
    }
}