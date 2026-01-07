
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
    public class DonMuaHangsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DonMuaHangsController(ApplicationDbContext db) => _db = db;


        public IActionResult Index(string? search)
        {
            // Lưu từ khóa tìm kiếm vào ViewData để hiển thị lại trên form
            ViewData["CurrentFilter"] = search;
            // Gọi phương thức GetData với tham số search. 
            // Phương thức này sẽ gọi Stored Procedure đã sửa của bạn.
            var list = _db.DonMuaHang_GetAll(search);
            return View(list);
        }

        // GET: LoaiSPs/Create
        public IActionResult Create()
        {

            // Xử lý MaNCC (Nhà Cung Cấp)
            var nccList = _db.NhaCC_GetAll() ?? new List<NhaCC>();
            // Lấy tất cả nhà cung cấp và truyền qua ViewBag cho View sử dụng
            ViewBag.NhaCCs = new SelectList(nccList, "MaNCC", "TenNCC");

            var nvList = _db.NhanVien_GetAll() ?? new List<NhanVien>();
            ViewBag.NhanViens = new SelectList(nvList, "MaNV", "TenNV");

            var ttdmhList = _db.TrangThaiDMH_GetAll() ?? new List<TrangThaiDMH>();
            ViewBag.TTDMHs = new SelectList(ttdmhList, "MaTTDMH", "TenTTDMH");

            return View();
        }

        [HttpPost]
        public IActionResult Create(DonMuaHang dmh)
        {
            _db.DonMuaHang_Insert(dmh);
            return RedirectToAction(nameof(Index));
        }

        // GET: LoaiSPs/Edit/5
        public IActionResult Edit(int id)
        {
            var dmh = _db.DonMuaHang_GetById(id);
            if (dmh == null) return NotFound();

            // Xử lý MaNCC (Nhà Cung Cấp)
            var nccList = _db.NhaCC_GetAll() ?? new List<NhaCC>();
            // Lấy tất cả nhà cung cấp, và chọn giá trị mặc định là nhà cung cấp hiện tại của loại này
            ViewBag.NhaCCs = new SelectList(nccList, "MaNCC", "TenNCC", dmh.MaNCC);

            var nvList = _db.NhanVien_GetAll() ?? new List<NhanVien>();
            ViewBag.NhanViens = new SelectList(nvList, "MaNV", "TenNV", dmh.MaNV);

            var ttdmhList = _db.TrangThaiDMH_GetAll() ?? new List<TrangThaiDMH>();
            ViewBag.TTDMHs = new SelectList(ttdmhList, "MaTTDMH", "TenTTDMH", dmh.MaTTDMH);

            return View(dmh);
        }

        [HttpPost]
        public IActionResult Edit(DonMuaHang dmh)
        {
            _db.DonMuaHang_Update(dmh);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var dmh = _db.DonMuaHang_GetById(id);
            return View(dmh);
        }

        public IActionResult Delete(int id)
        {
            var dmh = _db.DonMuaHang_GetById(id);
            return View(dmh);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _db.DonMuaHang_Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
