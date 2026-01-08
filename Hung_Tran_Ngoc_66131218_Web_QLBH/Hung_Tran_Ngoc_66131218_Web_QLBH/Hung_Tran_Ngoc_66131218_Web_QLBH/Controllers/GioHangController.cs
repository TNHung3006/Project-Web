using Hung_Tran_Ngoc_66131218_Web_QLBH.Data;
using Hung_Tran_Ngoc_66131218_Web_QLBH.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json; // Cần cài gói này hoặc dùng System.Text.Json

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
            // Lưu xong thì quay lại trang vừa đứng (Trang Shop hoặc Trang Chi tiết)
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

        // --- CÁC HÀM HỖ TRỢ LƯU SESSION (QUAN TRỌNG) ---

        // Lấy danh sách từ Session
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

        // Lưu danh sách vào Session
        private void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString("ShopCart", jsoncart);
        }
    }
}