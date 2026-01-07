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
    public class NhomSPsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NhomSPsController(ApplicationDbContext db) => _db = db;

        // GET: NhomSPs
        public IActionResult Index() => View(_db.NhomSP_GetAll());
        // GET: NhomSPs/Details/5
        public IActionResult Details(int id)
        {
            var nsp = _db.NhomSP_GetById(id);
            return View(nsp);
        }

        // GET: NhomSPs/Create
        public IActionResult Create() => View();

        // POST: NhomSPs/Create
        [HttpPost]
        public IActionResult Create(NhomSP nsp)
        {
            _db.NhomSP_Insert(nsp);
            return RedirectToAction(nameof(Index));
        }

        // GET: NhomSPs/Edit/5
        public IActionResult Edit(int id)
        {
            var nsp = _db.NhomSP_GetById(id);
            return View(nsp);
        }

        // POST: NhomSPs/Edit/5
        [HttpPost]
        public IActionResult Edit(NhomSP nhomSP)
        {
            _db.NhomSP_Update(nhomSP);
            return RedirectToAction(nameof(Index));
        }

        // GET: NhomSPs/Delete/5
        public IActionResult Delete(int id)
        {
            var nsp = _db.NhomSP_GetById(id);
            return View(nsp);
        }

        // POST: NhomSPs/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _db.NhomSP_Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
