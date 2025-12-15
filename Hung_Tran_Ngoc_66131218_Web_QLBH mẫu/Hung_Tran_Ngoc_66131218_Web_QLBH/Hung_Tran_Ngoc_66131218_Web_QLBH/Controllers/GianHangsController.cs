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
    public class GianHangsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public GianHangsController(ApplicationDbContext db) => _db = db;

        // GET: GianHangs
        public IActionResult Index() => View(_db.GianHang_GetAll());

        // GET: GianHangs/Create
        public IActionResult Create()
        {
            // Lấy dữ liệu xã, đảm bảo không trả về null bằng toán tử ??
            var xaList = _db.Xa_GetAll() ?? new List<Xa>();

            ViewBag.Xas = new SelectList(xaList, "MaXa", "TenXa");
            return View();
        }

        [HttpPost]
        public IActionResult Create(GianHang gh)
        {
            _db.GianHang_Insert(gh);
            return RedirectToAction(nameof(Index));
        }

        // GET: GianHangs/Edit/5
        public IActionResult Edit(int id)
        {
            var gh = _db.GianHang_GetById(id);
            if (gh == null) return NotFound();

            var xaList = _db.Xa_GetAll() ?? new List<Xa>();

            // Load danh sách Xã, và chọn MaTinh hiện tại của xã làm giá trị mặc định
            ViewBag.Xas = new SelectList(xaList, "MaXa", "TenXa", gh.MaXa);
            return View(gh);
        }

        [HttpPost]
        public IActionResult Edit(GianHang gh)
        {
            _db.GianHang_Update(gh);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var gh = _db.GianHang_GetById(id);
            return View(gh);
        }

        public IActionResult Delete(int id)
        {
            var gh = _db.GianHang_GetById(id);
            return View(gh);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _db.GianHang_Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
