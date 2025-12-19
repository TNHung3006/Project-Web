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
    public class DonBanHangsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DonBanHangsController(ApplicationDbContext db) => _db = db;


        public IActionResult Index() => View(_db.DonBanHang_GetAll());

        // GET: DonBanHangs/Create
        public IActionResult Create()
        {

            // Xử lý khách hàng
            var khList = _db.KhachHang_GetAll() ?? new List<KhachHang>();
            // Lấy tất cả khách hàng và truyền qua ViewBag cho View sử dụng
            ViewBag.KhachHangs = new SelectList(khList, "MaKH", "TenKH");
            return View();
        }

        [HttpPost]
        public IActionResult Create(DonBanHang dbh)
        {
            _db.DonBanHang_Insert(dbh);
            return RedirectToAction(nameof(Index));
        }

        // GET: DonBanHangs/Edit/5
        public IActionResult Edit(int id)
        {
            var dbh = _db.DonBanHang_GetById(id);
            if (dbh == null) return NotFound();

            // Xử lý khách hàng
            var khList = _db.KhachHang_GetAll() ?? new List<KhachHang>();
            // Lấy tất khách hàng, và chọn giá trị mặc định là khách hàng hiện tại của loại này
            ViewBag.KhachHangs = new SelectList(khList, "MaKH", "TenKH", dbh.MaKH);
            return View(dbh);
        }

        [HttpPost]
        public IActionResult Edit(DonBanHang dbh)
        {
            _db.DonBanHang_Update(dbh);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var dbh = _db.DonBanHang_GetById(id);
            return View(dbh);
        }

        public IActionResult Delete(int id)
        {
            var dbh = _db.DonBanHang_GetById(id);
            return View(dbh);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _db.DonBanHang_Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
