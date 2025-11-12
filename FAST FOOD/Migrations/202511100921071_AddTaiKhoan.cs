namespace FAST_FOOD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaiKhoan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.accounts",
                c => new
                    {
                        MaTK = c.Int(nullable: false, identity: true),
                        TenDangNhap = c.String(nullable: false, maxLength: 100),
                        MatKhau = c.String(nullable: false, maxLength: 255),
                        VaiTro = c.String(nullable: false, maxLength: 50),
                        HoTen = c.String(maxLength: 100),
                        Email = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.MaTK);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.accounts");
        }
    }
}
