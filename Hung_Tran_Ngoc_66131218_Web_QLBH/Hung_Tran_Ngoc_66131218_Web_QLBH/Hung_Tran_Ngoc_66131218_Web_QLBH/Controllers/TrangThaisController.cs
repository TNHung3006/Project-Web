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
    public class TrangThaisController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TrangThaisController(ApplicationDbContext db) => _db = db;

        // GET: TrangThais
        public IActionResult Index() => View(_db.TrangThai_GetAll());
        // GET: TrangThais/Details/5
        public IActionResult Details(int id)
        {
            var tt = _db.TrangThai_GetById(id);
            return View(tt);
        }

        // GET: TrangThais/Create
        public IActionResult Create() => View();

        // POST: TrangThais/Create
        [HttpPost]
        public IActionResult Create(TrangThai tt)
        {
            _db.TrangThai_Insert(tt);
            return RedirectToAction(nameof(Index));
        }

        // GET: TrangThais/Edit/5
        public IActionResult Edit(int id)
        {
            var tt = _db.TrangThai_GetById(id);
            return View(tt);
        }

        // POST: TrangThais/Edit/5
        [HttpPost]
        public IActionResult Edit(TrangThai tt)
        {
            _db.TrangThai_Update(tt);
            return RedirectToAction(nameof(Index));
        }

        // GET: TrangThais/Delete/5
        public IActionResult Delete(int id)
        {
            var tt = _db.TrangThai_GetById(id);
            return View(tt);
        }

        // POST: TrangThais/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _db.TrangThai_Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
