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
    public class XasController : Controller
    {
        private readonly ApplicationDbContext _db;

        public XasController(ApplicationDbContext db) => _db = db;

        // GET: Xas
        public IActionResult Index() => View(_db.Xa_GetAll());

        // GET: Xas/Create
        public IActionResult Create()
        {
            // Lấy dữ liệu Tỉnh, đảm bảo không trả về null bằng toán tử ??
            var tinhList = _db.Tinh_GetAll() ?? new List<Tinh>();

            ViewBag.Tinhs = new SelectList(tinhList, "MaTinh", "TenTinh");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Xa xa)
        {
            _db.Xa_Insert(xa);
            return RedirectToAction(nameof(Index));
        }

        // GET: Xas/Edit/5
        public IActionResult Edit(int id)
        {
            var xa = _db.Xa_GetById(id);
            if (xa == null) return NotFound();

            var tinhList = _db.Tinh_GetAll() ?? new List<Tinh>();

            // Load danh sách Tỉnh, và chọn MaTinh hiện tại của xã làm giá trị mặc định
            ViewBag.Tinhs = new SelectList(tinhList, "MaTinh", "TenTinh", xa.MaTinh);
            return View(xa);
        }

        [HttpPost]
        public IActionResult Edit(Xa xa)
        {
            _db.Xa_Update(xa);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var xa = _db.Xa_GetById(id);
            return View(xa);
        }

        public IActionResult Delete(int id)
        {
            var xa = _db.Xa_GetById(id);
            return View(xa);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _db.Xa_Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}