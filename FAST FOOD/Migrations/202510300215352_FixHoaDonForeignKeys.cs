namespace FAST_FOOD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixHoaDonForeignKeys : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HinhThucThanhToans",
                c => new
                    {
                        MaHTTT = c.Int(nullable: false, identity: true),
                        TenHinhThuc = c.String(),
                    })
                .PrimaryKey(t => t.MaHTTT);
            
            CreateTable(
                "dbo.HoaDons",
                c => new
                    {
                        MaHoaDon = c.Int(nullable: false, identity: true),
                        MaDonHang = c.Int(nullable: false),
                        MaNV = c.Int(nullable: false),
                        MaHTTT = c.Int(nullable: false),
                        NgayThanhToan = c.DateTime(nullable: false),
                        TongTien = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.MaHoaDon)
                .ForeignKey("dbo.DonHangs", t => t.MaDonHang, cascadeDelete: true)
                .ForeignKey("dbo.HinhThucThanhToans", t => t.MaHTTT, cascadeDelete: true)
                .ForeignKey("dbo.Nhanviens", t => t.MaNV, cascadeDelete: true)
                .Index(t => t.MaDonHang)
                .Index(t => t.MaNV)
                .Index(t => t.MaHTTT);
            
            CreateTable(
                "dbo.Nhanviens",
                c => new
                    {
                        MaNV = c.Int(nullable: false, identity: true),
                        HoTen = c.String(),
                        SoDienThoai = c.String(),
                        ChucVu = c.String(),
                        Luong = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.MaNV);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HoaDons", "MaNV", "dbo.Nhanviens");
            DropForeignKey("dbo.HoaDons", "MaHTTT", "dbo.HinhThucThanhToans");
            DropForeignKey("dbo.HoaDons", "MaDonHang", "dbo.DonHangs");
            DropIndex("dbo.HoaDons", new[] { "MaHTTT" });
            DropIndex("dbo.HoaDons", new[] { "MaNV" });
            DropIndex("dbo.HoaDons", new[] { "MaDonHang" });
            DropTable("dbo.Nhanviens");
            DropTable("dbo.HoaDons");
            DropTable("dbo.HinhThucThanhToans");
        }
    }
}
