using Microsoft.AspNetCore.Mvc;
using Hung_Tran_Ngoc_66131218_Web_QLBH.Data;
using Microsoft.AspNetCore.Http; // Cần để dùng Session
using System.Linq;
using Hung_Tran_Ngoc_66131218_Web_QLBH.Models;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // /////////////////////////////////////
        // NHÂN VIÊN
        // /////////////////////////////////////
        // GET: Hiển thị trang Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Xử lý khi bấm nút Đăng nhập
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Kiểm tra trong CSDL (Bảng nv - NhanVien)
            var user = _context.nv.FirstOrDefault(x => x.TenDN == username && x.MatKhau == password);

            if (user != null)
            {
                // 1. Lưu thông tin vào Session
                HttpContext.Session.SetString("AdminLogin", user.TenDN);
                HttpContext.Session.SetString("HoTen", user.HoNV + " " + user.TenNV);

                // --- QUAN TRỌNG: Lưu quyền (MaLNV) vào Session ---
                // 1=Admin, 2=Thu Ngân, 3=Kho
                HttpContext.Session.SetInt32("MaLNV", user.MaLNV);
                // -------------------------------------------------

                // 2. Chuyển hướng về trang chủ (Dashboard)
                return RedirectToAction("Index", "Home");
            }

            // Nếu sai thì báo lỗi
            ViewBag.Error = "Thông tin đăng nhập không chính xác!";
            return View();
        }

        // GET: Xem thông tin cá nhân
        public IActionResult Profile()
        {
            // 1. Kiểm tra đăng nhập
            var username = HttpContext.Session.GetString("AdminLogin");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login");
            }

            // 2. Tìm thông tin nhân viên trong Database
            var user = _context.nv.FirstOrDefault(x => x.TenDN == username);

            if (user == null)
            {
                return NotFound();
            }

            // 3. Trả về View kèm dữ liệu nhân viên
            return View(user);
        }

        // Đăng xuất
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Xóa sạch session
            return RedirectToAction("Login");
        }

        // GET: Hiển thị trang đổi mật khẩu
        public IActionResult ChangePassword()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminLogin")))
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        // POST: Xử lý đổi mật khẩu
        [HttpPost]
        public IActionResult ChangePassword(string OldPass, string NewPass, string ConfirmPass)
        {
            var username = HttpContext.Session.GetString("AdminLogin");
            if (string.IsNullOrEmpty(username)) return RedirectToAction("Login");

            // 1. Tìm user hiện tại
            var user = _context.nv.FirstOrDefault(x => x.TenDN == username);

            // 2. Kiểm tra mật khẩu cũ
            if (user.MatKhau != OldPass)
            {
                ViewBag.Error = "Mật khẩu cũ không đúng!";
                return View();
            }

            // 3. Kiểm tra mật khẩu mới
            if (NewPass != ConfirmPass)
            {
                ViewBag.Error = "Xác nhận mật khẩu mới không khớp!";
                return View();
            }

            if (NewPass.Length < 6)
            {
                ViewBag.Error = "Mật khẩu mới phải dài hơn 6 ký tự!";
                return View();
            }

            // 4. Lưu mật khẩu mới
            user.MatKhau = NewPass;
            _context.SaveChanges(); // Lưu vào Database

            ViewBag.Success = "Đổi mật khẩu thành công!";
            return View();
        }

        // ==========================================================
        // PHẦN DÀNH CHO KHÁCH HÀNG (SHOP)
        // ==========================================================

        // 1. Đăng ký Khách hàng (GET)
        public IActionResult Register()
        {
            return View();
        }

        // 2. Xử lý Đăng ký (POST)
        [HttpPost]
        public IActionResult Register(KhachHang kh, string ConfirmPassword)
        {
            if (ModelState.IsValid)
            {
                // A. Kiểm tra tên đăng nhập đã tồn tại chưa
                var checkUser = _context.kh.FirstOrDefault(x => x.TenDN == kh.TenDN);
                if (checkUser != null)
                {
                    ViewBag.Error = "Tên đăng nhập này đã có người dùng!";
                    return View(kh);
                }

                // B. Kiểm tra mật khẩu xác nhận
                if (kh.MatKhau != ConfirmPassword)
                {
                    ViewBag.Error = "Mật khẩu xác nhận không khớp!";
                    return View(kh);
                }

                // C. Gán giá trị mặc định cho các trường thiếu
                kh.MaXa = 1; // Mặc định xã ID = 1 để tránh lỗi khóa ngoại
                kh.AnhKH = "default.jpg"; // Ảnh mặc định

                // D. Lưu khách hàng mới
                _context.kh.Add(kh);
                _context.SaveChanges();

                ViewBag.Success = "Đăng ký thành công! Bạn có thể đăng nhập ngay.";
                return RedirectToAction("LoginCustomer");
            }
            return View(kh);
        }

        // 3. Đăng nhập Khách hàng (GET)
        public IActionResult LoginCustomer()
        {
            return View();
        }

        // 4. Xử lý Đăng nhập Khách hàng (POST)
        [HttpPost]
        public IActionResult LoginCustomer(string username, string password)
        {
            // Tìm trong bảng KhachHang
            var khach = _context.kh.FirstOrDefault(x => x.TenDN == username && x.MatKhau == password);

            if (khach != null)
            {
                // Lưu Session riêng cho Khách (Khác với AdminLogin)
                HttpContext.Session.SetString("KhachHangLogin", khach.TenDN);
                HttpContext.Session.SetInt32("MaKH", khach.MaKH);
                HttpContext.Session.SetString("TenKH", khach.HoKH + " " + khach.TenKH);

                // Quay về trang chủ Shop
                return RedirectToAction("Index", "Shop"); // Hoặc Home/Index tùy route của bạn
            }

            ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu!";
            return View();
        }

        // 5. Đăng xuất Khách hàng
        public IActionResult LogoutCustomer()
        {
            HttpContext.Session.Remove("KhachHangLogin");
            HttpContext.Session.Remove("MaKH");
            HttpContext.Session.Remove("TenKH");
            return RedirectToAction("Index", "Shop"); // Quay về trang chủ
        }

        // 6. XEM VÀ CẬP NHẬT HỒ SƠ (GET & POST)
        public IActionResult ProfileCustomer()
        {
            // Lấy mã khách từ Session
            var maKH = HttpContext.Session.GetInt32("MaKH");
            if (maKH == null) return RedirectToAction("LoginCustomer");

            // Tìm khách hàng trong Database
            var khach = _context.kh.FirstOrDefault(k => k.MaKH == maKH);
            return View(khach);
        }

        [HttpPost]
        public IActionResult ProfileCustomer(KhachHang kh)
        {
            // Logic cập nhật thông tin
            if (ModelState.IsValid)
            {
                var khachCu = _context.kh.FirstOrDefault(k => k.MaKH == kh.MaKH);
                if (khachCu != null)
                {
                    // Cập nhật các trường cho phép sửa
                    khachCu.HoKH = kh.HoKH;
                    khachCu.TenKH = kh.TenKH;
                    khachCu.SDT = kh.SDT;
                    khachCu.DiaChi = kh.DiaChi;

                    _context.SaveChanges();

                    // Cập nhật lại Session tên mới (phòng trường hợp đổi tên)
                    HttpContext.Session.SetString("TenKH", khachCu.HoKH + " " + khachCu.TenKH);

                    ViewBag.Success = "Cập nhật hồ sơ thành công!";
                    return View(khachCu);
                }
            }
            return View(kh);
        }

        // 7. XEM LỊCH SỬ ĐƠN HÀNG
        public IActionResult MyOrders()
        {
            var maKH = HttpContext.Session.GetInt32("MaKH");
            if (maKH == null) return RedirectToAction("LoginCustomer");

            // Lấy danh sách đơn hàng của khách này, sắp xếp mới nhất lên đầu
            var orders = _context.dbh
                            .Where(d => d.MaKH == maKH)
                            .OrderByDescending(d => d.NgayBan)
                            .ToList();

            return View(orders);
        }

        // 8. XEM CHI TIẾT ĐƠN HÀNG (Dùng Model CTBH gốc)
        public IActionResult OrderDetails(int id)
        {
            var maKH = HttpContext.Session.GetInt32("MaKH");
            if (maKH == null) return RedirectToAction("LoginCustomer");

            // 1. Lấy đơn hàng
            var donHang = _context.dbh.FirstOrDefault(d => d.MaDBH == id);

            // Kiểm tra bảo mật
            if (donHang == null || donHang.MaKH != maKH)
            {
                return RedirectToAction("MyOrders");
            }

            // 2. Lấy chi tiết và đổ vào Model CTBH
            var chiTiet = (from ct in _context.ctbh
                           join s in _context.sp on ct.MaSP equals s.MaSP
                           where ct.MaDBH == id
                           select new CTBH
                           {
                               MaDBH = ct.MaDBH,
                               MaSP = ct.MaSP,
                               SLB = ct.SLB,
                               DGB = ct.DGB,
                               ThanhTien = ct.ThanhTien,
                               NgayBan = ct.NgayBan,

                               // Lấy tên và ảnh từ bảng Sản Phẩm
                               TenSP = s.TenSP,
                               AnhSP = s.AnhSP  // Thuộc tính NotMapped vừa thêm
                           }).ToList();

            ViewBag.DonHang = donHang;
            return View(chiTiet);
        }
    }
}