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
        private readonly IWebHostEnvironment _env;

        public SanPhamsController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        // GET: SanPhams
        public IActionResult Index(string? search)
        {
            ViewData["CurrentFilter"] = search;
            var list = _db.SanPham_GetAll(search);

            // --- BỔ SUNG: Gán dữ liệu cho các thuộc tính [NotMapped] ---
            // Vì [NotMapped] nên EF không tự map, ta phải lấy tên dựa vào ID
            var listLoai = _db.LoaiSP_GetAll();
            var listHang = _db.HangSX_GetAll();
            var listDVT = _db.DonViTinh_GetAll();
            var listTT = _db.TrangThai_GetAll();

            foreach (var item in list)
            {
                // Tìm tên Loại theo MaLSP
                var loai = listLoai.FirstOrDefault(x => x.MaLSP == item.MaLSP);
                item.LoaiSP = loai != null ? loai.TenLSP : "";

                // Tìm tên Hãng theo MaHSX
                var hang = listHang.FirstOrDefault(x => x.MaHSX == item.MaHSX);
                item.HangSX = hang != null ? hang.TenHSX : "";

                // Tìm tên Đơn vị tính theo MaDVT
                var dvt = listDVT.FirstOrDefault(x => x.MaDVT == item.MaDVT);
                item.TenDVT = dvt != null ? dvt.TenDVT : "";

                // Tìm tên Trạng thái theo MaTT
                var tt = listTT.FirstOrDefault(x => x.MaTT == item.MaTT);
                item.TenTT = tt != null ? tt.TenTT : "";
            }
            // --- KẾT THÚC BỔ SUNG ---

            return View(list);
        }

        // --- KHU VỰC XỬ LÝ AJAX (Cascading Dropdown) ---

        // 1. Lấy danh sách Loại SP dựa theo Mã Nhóm SP
        [HttpGet]
        public IActionResult GetLoaiSPByNhom(int maNhomSP)
        {
            var allLoai = _db.LoaiSP_GetAll();
            // Lọc loại sản phẩm thuộc nhóm này
            var loaiSPs = allLoai.Where(x => x.MaNhomSP == maNhomSP).ToList();
            return Json(loaiSPs);
        }

        // 2. Lấy danh sách Hãng SX dựa theo Mã Nước
        [HttpGet]
        public IActionResult GetHangSXByNuoc(string maNuoc)
        {
            var allHang = _db.HangSX_GetAll();
            // Lọc hãng sản xuất thuộc nước này
            var hangSXs = allHang.Where(x => x.MaNuoc == maNuoc).ToList();
            return Json(hangSXs);
        }

        // GET: SanPhams/Create
        public IActionResult Create()
        {
            // A. Chuẩn bị dữ liệu cho Dropdown NHÓM SP -> LOẠI SP
            var listNhom = _db.NhomSP_GetAll() ?? new List<NhomSP>();
            ViewBag.NhomSPs = new SelectList(listNhom, "MaNhomSP", "TenNhomSP");
            // Ban đầu chưa chọn nhóm nên Loại SP để trống
            ViewBag.LoaiSPs = new SelectList(new List<LoaiSP>(), "MaLSP", "TenLSP");

            // B. Chuẩn bị dữ liệu cho Dropdown NƯỚC -> HÃNG SX
            var listNuoc = _db.Nuoc_GetAll() ?? new List<Nuoc>();
            ViewBag.Nuocs = new SelectList(listNuoc, "MaNuoc", "TenNuoc");
            // Ban đầu chưa chọn nước nên Hãng SX để trống
            ViewBag.HangSXs = new SelectList(new List<HangSX>(), "MaHSX", "TenHSX");

            // C. Các Dropdown độc lập khác
            var listTT = _db.TrangThai_GetAll() ?? new List<TrangThai>();
            ViewBag.TrangThais = new SelectList(listTT, "MaTT", "TenTT");

            var listDVT = _db.DonViTinh_GetAll() ?? new List<DonViTinh>();
            ViewBag.DonViTinhs = new SelectList(listDVT, "MaDVT", "TenDVT");

            return View();
        }

        // POST: SanPhams/Create
        [HttpPost]
        public async Task<IActionResult> Create(SanPham sp, IFormFile? image)
        {
            if (image != null && image.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "images", "uploads");
                Directory.CreateDirectory(uploads);
                var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);
                var path = Path.Combine(uploads, fileName);
                using var fs = new FileStream(path, FileMode.Create);
                await image.CopyToAsync(fs);
                sp.AnhSP = fileName;
            }

            // Gọi Stored Procedure Insert
            _db.SanPham_Insert(sp);
            return RedirectToAction(nameof(Index));
        }

        // GET: SanPhams/Edit/5
        public IActionResult Edit(string id)
        {
            var sp = _db.SanPham_GetById(id);
            if (sp == null) return NotFound();

            // --- XỬ LÝ LOGIC 1: NHÓM SP & LOẠI SP ---
            // 1. Lấy tất cả nhóm để nạp vào dropdown cha
            var listNhom = _db.NhomSP_GetAll() ?? new List<NhomSP>();

            // 2. Tìm xem sản phẩm này đang thuộc Nhóm nào (dựa vào MaLSP hiện tại của nó)
            var currentLoaiSP = _db.LoaiSP_GetAll().FirstOrDefault(x => x.MaLSP == sp.MaLSP);
            int selectedMaNhom = currentLoaiSP != null ? currentLoaiSP.MaNhomSP : 0;

            // 3. Lấy danh sách Loại SP tương ứng với Nhóm vừa tìm được
            var listLoaiOfNhom = _db.LoaiSP_GetAll().Where(x => x.MaNhomSP == selectedMaNhom).ToList();

            ViewBag.NhomSPs = new SelectList(listNhom, "MaNhomSP", "TenNhomSP", selectedMaNhom);
            ViewBag.LoaiSPs = new SelectList(listLoaiOfNhom, "MaLSP", "TenLSP", sp.MaLSP);


            // --- XỬ LÝ LOGIC 2: NƯỚC & HÃNG SX ---
            // 1. Lấy tất cả nước
            var listNuoc = _db.Nuoc_GetAll() ?? new List<Nuoc>();

            // 2. Tìm xem hãng SX hiện tại thuộc Nước nào
            var currentHangSX = _db.HangSX_GetAll().FirstOrDefault(x => x.MaHSX == sp.MaHSX);
            string selectedMaNuoc = currentHangSX != null ? currentHangSX.MaNuoc : null;

            // 3. Lấy danh sách Hãng SX thuộc Nước đó
            var listHangOfNuoc = _db.HangSX_GetAll().Where(x => x.MaNuoc == selectedMaNuoc).ToList();

            ViewBag.Nuocs = new SelectList(listNuoc, "MaNuoc", "TenNuoc", selectedMaNuoc);
            ViewBag.HangSXs = new SelectList(listHangOfNuoc, "MaHSX", "TenHSX", sp.MaHSX);


            // --- CÁC DROPDOWN KHÁC ---
            var listTT = _db.TrangThai_GetAll();
            ViewBag.TrangThais = new SelectList(listTT, "MaTT", "TenTT", sp.MaTT);

            var listDVT = _db.DonViTinh_GetAll();
            ViewBag.DonViTinhs = new SelectList(listDVT, "MaDVT", "TenDVT", sp.MaDVT);

            return View(sp);
        }

        // POST: SanPhams/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(SanPham sp, IFormFile? image, string? existingAnh)
        {
            var uploads = Path.Combine(_env.WebRootPath, "images", "uploads");
            Directory.CreateDirectory(uploads);

            if (image != null && image.Length > 0)
            {
                // Upload ảnh mới
                var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);
                var path = Path.Combine(uploads, fileName);
                using var fs = new FileStream(path, FileMode.Create);
                await image.CopyToAsync(fs);

                // Xóa ảnh cũ nếu có
                if (!string.IsNullOrEmpty(existingAnh))
                {
                    var oldPath = Path.Combine(uploads, existingAnh);
                    if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                }
                sp.AnhSP = fileName;
            }
            else
            {
                // Giữ nguyên ảnh cũ
                sp.AnhSP = existingAnh;
            }

            _db.SanPham_Update(sp);
            return RedirectToAction(nameof(Index));
        }

        // GET: SanPhams/Details
        public IActionResult Details(string id)
        {
            var sp = _db.SanPham_GetById(id);
            if (sp == null) return NotFound();

            // --- BỔ SUNG: Gán dữ liệu cho các thuộc tính [NotMapped] ---
            var loai = _db.LoaiSP_GetAll().FirstOrDefault(x => x.MaLSP == sp.MaLSP);
            sp.LoaiSP = loai != null ? loai.TenLSP : "";

            var hang = _db.HangSX_GetAll().FirstOrDefault(x => x.MaHSX == sp.MaHSX);
            sp.HangSX = hang != null ? hang.TenHSX : "";

            var dvt = _db.DonViTinh_GetAll().FirstOrDefault(x => x.MaDVT == sp.MaDVT);
            sp.TenDVT = dvt != null ? dvt.TenDVT : "";

            var tt = _db.TrangThai_GetAll().FirstOrDefault(x => x.MaTT == sp.MaTT);
            sp.TenTT = tt != null ? tt.TenTT : "";
            // --- KẾT THÚC BỔ SUNG ---

            return View(sp);
        }

        // GET: SanPhams/Delete
        public IActionResult Delete(string id)
        {
            var sp = _db.SanPham_GetById(id);
            if (sp == null) return NotFound();
            return View(sp);
        }

        // POST: SanPhams/Delete
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