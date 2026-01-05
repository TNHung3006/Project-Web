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
    public class CTMHsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CTMHsController(ApplicationDbContext db) => _db = db;


        public IActionResult Index() => View(_db.CTMH_GetAll());

        // GET: CTMHs/Create
        public IActionResult Create()
        {
            // Xử lý MaDMH (Đơn Mua Hàng)
            var dmhList = _db.DonMuaHang_GetAll() ?? new List<DonMuaHang>();
            ViewBag.DonMuaHangs = new SelectList(dmhList, "MaDMH", "MaDMH");

            // Xử lý MaSP (Sản Phẩm)
            var spList = _db.SanPham_GetAll(null) ?? new List<SanPham>();
            ViewBag.SanPhams = new SelectList(spList, "MaSP", "TenSP");

            return View();
        }

        [HttpPost]
        public IActionResult Create(CTMH ctmh)
        {

            // 1. Kiểm tra xem bản ghi (MaDMH, MaSP) này đã tồn tại chưa
            
            var existingItem = _db.CTMH_GetById(ctmh.MaDMH, ctmh.MaSP);

            if (existingItem != null)
            {
                // Logic UPSERT: Cập nhật Số Lượng và Đơn Giá
                existingItem.SLM += ctmh.SLM;
                existingItem.DGM = ctmh.DGM;

                _db.CTMH_Update(existingItem);
            }
            else
            {
                // Thực hiện INSERT
                _db.CTMH_Insert(ctmh);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: CTBHs/Edit?
        public IActionResult Edit(int maDMH, int maSP)
        {
            var ctmh = _db.CTMH_GetById(maDMH, maSP);
            if (ctmh == null) return NotFound();

            // Xử lý MaDBH (Đơn Bán Hàng)
            var dmhList = _db.DonBanHang_GetAll() ?? new List<DonBanHang>();
            ViewBag.DonMuaHangs = new SelectList(dmhList, "MaDMH", "MaDMH", ctmh.MaDMH);

            // Xử lý MaSP (Sản Phẩm)
            var spList = _db.SanPham_GetAll(null) ?? new List<SanPham>();
            ViewBag.SanPhams = new SelectList(spList, "MaSP", "TenSP", ctmh.MaSP);

            return View(ctmh);
        }

        [HttpPost]
        public IActionResult Edit(CTMH ctmh)
        {
            _db.CTMH_Update(ctmh);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int maDMH, int maSP)
        {
            var ctmh = _db.CTMH_GetById(maDMH, maSP);
            return View(ctmh);
        }

        public IActionResult Delete(int maDMH, int maSP)
        {
            var ctmh = _db.CTMH_GetById(maDMH, maSP);
            return View(ctmh);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int maDMH, int maSP)
        {
            _db.CTMH_Delete(maDMH, maSP);
            return RedirectToAction(nameof(Index));
        }
    }
}
