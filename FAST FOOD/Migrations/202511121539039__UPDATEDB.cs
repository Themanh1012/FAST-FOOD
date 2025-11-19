namespace FAST_FOOD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _UPDATEDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HoaDons", "MaHTTT", "dbo.HinhThucThanhToans");
            DropIndex("dbo.DonHangs", new[] { "MaHTTT" });
            AlterColumn("dbo.DonHangs", "MaHTTT", c => c.Int(nullable: false));
            CreateIndex("dbo.DonHangs", "MaHTTT");
            AddForeignKey("dbo.HoaDons", "MaHTTT", "dbo.HinhThucThanhToans", "MaHTTT");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HoaDons", "MaHTTT", "dbo.HinhThucThanhToans");
            DropIndex("dbo.DonHangs", new[] { "MaHTTT" });
            AlterColumn("dbo.DonHangs", "MaHTTT", c => c.Int());
            CreateIndex("dbo.DonHangs", "MaHTTT");
            AddForeignKey("dbo.HoaDons", "MaHTTT", "dbo.HinhThucThanhToans", "MaHTTT", cascadeDelete: true);
        }
    }
}
