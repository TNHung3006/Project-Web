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
    public class TrangThaiDMHsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TrangThaiDMHsController(ApplicationDbContext db) => _db = db;

        // GET: TrangThaiDMHs
        public IActionResult Index() => View(_db.TrangThaiDMH_GetAll());
        // GET: TrangThaiDMHs/Details/5
        public IActionResult Details(int id)
        {
            var ttDMH = _db.TrangThaiDMH_GetById(id);
            return View(ttDMH);
        }

        // GET: TrangThaiDMHs/Create
        public IActionResult Create() => View();

        // POST: TrangThaiDMHs/Create
        [HttpPost]
        public IActionResult Create(TrangThaiDMH ttDMH)
        {
            _db.TrangThaiDMH_Insert(ttDMH);
            return RedirectToAction(nameof(Index));
        }

        // GET: TrangThaiDMHs/Edit/5
        public IActionResult Edit(int id)
        {
            var ttDMH = _db.TrangThaiDMH_GetById(id);
            return View(ttDMH);
        }

        // POST: TrangThaiDMHs/Edit/5
        [HttpPost]
        public IActionResult Edit(TrangThaiDMH ttDMH)
        {
            _db.TrangThaiDMH_Update(ttDMH);
            return RedirectToAction(nameof(Index));
        }

        // GET: TrangThaiDMHs/Delete/5
        public IActionResult Delete(int id)
        {
            var ttDMH = _db.TrangThaiDMH_GetById(id);
            return View(ttDMH);
        }

        // POST: TrangThaiDMHs/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _db.TrangThaiDMH_Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
