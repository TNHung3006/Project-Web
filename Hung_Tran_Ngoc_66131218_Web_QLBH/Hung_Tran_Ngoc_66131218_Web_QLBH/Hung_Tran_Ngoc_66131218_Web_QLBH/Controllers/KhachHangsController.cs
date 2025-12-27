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
            // Gọi phương thức GetData với tham số search. 
            // Phương thức này sẽ gọi Stored Procedure đã sửa của bạn.
            var list = _db.KhachHang_GetAll(search);
            return View(list);
        }

        // GET: KhachHangs/Create
        public IActionResult Create()
        {
            // 1. Lấy danh sách Tỉnh để hiển thị dropdown đầu tiên
            var listTinh = _db.Tinh_GetAll() ?? new List<Tinh>();
            ViewBag.Tinhs = new SelectList(listTinh, "MaTinh", "TenTinh");

            // 2. Khởi tạo danh sách Xã rỗng (vì chưa chọn Tỉnh nào)
            // Hoặc có thể để null, nhưng new List<Xa>() sẽ an toàn hơn cho SelectList
            // nhưng trong trường hợp này new lish<Xa> sẽ không đảm bảo được việc chọn tỉnh trước rồi chọn xã sau.
            // vì nó sẽ hiện ra toàn bộ xã nếu new lish<xa>
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

            var newMaKH = _db.KhachHang_Insert(kh); // trả MaSP
            // nếu cần hiển thị newMaSP, có thể gửi ViewBag.NewMaSP = newMaSP
            return RedirectToAction(nameof(Index));
        }

        // 1. Thêm Action này để Ajax gọi lấy danh sách xã
        [HttpGet]
        public IActionResult GetXaByTinh(int maTinh)
        {
            // Giả sử Xa_GetAll trả về List và trong model Xa có thuộc tính MaTinh
            // Bạn cần lọc danh sách xã theo maTinh được gửi lên
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
            // Giả sử bạn có hàm _db.Tinh_GetAll(), nếu chưa có bạn phải tạo thêm
            var listTinh = _db.Tinh_GetAll() ?? new List<Tinh>();

            // B. Xác định Tỉnh hiện tại của Nhà cung cấp (để chọn sẵn khi mở form)
            // Chúng ta phải tìm xem MaXa hiện tại thuộc MaTinh nào.
            // Cách làm: Tìm thông tin xã hiện tại -> lấy MaTinh của nó
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
            return View(kh);
        }

        public IActionResult Delete(int id)
        {
            var kh = _db.KhachHang_GetById(id);
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