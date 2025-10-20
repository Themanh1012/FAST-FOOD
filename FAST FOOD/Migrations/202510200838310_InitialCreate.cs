namespace FAST_FOOD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
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
                .ForeignKey("dbo.MonAns", t => t.MonAnId, cascadeDelete: true)
                .Index(t => t.DonHangId)
                .Index(t => t.MonAnId);
            
            CreateTable(
                "dbo.DonHangs",
                c => new
                    {
                        DonHangId = c.Int(nullable: false, identity: true),
                        NgayDat = c.DateTime(nullable: false),
                        TenKhachHang = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.DonHangId);
            
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
                .ForeignKey("dbo.Danhmucs", t => t.DanhMucId, cascadeDelete: true)
                .Index(t => t.DanhMucId);
            
            CreateTable(
                "dbo.Danhmucs",
                c => new
                    {
                        DanhMucId = c.Int(nullable: false, identity: true),
                        TenDanhMuc = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.DanhMucId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChiTietDonHangs", "MonAnId", "dbo.MonAns");
            DropForeignKey("dbo.MonAns", "DanhMucId", "dbo.Danhmucs");
            DropForeignKey("dbo.ChiTietDonHangs", "DonHangId", "dbo.DonHangs");
            DropIndex("dbo.MonAns", new[] { "DanhMucId" });
            DropIndex("dbo.ChiTietDonHangs", new[] { "MonAnId" });
            DropIndex("dbo.ChiTietDonHangs", new[] { "DonHangId" });
            DropTable("dbo.Danhmucs");
            DropTable("dbo.MonAns");
            DropTable("dbo.DonHangs");
            DropTable("dbo.ChiTietDonHangs");
        }
    }
}
