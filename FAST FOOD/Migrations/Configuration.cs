namespace FAST_FOOD.Migrations
{
    using FAST_FOOD.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using BCrypt.Net;
    using BC = BCrypt.Net.BCrypt;

    internal sealed class Configuration : DbMigrationsConfiguration<FAST_FOOD.Models.KFCContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FAST_FOOD.Models.KFCContext context)
        {
            if (!context.accounts.Any(a => a.TenDangNhap == "admin"))
            {
                var admin = new account
                {
                    TenDangNhap = "admin",
                    MatKhau = BC.HashPassword("@Admin123"),
                    VaiTro = "Admin",
                    HoTen = "Quản Trị Viên",
                    Email = "admin123@gmail.com"

                };
                context.accounts.Add(admin);
                context.SaveChanges();
            }
        }
    }
}
