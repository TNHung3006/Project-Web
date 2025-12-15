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
    public class NhanViensController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NhanViensController(ApplicationDbContext db) => _db = db;

        // GET: NhanVien
        public IActionResult Index() => View(_db.NhanVien_GetAll());
        // GET: NhanVien/Details/5
        public IActionResult Details(int id)
        {
            var nv = _db.NhanVien_GetById(id);
            return View(nv);
        }

        // GET: NhanVien/Create
        public IActionResult Create() => View();

        // POST: NhanVien/Create
        [HttpPost]
        public IActionResult Create(NhanVien nv)
        {
            _db.NhanVien_Insert(nv);
            return RedirectToAction(nameof(Index));
        }

        // GET: NhanVien/Edit/5
        public IActionResult Edit(int id)
        {
            var nv = _db.NhanVien_GetById(id);
            return View(nv);
        }

        // POST: NhanVien/Edit/5
        [HttpPost]
        public IActionResult Edit(NhanVien nhanVien)
        {
            _db.NhanVien_Update(nhanVien);
            return RedirectToAction(nameof(Index));
        }

        // GET: NhanVien/Delete/5
        public IActionResult Delete(int id)
        {
            var nv = _db.NhanVien_GetById(id);
            return View(nv);
        }

        // POST: NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _db.NhanVien_Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
