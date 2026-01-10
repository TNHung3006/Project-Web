using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hung_Tran_Ngoc_66131218_Web_QLBH.Data;
using Hung_Tran_Ngoc_66131218_Web_QLBH.Models;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Controllers
{
    public class KhachHangsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;//Khai báo biến môi trường để upload ảnh lên web server

        public KhachHangsController(ApplicationDbContext db, IWebHostEnvironment env) { _db = db; _env = env; }

        // GET: KhacHangs
        public IActionResult Index(string? search)
        {
            // Lưu từ khóa tìm kiếm vào ViewData để hiển thị lại trên form
            ViewData["CurrentFilter"] = search;

            var list = _db.KhachHang_GetAll(search);

            // --- BỔ SUNG: Gán dữ liệu cho [NotMapped] DiaChiFull ---
            // 1. Lấy danh sách Xã và Tỉnh để tra cứu
            var listXa = _db.Xa_GetAll();
            var listTinh = _db.Tinh_GetAll();

            foreach (var kh in list)
            {
                // Tìm Xã của khách hàng
                var xa = listXa.FirstOrDefault(x => x.MaXa == kh.MaXa);
                // Tìm Tỉnh dựa trên Xã đó
                var tinh = xa != null ? listTinh.FirstOrDefault(t => t.MaTinh == xa.MaTinh) : null;

                // Ghép chuỗi địa chỉ: "Số nhà, Xã ..., Tỉnh ..."
                string tenXa = xa?.TenXa ?? "";
                string tenTinh = tinh?.TenTinh ?? "";
                string diaChiCu = kh.DiaChi ?? "";

                // Format đẹp: Loại bỏ các dấu phẩy dư thừa nếu dữ liệu trống
                var parts = new List<string> { diaChiCu, tenXa, tenTinh };
                // Lọc bỏ các chuỗi rỗng và nối lại bằng dấu phẩy
                kh.DiaChiFull = string.Join(", ", parts.Where(s => !string.IsNullOrEmpty(s)));
            }
            // -------------------------------------------------------

            return View(list);
        }

        // GET: KhachHangs/Create
        public IActionResult Create()
        {
            // 1. Lấy danh sách Tỉnh để hiển thị dropdown đầu tiên
            var listTinh = _db.Tinh_GetAll() ?? new List<Tinh>();
            ViewBag.Tinhs = new SelectList(listTinh, "MaTinh", "TenTinh");

            // 2. Khởi tạo danh sách Xã rỗng (vì chưa chọn Tỉnh nào)
            ViewBag.Xas = new SelectList(new List<Xa>(), "MaXa", "TenXa");

            return View();
        }
        // POST: KhacHangs/Create
        [HttpPost]
        public async Task<IActionResult> Create(KhachHang kh, IFormFile? image)
        {

            if (image != null && image.Length > 0)//Nếu có chọn ảnh để upload
            {
                var uploads = Path.Combine(_env.WebRootPath, "images", "uploads");
                Directory.CreateDirectory(uploads);//Tạo thư mục nếu nó chưa tồn tại
                var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);
                var path = Path.Combine(uploads, fileName);
                using var fs = new FileStream(path, FileMode.Create);
                await image.CopyToAsync(fs);
                kh.AnhKH = fileName;
            }

            var newMaKH = _db.KhachHang_Insert(kh);
            return RedirectToAction(nameof(Index));
        }

        // 1. Thêm Action này để Ajax gọi lấy danh sách xã
        [HttpGet]
        public IActionResult GetXaByTinh(int maTinh)
        {
            var allXas = _db.Xa_GetAll();
            var xas = allXas.Where(x => x.MaTinh == maTinh).ToList();
            return Json(xas);
        }

        // GET: KhacHangs/Edit/5
        public IActionResult Edit(int id)
        {
            var kh = _db.KhachHang_GetById(id);
            if (kh == null) return NotFound();

            // A. Lấy tất cả Tỉnh để đổ vào Dropdown Tỉnh
            var listTinh = _db.Tinh_GetAll() ?? new List<Tinh>();

            // B. Xác định Tỉnh hiện tại của Nhà cung cấp (để chọn sẵn khi mở form)
            var currentXa = _db.Xa_GetAll().FirstOrDefault(x => x.MaXa == kh.MaXa);
            int selectedMaTinh = currentXa != null ? currentXa.MaTinh : 0;

            // C. Lấy danh sách Xã thuộc Tỉnh hiện tại (chứ không lấy hết tất cả xã)
            var listXaOfTinh = _db.Xa_GetAll().Where(x => x.MaTinh == selectedMaTinh).ToList();

            // D. Truyền dữ liệu qua View
            ViewBag.Tinhs = new SelectList(listTinh, "MaTinh", "TenTinh", selectedMaTinh);
            ViewBag.Xas = new SelectList(listXaOfTinh, "MaXa", "TenXa", kh.MaXa);
            return View(kh);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(KhachHang kh, IFormFile? image, string? existingAnh)
        {
            var uploads = Path.Combine(_env.WebRootPath, "images", "uploads");
            Directory.CreateDirectory(uploads);

            if (image != null && image.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);
                var path = Path.Combine(uploads, fileName);
                using var fs = new FileStream(path, FileMode.Create);
                await image.CopyToAsync(fs);

                // xóa file cũ nếu có
                if (!string.IsNullOrEmpty(existingAnh))
                {
                    var old = Path.Combine(uploads, existingAnh);
                    if (System.IO.File.Exists(old)) System.IO.File.Delete(old);
                }
                kh.AnhKH = fileName;
            }
            else
            {
                // giữ ảnh cũ
                kh.AnhKH = existingAnh;
            }

            _db.KhachHang_Update(kh);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var kh = _db.KhachHang_GetById(id);
            if (kh == null) return NotFound();

            // --- BỔ SUNG: Hiển thị DiaChiFull trong Details ---
            var xa = _db.Xa_GetAll().FirstOrDefault(x => x.MaXa == kh.MaXa);
            var tinh = xa != null ? _db.Tinh_GetAll().FirstOrDefault(t => t.MaTinh == xa.MaTinh) : null;

            string tenXa = xa?.TenXa ?? "";
            string tenTinh = tinh?.TenTinh ?? "";
            string diaChiCu = kh.DiaChi ?? "";

            var parts = new List<string> { diaChiCu, tenXa, tenTinh };
            kh.DiaChiFull = string.Join(", ", parts.Where(s => !string.IsNullOrEmpty(s)));
            // -------------------------------------------------

            return View(kh);
        }

        public IActionResult Delete(int id)
        {
            var kh = _db.KhachHang_GetById(id);
            if (kh == null) return NotFound();

            // --- BỔ SUNG: Hiển thị DiaChiFull trong Delete để xác nhận ---
            var xa = _db.Xa_GetAll().FirstOrDefault(x => x.MaXa == kh.MaXa);
            var tinh = xa != null ? _db.Tinh_GetAll().FirstOrDefault(t => t.MaTinh == xa.MaTinh) : null;

            string tenXa = xa?.TenXa ?? "";
            string tenTinh = tinh?.TenTinh ?? "";
            string diaChiCu = kh.DiaChi ?? "";

            var parts = new List<string> { diaChiCu, tenXa, tenTinh };
            kh.DiaChiFull = string.Join(", ", parts.Where(s => !string.IsNullOrEmpty(s)));
            // -------------------------------------------------------------

            return View(kh);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var kh = _db.KhachHang_GetById(id);
            if (kh != null && !string.IsNullOrEmpty(kh.AnhKH))
            {
                var uploads = Path.Combine(_env.WebRootPath, "images", "uploads");
                var file = Path.Combine(uploads, kh.AnhKH);
                if (System.IO.File.Exists(file)) System.IO.File.Delete(file);
            }
            _db.KhachHang_Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}