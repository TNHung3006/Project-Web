using Hung_Tran_Ngoc_66131218_Web_QLBH.Models;
using Hung_Tran_Ngoc_66131218_Web_QLBH.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Http; // Để check Session
using System.Collections.Generic; // Để dùng List

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // --- BẢO MẬT: Kiểm tra Đăng nhập ---
            // Nếu Session trống => Chưa đăng nhập => Chuyển về trang Login
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminLogin")))
            {
                return RedirectToAction("Login", "Account");
            }
            // -----------------------------------

            // Code thống kê cũ của bạn (Giữ nguyên)
            ViewBag.SoLuongSanPham = _context.sp != null ? _context.sp.Count() : 0;
            ViewBag.SoLuongKhachHang = _context.kh != null ? _context.kh.Count() : 0;
            ViewBag.SoLuongDonBan = _context.dbh != null ? _context.dbh.Count() : 0;
            ViewBag.SoLuongDonMua = _context.dmh != null ? _context.dmh.Count() : 0;

            // Code lấy đơn hàng mới (Giữ nguyên logic của bạn)
            var donHangMoi = new List<DonBanHang>();
            if (_context.dbh != null)
            {
                donHangMoi = _context.dbh.OrderByDescending(d => d.NgayBan).Take(5).ToList();
                var listKhachHang = _context.kh.ToList();
                foreach (var item in donHangMoi)
                {
                    var khach = listKhachHang.FirstOrDefault(k => k.MaKH == item.MaKH);
                    item.TenKHFull = khach != null ? (khach.HoKH + " " + khach.TenKH) : "Khách vãng lai";
                }
            }

            return View(donHangMoi);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}