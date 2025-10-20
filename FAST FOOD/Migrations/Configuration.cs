namespace FAST_FOOD.Migrations
{
    using FAST_FOOD.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FAST_FOOD.Models.KFCContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FAST_FOOD.Models.KFCContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            if (!context.Danhmucs.Any())
            {
                context.Danhmucs.Add(new Danhmuc { TenDanhMuc = "Gà rán" });
                context.Danhmucs.Add(new Danhmuc { TenDanhMuc = "Mì ý" });
                context.Danhmucs.Add(new Danhmuc { TenDanhMuc = "Cơm gà" });
                context.SaveChanges();
                    }
            
        }
    }
}
