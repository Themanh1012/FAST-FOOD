using FAST_FOOD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FAST_FOOD.Controllers
{
    public class TinTucController : Controller
    {
        private List<TinTuc> GetMockArticles()
        {
            // Dữ liệu mẫu (sử dụng link ảnh KFC giả định)
            return new List<TinTuc>
            {
                new TinTuc { Id = 1, TieuDe = "KFC Ra Mắt Gà Giòn Sốt Trứng Muối Mới", TomTat = "Món Gà Sốt Trứng Muối với hương vị đậm đà, béo ngậy hứa hẹn là món ăn gây bão mùa hè này...",
                    NgayDang = DateTime.Parse("2025-10-15"), HinhAnh = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQWV6cKJCQCZ2OYsEEoLfeB26hlkjsc0WbKIkzKdZ_kp7D1h77ksdj1yQ2fo8QZnALEQVk&usqp=CAU", Slug = "ga-sot-trung-muoi" },
                new TinTuc { Id = 2, TieuDe = "Ưu Đãi Đặc Biệt: Mua 1 Tặng 1 Burger Zinger", TomTat = "Cơ hội thưởng thức món Burger Zinger lừng danh với giá cực kỳ ưu đãi, chỉ áp dụng trong tháng 11.",
                    NgayDang = DateTime.Parse("2025-11-01"), HinhAnh = "https://globalprimenews.com/wp-content/uploads/2020/10/IMG-20201031-WA0004.jpg", Slug = "mua-1-tang-1-burger" },
                new TinTuc { Id = 3, TieuDe = "KFC Chính Thức Cung Cấp Dịch Vụ Đặt Tiệc Tại Nhà", TomTat = "Dịch vụ đặt tiệc mới giúp bạn tổ chức các buổi liên hoan, sinh nhật dễ dàng với thực đơn phong phú từ KFC.",
                    NgayDang = DateTime.Parse("2025-09-20"), HinhAnh = "https://static.kfcvietnam.com.vn/images/email/TUNG-BUNG.jpg", Slug = "dich-vu-dat-tiec-tai-nha" },
            };
        }

        public ActionResult TinTuc()
        {
            var articles = GetMockArticles().OrderByDescending(a => a.NgayDang).ToList();
            return View(articles);
        }

        public ActionResult ChiTiet(string slug)
        {
            var article = GetMockArticles().FirstOrDefault(a => a.Slug == slug);
            if (article == null) return HttpNotFound();

            // Nội dung chi tiết giả lập
            article.NoiDungChiTiet = "Chi tiết về món ăn mới được làm từ gà tươi tẩm ướp 11 loại gia vị bí mật, sau đó được phủ lớp sốt trứng muối đặc biệt và chiên giòn hoàn hảo. Hãy đến cửa hàng gần nhất hoặc đặt hàng online để thưởng thức ngay hôm nay!";

            return View(article);
        }
    }
}