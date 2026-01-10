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
    public class NhanViensController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NhanViensController(ApplicationDbContext db) => _db = db;

        // GET: NhanVien
        public IActionResult Index(string? search)
        {
            // Lưu từ khóa tìm kiếm vào ViewData để hiển thị lại trên form
            ViewData["CurrentFilter"] = search;

            var list = _db.NhanVien_GetAll(search);

            // --- BỔ SUNG: Gán dữ liệu cho [NotMapped] (TenLNV, DiaChiFull) ---
            var listXa = _db.Xa_GetAll();
            var listTinh = _db.Tinh_GetAll();
            var listLoaiNV = _db.LoaiNV_GetAll(); // Lấy danh sách loại NV

            foreach (var nv in list)
            {
                // 1. Gán Tên Loại Nhân Viên
                var lnv = listLoaiNV.FirstOrDefault(x => x.MaLNV == nv.MaLNV);
                nv.TenLNV = lnv != null ? lnv.TenLNV : "";

                // 2. Gán Địa chỉ đầy đủ (Ghép chuỗi)
                var xa = listXa.FirstOrDefault(x => x.MaXa == nv.MaXa);
                var tinh = xa != null ? listTinh.FirstOrDefault(t => t.MaTinh == xa.MaTinh) : null;

                string tenXa = xa?.TenXa ?? "";
                string tenTinh = tinh?.TenTinh ?? "";
                string diaChiCu = nv.DiaChi ?? "";

                // Nối chuỗi thông minh, bỏ qua các thành phần bị rỗng
                var parts = new List<string> { diaChiCu, tenXa, tenTinh };
                nv.DiaChiFull = string.Join(", ", parts.Where(s => !string.IsNullOrEmpty(s)));
            }
            // -----------------------------------------------------------------

            return View(list);
        }

        // GET: NhanVien/Details/5
        public IActionResult Details(int id)
        {
            var nv = _db.NhanVien_GetById(id);
            if (nv == null) return NotFound();

            // --- BỔ SUNG: Hiển thị chi tiết đầy đủ ---
            // 1. Lấy tên loại NV
            var lnv = _db.LoaiNV_GetAll().FirstOrDefault(x => x.MaLNV == nv.MaLNV);
            nv.TenLNV = lnv?.TenLNV;

            // 2. Lấy địa chỉ full
            var xa = _db.Xa_GetAll().FirstOrDefault(x => x.MaXa == nv.MaXa);
            var tinh = xa != null ? _db.Tinh_GetAll().FirstOrDefault(t => t.MaTinh == xa.MaTinh) : null;

            string tenXa = xa?.TenXa ?? "";
            string tenTinh = tinh?.TenTinh ?? "";
            string diaChiCu = nv.DiaChi ?? "";

            var parts = new List<string> { diaChiCu, tenXa, tenTinh };
            nv.DiaChiFull = string.Join(", ", parts.Where(s => !string.IsNullOrEmpty(s)));
            // ----------------------------------------

            return View(nv);
        }

        // GET: NhanVien/Create
        public IActionResult Create()
        {
            // 1. Lấy danh sách Tỉnh để hiển thị dropdown đầu tiên
            var listTinh = _db.Tinh_GetAll() ?? new List<Tinh>();
            ViewBag.Tinhs = new SelectList(listTinh, "MaTinh", "TenTinh");

            // 2. Khởi tạo danh sách Xã rỗng (vì chưa chọn Tỉnh nào)
            ViewBag.Xas = new SelectList(new List<Xa>(), "MaXa", "TenXa");

            var listLNV = _db.LoaiNV_GetAll() ?? new List<LoaiNV>();
            ViewBag.LoaiNVs = new SelectList(listLNV, "MaLNV", "TenLNV");

            return View();
        }

        // POST: NhanVien/Create
        [HttpPost]
        public IActionResult Create(NhanVien nv)
        {
            _db.NhanVien_Insert(nv);
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

        // GET: NhanVien/Edit/5
        public IActionResult Edit(int id)
        {
            var nv = _db.NhanVien_GetById(id);
            if (nv == null) return NotFound();

            // A. Lấy tất cả Tỉnh để đổ vào Dropdown Tỉnh
            var listTinh = _db.Tinh_GetAll() ?? new List<Tinh>();

            // B. Xác định Tỉnh hiện tại (để chọn sẵn khi mở form)
            var currentXa = _db.Xa_GetAll().FirstOrDefault(x => x.MaXa == nv.MaXa);
            int selectedMaTinh = currentXa != null ? currentXa.MaTinh : 0;

            // C. Lấy danh sách Xã thuộc Tỉnh hiện tại
            var listXaOfTinh = _db.Xa_GetAll().Where(x => x.MaTinh == selectedMaTinh).ToList();

            // D. Truyền dữ liệu qua View
            ViewBag.Tinhs = new SelectList(listTinh, "MaTinh", "TenTinh", selectedMaTinh);
            ViewBag.Xas = new SelectList(listXaOfTinh, "MaXa", "TenXa", nv.MaXa);

            var listLNV = _db.LoaiNV_GetAll() ?? new List<LoaiNV>();
            ViewBag.LoaiNVs = new SelectList(listLNV, "MaLNV", "TenLNV", nv.MaLNV);

            return View(nv);
        }

        // POST: NhanVien/Edit/5
        [HttpPost]
        public IActionResult Edit(NhanVien nhanVien)
        {
            _db.NhanVien_Update(nhanVien);
            return RedirectToAction(nameof(Index));
        }

        // GET: NhanVien/Delete/5
        public IActionResult Delete(int id)
        {
            var nv = _db.NhanVien_GetById(id);
            if (nv == null) return NotFound();

            // --- BỔ SUNG: Hiển thị thông tin rõ ràng khi xóa ---
            var lnv = _db.LoaiNV_GetAll().FirstOrDefault(x => x.MaLNV == nv.MaLNV);
            nv.TenLNV = lnv?.TenLNV;

            var xa = _db.Xa_GetAll().FirstOrDefault(x => x.MaXa == nv.MaXa);
            var tinh = xa != null ? _db.Tinh_GetAll().FirstOrDefault(t => t.MaTinh == xa.MaTinh) : null;

            string tenXa = xa?.TenXa ?? "";
            string tenTinh = tinh?.TenTinh ?? "";
            string diaChiCu = nv.DiaChi ?? "";

            var parts = new List<string> { diaChiCu, tenXa, tenTinh };
            nv.DiaChiFull = string.Join(", ", parts.Where(s => !string.IsNullOrEmpty(s)));
            // ---------------------------------------------------

            return View(nv);
        }

        // POST: NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _db.NhanVien_Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}