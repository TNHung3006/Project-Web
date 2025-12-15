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
    public class NhaCCsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NhaCCsController(ApplicationDbContext db) => _db = db;

        // GET: Tinhs
        public IActionResult Index() => View(_db.NhaCC_GetAll());
        // GET: Tinhs/Details/5
        public IActionResult Details(int id)
        {
            var nhaCC = _db.NhaCC_GetById(id);
            return View(nhaCC);
        }

        // GET: Tinhs/Create
        public IActionResult Create() => View();

        // POST: Tinhs/Create
        [HttpPost]
        public IActionResult Create(NhaCC nhaCC)
        {
            _db.NhaCC_Insert(nhaCC);
            return RedirectToAction(nameof(Index));
        }

        // GET: Tinhs/Edit/5
        public IActionResult Edit(int id)
        {
            var nhaCC = _db.NhaCC_GetById(id);
            return View(nhaCC);
        }

        // POST: Tinhs/Edit/5
        [HttpPost]
        public IActionResult Edit(NhaCC nhaCC)
        {
            _db.NhaCC_Update(nhaCC);
            return RedirectToAction(nameof(Index));
        }

        // GET: Tinhs/Delete/5
        public IActionResult Delete(int id)
        {
            var nhaCC = _db.NhaCC_GetById(id);
            return View(nhaCC);
        }

        // POST: Tinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _db.NhaCC_Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
