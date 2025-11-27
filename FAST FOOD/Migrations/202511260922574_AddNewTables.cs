namespace FAST_FOOD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DatTiecs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HoTenNguoiDat = c.String(nullable: false),
                        SoDienThoai = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        DiaChiToChuc = c.String(nullable: false),
                        TinhThanhPho = c.String(nullable: false),
                        QuanHuyen = c.String(nullable: false),
                        PhuongXa = c.String(),
                        ThoiGianToChuc = c.DateTime(nullable: false),
                        SoLuongNguoiThamDu = c.String(nullable: false),
                        NgayDat = c.DateTime(nullable: false),
                        TrangThai = c.String(maxLength: 50),
                        DongYDieuKhoan = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TinTucs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TieuDe = c.String(),
                        TomTat = c.String(),
                        NoiDungChiTiet = c.String(),
                        NgayDang = c.DateTime(nullable: false),
                        HinhAnh = c.String(),
                        Slug = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TinTucs");
            DropTable("dbo.DatTiecs");
        }
    }
}
