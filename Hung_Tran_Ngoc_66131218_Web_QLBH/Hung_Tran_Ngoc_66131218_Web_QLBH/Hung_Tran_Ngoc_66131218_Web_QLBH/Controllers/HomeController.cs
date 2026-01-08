using Hung_Tran_Ngoc_66131218_Web_QLBH.Models;
using Hung_Tran_Ngoc_66131218_Web_QLBH.Data; // QUAN TRỌNG: Phải có dòng này để nhận diện ApplicationDbContext
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq; // Thêm thư viện này để dùng Count()

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // 1. Sửa tên Context thành ApplicationDbContext
        private readonly ApplicationDbContext _context;

        // 2. Inject Context vào constructor
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Sản phẩm
            ViewBag.SoLuongSanPham = _context.sp != null ? _context.sp.Count() : 0;

            // Khách hàng
            ViewBag.SoLuongKhachHang = _context.kh != null ? _context.kh.Count() : 0;

            // Đơn bán
            ViewBag.SoLuongDonBan = _context.dbh != null ? _context.dbh.Count() : 0;

            // Đơn mua
            ViewBag.SoLuongDonMua = _context.dmh != null ? _context.dmh.Count() : 0;

            return View();
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