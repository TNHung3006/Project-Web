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
    public class NuocsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NuocsController(ApplicationDbContext db) => _db = db;

        // GET: Nuocs
        public IActionResult Index(string? search)
        {
            // Lưu từ khóa tìm kiếm vào ViewData để hiển thị lại trên form
            ViewData["CurrentFilter"] = search;
            // Gọi phương thức GetData với tham số search. 
            // Phương thức này sẽ gọi Stored Procedure đã sửa của bạn.
            var list = _db.Nuoc_GetAll(search);
            return View(list);
        }
        // GET: Nuocss/Details/5
        public IActionResult Details(string id)
        {
            var n = _db.Nuoc_GetById(id);
            return View(n);
        }

        // GET: Nuocs/Create
        public IActionResult Create() => View();

        // POST: Nuocs/Create
        [HttpPost]
        public IActionResult Create(Nuoc n)
        {
            _db.Nuoc_Insert(n);
            return RedirectToAction(nameof(Index));
        }

        // GET: Nuocs/Edit/5
        public IActionResult Edit(string id)
        {
            var nuoc = _db.Nuoc_GetById(id);
            return View(nuoc);
        }

        // POST: Nuocs/Edit/5
        [HttpPost]
        public IActionResult Edit(string id, Nuoc nuoc)
        {
            _db.Nuoc_Update(id, nuoc);
            return RedirectToAction(nameof(Index));
        }

        // GET: Nuocs/Delete/5
        public IActionResult Delete(string id)
        {
            var n = _db.Nuoc_GetById(id);
            return View(n);
        }

        // POST: Nuocs/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(string id)
        {
            _db.Nuoc_Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
