namespace FAST_FOOD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DonHangs", "account_MaTK", c => c.Int());
            AddColumn("dbo.DatTiecs", "MaTK", c => c.Int(nullable: false));
            AddColumn("dbo.TinTucs", "CreatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.TinTucs", "Account_MaTK", c => c.Int());
            CreateIndex("dbo.DatTiecs", "MaTK");
            CreateIndex("dbo.DonHangs", "account_MaTK");
            CreateIndex("dbo.TinTucs", "Account_MaTK");
            AddForeignKey("dbo.DatTiecs", "MaTK", "dbo.accounts", "MaTK", cascadeDelete: true);
            AddForeignKey("dbo.DonHangs", "account_MaTK", "dbo.accounts", "MaTK");
            AddForeignKey("dbo.TinTucs", "Account_MaTK", "dbo.accounts", "MaTK");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TinTucs", "Account_MaTK", "dbo.accounts");
            DropForeignKey("dbo.DonHangs", "account_MaTK", "dbo.accounts");
            DropForeignKey("dbo.DatTiecs", "MaTK", "dbo.accounts");
            DropIndex("dbo.TinTucs", new[] { "Account_MaTK" });
            DropIndex("dbo.DonHangs", new[] { "account_MaTK" });
            DropIndex("dbo.DatTiecs", new[] { "MaTK" });
            DropColumn("dbo.TinTucs", "Account_MaTK");
            DropColumn("dbo.TinTucs", "CreatedBy");
            DropColumn("dbo.DatTiecs", "MaTK");
            DropColumn("dbo.DonHangs", "account_MaTK");
        }
    }
}
