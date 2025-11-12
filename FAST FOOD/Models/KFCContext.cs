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
                 this.Configuration.ProxyCreationEnabled = false;   
        }
        public DbSet<Danhmuc> Danhmucs { get; set; }
       
        public DbSet<MonAn> MonAns { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
       
        public DbSet<HinhThucThanhToan> HinhThucThanhToans { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }

        public DbSet<account> accounts { get; set; }
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
                .HasForeignKey(ct => ct.DonHangId)
                .WillCascadeOnDelete(true);

            //1:N MONAN - CHITIETDONHANG

            modelBuilder.Entity<ChiTietDonHang>()
                .HasRequired(ct => ct.MonAn)
                .WithMany(d => d.ChiTietDonHangs)
                .HasForeignKey(ct => ct.MonAnId)
                .WillCascadeOnDelete(false);

            //1:N HINHTHUCTHANHTOAN - DONHANG
            modelBuilder.Entity<DonHang>()
                .HasOptional(d => d.HinhThucThanhToan)
                .WithMany()
                .HasForeignKey(d => d.MaHTTT)
                .WillCascadeOnDelete(false);

            //1:1 :DOnhang co 1 hoa don
            modelBuilder.Entity<HoaDon>()
                .HasRequired(hd => hd.DonHang)
                .WithMany()
                .WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);

           
        }
    }
}