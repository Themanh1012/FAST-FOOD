namespace FAST_FOOD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateKFCModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MonAns", "DanhMucId", "dbo.Danhmucs");
            AddForeignKey("dbo.MonAns", "DanhMucId", "dbo.Danhmucs", "DanhMucId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MonAns", "DanhMucId", "dbo.Danhmucs");
            AddForeignKey("dbo.MonAns", "DanhMucId", "dbo.Danhmucs", "DanhMucId", cascadeDelete: true);
        }
    }
}
