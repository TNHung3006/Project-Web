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
    public class LoaiNVsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LoaiNVsController(ApplicationDbContext db) => _db = db;

        // GET: LoaiNVs
        public IActionResult Index() => View(_db.LoaiNV_GetAll());
        // GET: LoaiNV/Details/5
        public IActionResult Details(int id)
        {
            var lnv = _db.LoaiNV_GetById(id);
            return View(lnv);
        }

        // GET: LoaiNV/Create
        public IActionResult Create() => View();

        // POST: LoaiNVs/Create
        [HttpPost]
        public IActionResult Create(LoaiNV lnv)
        {
            _db.LoaiNV_Insert(lnv);
            return RedirectToAction(nameof(Index));
        }

        // GET: LoaiNVs/Edit/5
        public IActionResult Edit(int id)
        {
            var lnv = _db.LoaiNV_GetById(id);
            return View(lnv);
        }

        // POST: LoaiNVs/Edit/5
        [HttpPost]
        public IActionResult Edit(LoaiNV lnv)
        {
            _db.LoaiNV_Update(lnv);
            return RedirectToAction(nameof(Index));
        }

        // GET: LoaiNVs/Delete/5
        public IActionResult Delete(int id)
        {
            var lnv = _db.LoaiNV_GetById(id);
            return View(lnv);
        }

        // POST: LoaiNVs/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _db.LoaiNV_Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
