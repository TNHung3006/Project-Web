using Hung_Tran_Ngoc_66131218_Web_QLBH.Data;
using Hung_Tran_Ngoc_66131218_Web_QLBH.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Controllers
{
    public class GioHangController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GioHangController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Trang hiển thị giỏ hàng
        public IActionResult Index()
        {
            var cart = GetCartItems();
            return View(cart);
        }

        // Hàm thêm vào giỏ hàng
        public IActionResult ThemVaoGio(int maSP)
        {
            var cart = GetCartItems();
            var item = cart.FirstOrDefault(p => p.MaSP == maSP);

            if (item == null)
            {
                // Nếu chưa có thì thêm mới
                var sp = _context.sp.FirstOrDefault(p => p.MaSP == maSP);
                if (sp == null) return NotFound();

                item = new CartItem
                {
                    MaSP = sp.MaSP,
                    TenSP = sp.TenSP,
                    DonGia = sp.DonGia,
                    AnhSP = sp.AnhSP,
                    SoLuong = 1
                };
                cart.Add(item);
            }
            else
            {
                // Nếu có rồi thì tăng số lượng
                item.SoLuong++;
            }

            SaveCartSession(cart);
            // Lưu xong thì quay lại trang vừa đứng
            return Redirect(Request.Headers["Referer"].ToString());
        }

        // Hàm xóa khỏi giỏ hàng
        public IActionResult XoaKhoiGio(int maSP)
        {
            var cart = GetCartItems();
            var item = cart.FirstOrDefault(p => p.MaSP == maSP);
            if (item != null)
            {
                cart.Remove(item);
                SaveCartSession(cart);
            }
            return RedirectToAction(nameof(Index));
        }

        // --- HÀM CẬP NHẬT SỐ LƯỢNG (MỚI THÊM) ---
        public IActionResult CapNhatSoLuong(int maSP, int soLuong)
        {
            var cart = GetCartItems();
            var item = cart.FirstOrDefault(p => p.MaSP == maSP);

            if (item != null)
            {
                // Nếu số lượng > 0 thì cập nhật
                if (soLuong > 0)
                {
                    item.SoLuong = soLuong;
                }
                else
                {
                    // Nếu giảm về 0 thì xóa luôn sản phẩm
                    cart.Remove(item);
                }

                // Lưu lại vào Session
                SaveCartSession(cart);
            }

            // Load lại trang giỏ hàng
            return RedirectToAction("Index");
        }

        // =============================================================
        // HÀM THANH TOÁN (CHECKOUT)
        // =============================================================
        public IActionResult Checkout()
        {
            // 1. Kiểm tra đăng nhập
            var maKH = HttpContext.Session.GetInt32("MaKH");
            if (maKH == null)
            {
                return RedirectToAction("LoginCustomer", "Account");
            }

            // 2. Kiểm tra giỏ hàng
            var cart = GetCartItems();
            if (cart.Count == 0)
            {
                return RedirectToAction("Index");
            }

            // 3. Lấy thông tin khách hàng để lấy địa chỉ
            var khachHang = _context.kh.FirstOrDefault(k => k.MaKH == maKH);

            // 4. Tạo đối tượng Đơn Bán Hàng
            var donHang = new DonBanHang
            {
                MaKH = maKH.Value,
                NgayBan = DateTime.Now,
                DiaChiGH = khachHang?.DiaChi ?? "Chưa cung cấp",
                MaXa = khachHang?.MaXa ?? 1,
                MaTTDBH = 1
            };

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // A. Lưu Đơn hàng
                    _context.DonBanHang_Insert(donHang);

                    // B. Lấy lại Mã Đơn Hàng vừa tạo
                    var newDonHang = _context.dbh
                                        .Where(d => d.MaKH == maKH)
                                        .OrderByDescending(d => d.MaDBH)
                                        .FirstOrDefault();

                    if (newDonHang != null)
                    {
                        // C. Lưu từng món trong giỏ vào bảng Chi Tiết (CTBH)
                        foreach (var item in cart)
                        {
                            var ctbh = new CTBH
                            {
                                MaDBH = newDonHang.MaDBH,
                                MaSP = item.MaSP,
                                SLB = item.SoLuong,
                                DGB = item.DonGia
                            };
                            _context.CTBH_Insert(ctbh);
                        }

                        // D. Xóa giỏ hàng trong Session
                        HttpContext.Session.Remove("ShopCart");

                        // E. Xác nhận giao dịch thành công
                        transaction.Commit();

                        return RedirectToAction("MyOrders", "Account");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Content("Có lỗi xảy ra khi thanh toán: " + ex.Message);
                }
            }

            return RedirectToAction("Index");
        }


        // --- CÁC HÀM HỖ TRỢ SESSION ---
        private List<CartItem> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString("ShopCart");
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }

        private void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString("ShopCart", jsoncart);
        }
    }
}