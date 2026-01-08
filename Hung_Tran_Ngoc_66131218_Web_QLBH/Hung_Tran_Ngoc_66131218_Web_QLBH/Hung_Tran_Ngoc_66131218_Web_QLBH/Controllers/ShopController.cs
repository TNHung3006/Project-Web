using Hung_Tran_Ngoc_66131218_Web_QLBH.Data;
using Hung_Tran_Ngoc_66131218_Web_QLBH.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Trang chủ cửa hàng
        // Thêm tham số searchString vào hàm Index
        public IActionResult Index(string searchString)
        {
            // 1. Tạo câu truy vấn cơ bản
            var query = _context.sp.AsQueryable();

            // 2. Nếu có từ khóa tìm kiếm -> Lọc theo Tên sản phẩm
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.TenSP.Contains(searchString));
            }

            // 3. Thực thi truy vấn lấy danh sách
            var sanPhams = query.ToList();

            // 4. (Giữ nguyên logic cũ) Lấy tên loại sản phẩm thủ công
            var loaiSPs = _context.lsp.ToList();
            foreach (var item in sanPhams)
            {
                var loai = loaiSPs.FirstOrDefault(l => l.MaLSP == item.MaLSP);
                item.LoaiSP = loai?.TenLSP ?? "Khác";
            }

            // 5. Lưu lại từ khóa để hiển thị lại trên ô tìm kiếm (trải nghiệm người dùng)
            ViewData["CurrentFilter"] = searchString;

            return View(sanPhams);
        }

        // Trang chi tiết
        public IActionResult Details(int id)
        {
            var sanPham = _context.sp.FirstOrDefault(m => m.MaSP == id);

            if (sanPham == null) return NotFound();

            // Lấy tên loại thủ công
            var loai = _context.lsp.FirstOrDefault(l => l.MaLSP == sanPham.MaLSP);
            sanPham.LoaiSP = loai?.TenLSP ?? "Khác";

            return View(sanPham);
        }
    }
}