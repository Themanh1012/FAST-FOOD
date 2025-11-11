namespace FAST_FOOD.Migrations
{
    using FAST_FOOD.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FAST_FOOD.Models.KFCContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FAST_FOOD.Models.KFCContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            //if (!context.Danhmucs.Any())
            //{
            //    context.Danhmucs.AddOrUpdate(
            //        d=> d.TenDanhMuc,
            //    new Danhmuc{ TenDanhMuc = "Gà rán" },
            //    new Danhmuc { TenDanhMuc = "Mì ý" },
            //    new Danhmuc { TenDanhMuc = "Cơm gà" }
            //    );
            //    context.SaveChanges();
            //        }
            //context.DonHangs.AddOrUpdate(
            //    new DonHang
            //    {
            //        TenKhachHang = "Nguyễn Văn A",
            //        DiaChi = "QUận 12 ,tphcm",
            //        SoDienThoai = "999999999",
            //        NgayDat = DateTime.Now.AddDays(-1),
            //        TrangThai = "Đang xử lý",
            //        TongTien = 19990000,
            //    },
            //    new DonHang
            //    {
            //        TenKhachHang = "Nguyễn Văn B",
            //        DiaChi = "TPHCM",
            //        SoDienThoai = "1111111",
            //        NgayDat = DateTime.Now,
            //        TrangThai = "Hoàn Tất",
            //        TongTien = 9999999,
            //    }
            //    );
            var donHangA = context.DonHangs.FirstOrDefault(d => d.TenKhachHang == "Nguyễn Văn A");
            var monAn = context.MonAns.FirstOrDefault();

            if (donHangA != null && monAn != null && !context.ChiTietDonHangs.Any())
            {
                context.ChiTietDonHangs.AddOrUpdate(
                    new ChiTietDonHang
                    {
                        DonHangId = donHangA.DonHangId,
                        MonAnId = monAn.MonAnId,
                        SoLuong = 2,
                        ThanhTien = monAn.Gia * 2
                    }
                );
                context.SaveChanges();
            }

            context.SaveChanges();
        }
    }
}
