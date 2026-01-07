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
    public class TrangThaiDBHsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TrangThaiDBHsController(ApplicationDbContext db) => _db = db;

        // GET: TrangThaiDBHs
        public IActionResult Index() => View(_db.TrangThaiDBH_GetAll());
        // GET: TrangThaiDBHs/Details/5
        public IActionResult Details(int id)
        {
            var tt = _db.TrangThaiDBH_GetById(id);
            return View(tt);
        }

        // GET: TrangThaiDBHs/Create
        public IActionResult Create() => View();

        // POST: TrangThaiDBHs/Create
        [HttpPost]
        public IActionResult Create(TrangThaiDBH ttDBH)
        {
            _db.TrangThaiDBH_Insert(ttDBH);
            return RedirectToAction(nameof(Index));
        }

        // GET: TrangThaiDBHs/Edit/5
        public IActionResult Edit(int id)
        {
            var tt = _db.TrangThaiDBH_GetById(id);
            return View(tt);
        }

        // POST: TrangThaiDBHs/Edit/5
        [HttpPost]
        public IActionResult Edit(TrangThaiDBH ttDBH)
        {
            _db.TrangThaiDBH_Update(ttDBH);
            return RedirectToAction(nameof(Index));
        }

        // GET: TrangThaiDBHs/Delete/5
        public IActionResult Delete(int id)
        {
            var tt = _db.TrangThaiDBH_GetById(id);
            return View(tt);
        }

        // POST: TrangThaiDBHs/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _db.TrangThaiDBH_Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
