namespace FAST_FOOD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDangki : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.accounts", "VaiTro", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.accounts", "VaiTro", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
