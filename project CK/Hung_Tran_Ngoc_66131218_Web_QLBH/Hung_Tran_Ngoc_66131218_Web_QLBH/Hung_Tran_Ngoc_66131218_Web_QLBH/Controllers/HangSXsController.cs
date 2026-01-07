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
    public class HangSXsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HangSXsController(ApplicationDbContext db) => _db = db;


        public IActionResult Index() => View(_db.HangSX_GetAll());

        // GET: HangSXs/Create
        public IActionResult Create()
        {
            var nspList = _db.Nuoc_GetAll() ?? new List<Nuoc>();
            // Lấy tất cả Nhóm SP và truyền qua ViewBag cho View sử dụng
            ViewBag.Nuocs = new SelectList(nspList, "MaNuoc", "TenNuoc");
            return View();
        }

        [HttpPost]
        public IActionResult Create(HangSX hsx)
        {
            _db.HangSX_Insert(hsx);
            return RedirectToAction(nameof(Index));
        }

        // GET: LoaiSPs/Edit/5
        public IActionResult Edit(int id)
        {
            var hsx = _db.HangSX_GetById(id);
            if (hsx == null) return NotFound();


            var nspList = _db.Nuoc_GetAll() ?? new List<Nuoc>();
            // Lấy tất cả Nhóm SP, và chọn giá trị mặc định là MaNhomSP hiện tại của loại này
            ViewBag.Nuocs = new SelectList(nspList, "MaNuoc", "TenNuoc", hsx.MaNuoc);
            return View(hsx);
        }

        [HttpPost]
        public IActionResult Edit(HangSX hsx)
        {
            _db.HangSX_Update(hsx);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var hsx = _db.HangSX_GetById(id);
            return View(hsx);
        }

        public IActionResult Delete(int id)
        {
            var hsx = _db.HangSX_GetById(id);
            return View(hsx);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _db.HangSX_Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
