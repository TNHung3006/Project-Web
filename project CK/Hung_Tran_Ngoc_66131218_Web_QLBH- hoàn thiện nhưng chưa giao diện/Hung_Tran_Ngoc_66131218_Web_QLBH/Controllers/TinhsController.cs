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
    public class TinhsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TinhsController(ApplicationDbContext db) => _db = db;

        // GET: Tinhs
        public IActionResult Index() => View(_db.Tinh_GetAll());
        // GET: Tinhs/Details/5
        public IActionResult Details(int id)
        {
            var tinh = _db.Tinh_GetById(id);
            return View(tinh);
        }

        // GET: Tinhs/Create
        public IActionResult Create() => View();

        // POST: Tinhs/Create
        [HttpPost]
        public IActionResult Create(Tinh tinh)
        {
            _db.Tinh_Insert(tinh);
            return RedirectToAction(nameof(Index));
        }

        // GET: Tinhs/Edit/5
        public IActionResult Edit(int id)
        {
            var tinh = _db.Tinh_GetById(id);
            return View(tinh);
        }

        // POST: Tinhs/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, Tinh tinh)
        {
            _db.Tinh_Update(id, tinh);
            return RedirectToAction(nameof(Index));
        }

        // GET: Tinhs/Delete/5
        public IActionResult Delete(int id)
        {
            var tinh = _db.Tinh_GetById(id);
            return View(tinh);
        }

        // POST: Tinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _db.Tinh_Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
