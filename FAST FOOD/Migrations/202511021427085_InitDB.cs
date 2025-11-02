namespace FAST_FOOD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Danhmucs", "HinhAnh", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Danhmucs", "HinhAnh");
        }
    }
}
