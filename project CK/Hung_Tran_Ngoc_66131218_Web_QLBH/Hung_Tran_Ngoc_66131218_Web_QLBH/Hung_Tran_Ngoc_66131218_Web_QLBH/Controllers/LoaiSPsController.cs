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
    public class LoaiSPsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LoaiSPsController(ApplicationDbContext db) => _db = db;


        public IActionResult Index() => View(_db.LoaiSP_GetAll());

        // GET: LoaiSPs/Create
        public IActionResult Create()
        {
            var nspList = _db.NhomSP_GetAll() ?? new List<NhomSP>();
            // Lấy tất cả Nhóm SP và truyền qua ViewBag cho View sử dụng
            ViewBag.NhomSPs = new SelectList(nspList, "MaNhomSP", "TenNhomSP");
            return View();
        }

        [HttpPost]
        public IActionResult Create(LoaiSP loai)
        {
            _db.LoaiSP_Insert(loai);
            return RedirectToAction(nameof(Index));
        }

        // GET: LoaiSPs/Edit/5
        public IActionResult Edit(int id)
        {
            var loai = _db.LoaiSP_GetById(id);
            if (loai == null) return NotFound();


            var nspList = _db.NhomSP_GetAll() ?? new List<NhomSP>();
            // Lấy tất cả Nhóm SP, và chọn giá trị mặc định là MaNhomSP hiện tại của loại này
            ViewBag.NhomSPs = new SelectList(nspList, "MaNhomSP", "TenNhomSP", loai.MaNhomSP);
            return View(loai);
        }

        [HttpPost]
        public IActionResult Edit(LoaiSP loai)
        {
            _db.LoaiSP_Update(loai);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var loai = _db.LoaiSP_GetById(id);
            return View(loai);
        }

        public IActionResult Delete(int id)
        {
            var loai = _db.LoaiSP_GetById(id);
            return View(loai);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _db.LoaiSP_Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
