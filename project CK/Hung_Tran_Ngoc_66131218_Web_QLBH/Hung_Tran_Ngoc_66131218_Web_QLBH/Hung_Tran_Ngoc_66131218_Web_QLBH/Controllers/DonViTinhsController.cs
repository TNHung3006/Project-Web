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
    public class DonViTinhsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DonViTinhsController(ApplicationDbContext db) => _db = db;

        // GET: DonViTinhs
        public IActionResult Index() => View(_db.DonViTinh_GetAll());
        // GET: DonViTinh/Details/5
        public IActionResult Details(int id)
        {
            var dvt = _db.DonViTinh_GetById(id);
            return View(dvt);
        }

        // GET: DonViTinh/Create
        public IActionResult Create() => View();

        // POST: DonViTinhs/Create
        [HttpPost]
        public IActionResult Create(DonViTinh dvt)
        {
            _db.DonViTinh_Insert(dvt);
            return RedirectToAction(nameof(Index));
        }

        // GET: DonViTinhs/Edit/5
        public IActionResult Edit(int id)
        {
            var dvt = _db.DonViTinh_GetById(id);
            return View(dvt);
        }

        // POST: DonViTinhs/Edit/5
        [HttpPost]
        public IActionResult Edit(DonViTinh dvt)
        {
            _db.DonViTinh_Update(dvt);
            return RedirectToAction(nameof(Index));
        }

        // GET: DonViTinhs/Delete/5
        public IActionResult Delete(int id)
        {
            var dvt = _db.DonViTinh_GetById(id);
            return View(dvt);
        }

        // POST: DonViTinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _db.DonViTinh_Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
