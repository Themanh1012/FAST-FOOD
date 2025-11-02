namespace FAST_FOOD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChiTietDonHangs", "MonAnId", "dbo.MonAns");
            DropForeignKey("dbo.HoaDons", "MaDonHang", "dbo.DonHangs");
            AddColumn("dbo.DonHangs", "DiaChi", c => c.String(maxLength: 200));
            AddColumn("dbo.DonHangs", "SoDienThoai", c => c.String(maxLength: 15));
            AddColumn("dbo.DonHangs", "TongTien", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.DonHangs", "TrangThai", c => c.String());
            AddColumn("dbo.DonHangs", "MaHTTT", c => c.Int());
            AlterColumn("dbo.HinhThucThanhToans", "TenHinhThuc", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.DonHangs", "MaHTTT");
            AddForeignKey("dbo.DonHangs", "MaHTTT", "dbo.HinhThucThanhToans", "MaHTTT");
            AddForeignKey("dbo.ChiTietDonHangs", "MonAnId", "dbo.MonAns", "MonAnId");
            AddForeignKey("dbo.HoaDons", "MaDonHang", "dbo.DonHangs", "DonHangId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HoaDons", "MaDonHang", "dbo.DonHangs");
            DropForeignKey("dbo.ChiTietDonHangs", "MonAnId", "dbo.MonAns");
            DropForeignKey("dbo.DonHangs", "MaHTTT", "dbo.HinhThucThanhToans");
            DropIndex("dbo.DonHangs", new[] { "MaHTTT" });
            AlterColumn("dbo.HinhThucThanhToans", "TenHinhThuc", c => c.String());
            DropColumn("dbo.DonHangs", "MaHTTT");
            DropColumn("dbo.DonHangs", "TrangThai");
            DropColumn("dbo.DonHangs", "TongTien");
            DropColumn("dbo.DonHangs", "SoDienThoai");
            DropColumn("dbo.DonHangs", "DiaChi");
            AddForeignKey("dbo.HoaDons", "MaDonHang", "dbo.DonHangs", "DonHangId", cascadeDelete: true);
            AddForeignKey("dbo.ChiTietDonHangs", "MonAnId", "dbo.MonAns", "MonAnId", cascadeDelete: true);
        }
    }
}
