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

        public KhachHangsController(ApplicationDbContext db) => _db = db;

        // GET: GianHangs
        public IActionResult Index(string? search)
        {
            // Lưu từ khóa tìm kiếm vào ViewData để hiển thị lại trên form
            ViewData["CurrentFilter"] = search;
            // Gọi phương thức GetData với tham số search. 
            // Phương thức này sẽ gọi Stored Procedure đã sửa của bạn.
            var list = _db.KhachHang_GetAll(search);
            return View(list);
        }

        // GET: GianHangs/Create
        public IActionResult Create()
        {
            // Lấy dữ liệu xã, đảm bảo không trả về null bằng toán tử ??
            var xaList = _db.Xa_GetAll() ?? new List<Xa>();

            ViewBag.Xas = new SelectList(xaList, "MaXa", "TenXa");
            return View();
        }

        [HttpPost]
        //public IActionResult Create(KhachHang kh)
        //{
        //    _db.KhachHang_Insert(kh);
        //    return RedirectToAction(nameof(Index));
        //}
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

        // GET: GianHangs/Edit/5
        public IActionResult Edit(int id)
        {
            var kh = _db.KhachHang_GetById(id);
            if (kh == null) return NotFound();

            var xaList = _db.Xa_GetAll() ?? new List<Xa>();

            // Load danh sách Xã, và chọn MaTinh hiện tại của xã làm giá trị mặc định
            ViewBag.Xas = new SelectList(xaList, "MaXa", "TenXa", kh.MaXa);
            return View(kh);
        }

        //[HttpPost]
        //public IActionResult Edit(KhachHang kh)
        //{
        //    _db.KhachHang_Update(kh);
        //    return RedirectToAction(nameof(Index));
        //}
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
