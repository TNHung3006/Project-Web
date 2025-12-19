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
    public class SanPhamsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;//Khai báo biến môi trường để upload ảnh lên web server
        public SanPhamsController(ApplicationDbContext db, IWebHostEnvironment env) { _db = db; _env = env; }

        public IActionResult Index(string? search)
        {
            // Lưu từ khóa tìm kiếm vào ViewData để hiển thị lại trên form
            ViewData["CurrentFilter"] = search;
            // Gọi phương thức GetData với tham số search. 
            // Phương thức này sẽ gọi Stored Procedure đã sửa của bạn.
            var list = _db.SanPham_GetAll(search);
            return View(list);
        }

        public IActionResult Create()
        {
            //Xử lý MaLSP
            ViewBag.LoaiSPs = new SelectList(_db.LoaiSP_GetAll(), "MaLSP", "TenLSP");


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SanPham sp, IFormFile? image)
        {

            if (image != null && image.Length > 0)//Nếu có chọn ảnh để upload
            {
                var uploads = Path.Combine(_env.WebRootPath, "images", "uploads");
                Directory.CreateDirectory(uploads);//Tạo thư mục nếu nó chưa tồn tại
                var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);
                var path = Path.Combine(uploads, fileName);
                using var fs = new FileStream(path, FileMode.Create);
                await image.CopyToAsync(fs);
                sp.AnhSP = fileName;
            }

            var newMaSP = _db.SanPham_Insert(sp); // trả MaSP
            // nếu cần hiển thị newMaSP, có thể gửi ViewBag.NewMaSP = newMaSP
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(string id)
        {
            var sp = _db.SanPham_GetById(id);
            ViewBag.LoaiSPs = new SelectList(_db.LoaiSP_GetAll(), "MaLSP", "TenLSP", sp.MaLSP);


            return View(sp);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SanPham sp, IFormFile? image, string? existingAnh)
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
                sp.AnhSP = fileName;
            }
            else
            {
                // giữ ảnh cũ
                sp.AnhSP = existingAnh;
            }

            _db.SanPham_Update(sp);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(string id)
        {
            var sp = _db.SanPham_GetById(id);
            return View(sp);
        }

        public IActionResult Delete(string id)
        {
            var sp = _db.SanPham_GetById(id);
            return View(sp);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(string id)
        {
            var sp = _db.SanPham_GetById(id);
            if (sp != null && !string.IsNullOrEmpty(sp.AnhSP))
            {
                var uploads = Path.Combine(_env.WebRootPath, "images", "uploads");
                var file = Path.Combine(uploads, sp.AnhSP);
                if (System.IO.File.Exists(file)) System.IO.File.Delete(file);
            }
            _db.SanPham_Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
