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
    public class CTBHsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CTBHsController(ApplicationDbContext db) => _db = db;


        public IActionResult Index() => View(_db.CTBH_GetAll());

        // GET: CTBHs/Create
        public IActionResult Create()
        {
            // Xử lý MaDBH (Đơn Bán Hàng)
            var dbhList = _db.DonBanHang_GetAll() ?? new List<DonBanHang>();
            ViewBag.DonBanHangs = new SelectList(dbhList, "MaDBH", "MaDBH");

            // Xử lý MaSP (Sản Phẩm)
            var spList = _db.SanPham_GetAll(null) ?? new List<SanPham>();
            ViewBag.SanPhams = new SelectList(spList, "MaSP", "TenSP");

            return View();
        }

        [HttpPost]
        public IActionResult Create(CTBH ctbh)
        {

            // 1. Kiểm tra xem bản ghi (MaDBH, MaSP) này đã tồn tại chưa
            var existingItem = _db.CTBH_GetById(ctbh.MaDBH, ctbh.MaSP);

            if (existingItem != null)
            {
                // 2. Nếu tồn tại: Cập nhật Số Lượng và Đơn Giá
                // Giả định bạn muốn cộng dồn số lượng. Đơn giá có thể lấy từ bản ghi mới
                existingItem.SoLuong += ctbh.SoLuong;
                existingItem.DonGia = ctbh.DonGia; // Cập nhật đơn giá theo giá mới nhất

                _db.CTBH_Update(existingItem);
            }
            else
            {
                // 3. Nếu chưa tồn tại: Thực hiện INSERT
                _db.CTBH_Insert(ctbh);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: CTBHs/Edit?
        public IActionResult Edit(int maDBH, int maSP)
        {
            var ctbh = _db.CTBH_GetById(maDBH, maSP);
            if (ctbh == null) return NotFound();

            // Xử lý MaDBH (Đơn Bán Hàng)
            var dbhList = _db.DonBanHang_GetAll() ?? new List<DonBanHang>();
            ViewBag.DonBanHangs = new SelectList(dbhList, "MaDBH", "MaDBH", ctbh.MaDBH);

            // Xử lý MaSP (Sản Phẩm)
            var spList = _db.SanPham_GetAll(null) ?? new List<SanPham>();
            ViewBag.SanPhams = new SelectList(spList, "MaSP", "TenSP", ctbh.MaSP);

            return View(ctbh);
        }

        [HttpPost]
        public IActionResult Edit(CTBH ctbh)
        {
            _db.CTBH_Update(ctbh);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int maDBH, int maSP)
        {
            var ctbh = _db.CTBH_GetById(maDBH, maSP);
            return View(ctbh);
        }

        public IActionResult Delete(int maDBH, int maSP)
        {
            var ctbh = _db.CTBH_GetById(maDBH, maSP);
            return View(ctbh);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int maDBH, int maSP)
        {
            _db.CTBH_Delete(maDBH, maSP);
            return RedirectToAction(nameof(Index));
        }
    }
}