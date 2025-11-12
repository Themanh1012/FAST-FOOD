namespace FAST_FOOD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChiTietDonHangs",
                c => new
                    {
                        ChiTietId = c.Int(nullable: false, identity: true),
                        DonHangId = c.Int(nullable: false),
                        MonAnId = c.Int(nullable: false),
                        SoLuong = c.Int(nullable: false),
                        ThanhTien = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ChiTietId)
                .ForeignKey("dbo.DonHangs", t => t.DonHangId, cascadeDelete: true)
                .ForeignKey("dbo.MonAns", t => t.MonAnId)
                .Index(t => t.DonHangId)
                .Index(t => t.MonAnId);
            
            CreateTable(
                "dbo.DonHangs",
                c => new
                    {
                        MaDonHang = c.Int(nullable: false, identity: true),
                        NgayDat = c.DateTime(nullable: false),
                        TenKhachHang = c.String(nullable: false, maxLength: 100),
                        DiaChi = c.String(maxLength: 200),
                        SoDienThoai = c.String(maxLength: 15),
                        TongTien = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TrangThai = c.String(),
                        MaHTTT = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaDonHang)
                .ForeignKey("dbo.HinhThucThanhToans", t => t.MaHTTT)
                .Index(t => t.MaHTTT);
            
            CreateTable(
                "dbo.HinhThucThanhToans",
                c => new
                    {
                        MaHTTT = c.Int(nullable: false, identity: true),
                        TenHinhThuc = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.MaHTTT);
            
            CreateTable(
                "dbo.MonAns",
                c => new
                    {
                        MonAnId = c.Int(nullable: false, identity: true),
                        TenMon = c.String(nullable: false, maxLength: 150),
                        Gia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HinhAnh = c.String(),
                        DanhMucId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MonAnId)
                .ForeignKey("dbo.Danhmucs", t => t.DanhMucId)
                .Index(t => t.DanhMucId);
            
            CreateTable(
                "dbo.Danhmucs",
                c => new
                    {
                        DanhMucId = c.Int(nullable: false, identity: true),
                        TenDanhMuc = c.String(nullable: false, maxLength: 100),
                        HinhAnh = c.String(),
                    })
                .PrimaryKey(t => t.DanhMucId);
            
            CreateTable(
                "dbo.HoaDons",
                c => new
                    {
                        MaHoaDon = c.Int(nullable: false, identity: true),
                        MaDonHang = c.Int(nullable: false),
                        MaHTTT = c.Int(nullable: false),
                        NgayThanhToan = c.DateTime(nullable: false),
                        TongTien = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.MaHoaDon)
                .ForeignKey("dbo.DonHangs", t => t.MaDonHang)
                .ForeignKey("dbo.HinhThucThanhToans", t => t.MaHTTT)
                .Index(t => t.MaDonHang)
                .Index(t => t.MaHTTT);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HoaDons", "MaHTTT", "dbo.HinhThucThanhToans");
            DropForeignKey("dbo.HoaDons", "MaDonHang", "dbo.DonHangs");
            DropForeignKey("dbo.ChiTietDonHangs", "MonAnId", "dbo.MonAns");
            DropForeignKey("dbo.MonAns", "DanhMucId", "dbo.Danhmucs");
            DropForeignKey("dbo.ChiTietDonHangs", "DonHangId", "dbo.DonHangs");
            DropForeignKey("dbo.DonHangs", "MaHTTT", "dbo.HinhThucThanhToans");
            DropIndex("dbo.HoaDons", new[] { "MaHTTT" });
            DropIndex("dbo.HoaDons", new[] { "MaDonHang" });
            DropIndex("dbo.MonAns", new[] { "DanhMucId" });
            DropIndex("dbo.DonHangs", new[] { "MaHTTT" });
            DropIndex("dbo.ChiTietDonHangs", new[] { "MonAnId" });
            DropIndex("dbo.ChiTietDonHangs", new[] { "DonHangId" });
            DropTable("dbo.HoaDons");
            DropTable("dbo.Danhmucs");
            DropTable("dbo.MonAns");
            DropTable("dbo.HinhThucThanhToans");
            DropTable("dbo.DonHangs");
            DropTable("dbo.ChiTietDonHangs");
        }
    }
}
