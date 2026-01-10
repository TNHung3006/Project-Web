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

        // 1. SỬA HÀM INDEX: Để hiển thị Tên Khách và Trạng Thái trong danh sách
        public IActionResult Index(string? search)
        {
            ViewData["CurrentFilter"] = search;
            var list = _db.DonBanHang_GetAll(search);

            // Lấy danh sách phụ trợ để tra cứu tên
            var listKhach = _db.KhachHang_GetAll();
            var listTT = _db.TrangThaiDBH_GetAll();

            // Vòng lặp để điền thông tin vào các biến [NotMapped]
            foreach (var item in list)
            {
                // A. Điền tên khách hàng (Họ + Tên)
                var kh = listKhach.FirstOrDefault(k => k.MaKH == item.MaKH);
                if (kh != null)
                {
                    item.TenKHFull = kh.HoKH + " " + kh.TenKH;
                }
                else
                {
                    item.TenKHFull = "Khách vãng lai";
                }

                // B. Điền tên trạng thái đơn hàng
                var tt = listTT.FirstOrDefault(t => t.MaTTDBH == item.MaTTDBH);
                item.TenTTDBH = tt?.TenTTDBH ?? "Chưa xác định";

                // C. Xử lý địa chỉ hiển thị (Nếu cần)
                if (string.IsNullOrEmpty(item.DiaChiGH))
                {
                    item.DiaChiGHFull = "Mua trực tiếp tại quầy";
                }
                else
                {
                    item.DiaChiGHFull = item.DiaChiGH; // Có thể nối thêm Xã/Tỉnh nếu muốn
                }
            }

            return View(list);
        }

        // GET: DonBanHangs/Create
        public IActionResult Create()
        {
            var khList = _db.KhachHang_GetAll() ?? new List<KhachHang>();
            ViewBag.KhachHangs = new SelectList(khList, "MaKH", "TenKH");

            var listTinh = _db.Tinh_GetAll() ?? new List<Tinh>();
            ViewBag.Tinhs = new SelectList(listTinh, "MaTinh", "TenTinh");
            ViewBag.Xas = new SelectList(new List<Xa>(), "MaXa", "TenXa");

            var ttdbhList = _db.TrangThaiDBH_GetAll() ?? new List<TrangThaiDBH>();
            ViewBag.TrangThaiDBHs = new SelectList(ttdbhList, "MaTTDBH", "TenTTDBH");
            return View();
        }

        [HttpPost]
        public IActionResult Create(DonBanHang dbh)
        {
            _db.DonBanHang_Insert(dbh);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult GetXaByTinh(int maTinh)
        {
            var allXas = _db.Xa_GetAll();
            var xas = allXas.Where(x => x.MaTinh == maTinh).ToList();
            return Json(xas);
        }

        // GET: DonBanHangs/Edit/5
        public IActionResult Edit(int id)
        {
            var dbh = _db.DonBanHang_GetById(id);
            if (dbh == null) return NotFound();

            var listTinh = _db.Tinh_GetAll() ?? new List<Tinh>();
            var currentXa = _db.Xa_GetAll().FirstOrDefault(x => x.MaXa == dbh.MaXa);
            int selectedMaTinh = currentXa != null ? currentXa.MaTinh : 0;
            var listXaOfTinh = _db.Xa_GetAll().Where(x => x.MaTinh == selectedMaTinh).ToList();

            ViewBag.Tinhs = new SelectList(listTinh, "MaTinh", "TenTinh", selectedMaTinh);
            ViewBag.Xas = new SelectList(listXaOfTinh, "MaXa", "TenXa", dbh.MaXa);

            var khList = _db.KhachHang_GetAll() ?? new List<KhachHang>();
            ViewBag.KhachHangs = new SelectList(khList, "MaKH", "TenKH", dbh.MaKH);

            var ttdbhList = _db.TrangThaiDBH_GetAll() ?? new List<TrangThaiDBH>();
            ViewBag.TrangThaiDBHs = new SelectList(ttdbhList, "MaTTDBH", "TenTTDBH", dbh.MaTTDBH);

            return View(dbh);
        }

        [HttpPost]
        public IActionResult Edit(DonBanHang dbh)
        {
            _db.DonBanHang_Update(dbh);
            return RedirectToAction(nameof(Index));
        }

        // 2. SỬA HÀM DETAILS: Để hiển thị chi tiết đầy đủ trong bảng thông tin
        public IActionResult Details(int id)
        {
            var dbh = _db.DonBanHang_GetById(id);
            if (dbh == null) return NotFound();

            // A. Lấy tên Khách hàng
            var kh = _db.KhachHang_GetById(dbh.MaKH);
            dbh.TenKHFull = kh != null ? (kh.HoKH + " " + kh.TenKH) : "Khách vãng lai";

            // B. Lấy tên Trạng thái
            var tt = _db.TrangThaiDBH_GetById(dbh.MaTTDBH);
            dbh.TenTTDBH = tt?.TenTTDBH ?? "Chưa xác định";

            // C. Lấy tên Xã/Tỉnh để hiển thị địa chỉ đẹp hơn
            var xa = _db.Xa_GetById(dbh.MaXa);
            string tenXa = xa?.TenXa ?? "";

            // Nếu có xã thì tìm tiếp Tỉnh
            string tenTinh = "";
            if (xa != null)
            {
                var tinh = _db.Tinh_GetById(xa.MaTinh);
                tenTinh = tinh?.TenTinh ?? "";
            }

            // Ghép chuỗi địa chỉ đầy đủ
            if (!string.IsNullOrEmpty(dbh.DiaChiGH))
            {
                // Ví dụ: 123 Đường ABC, Xã Hòa Thắng, Tỉnh Đắk Lắk
                dbh.DiaChiGHFull = $"{dbh.DiaChiGH}, {tenXa}, {tenTinh}".Trim(',', ' ');
            }
            else
            {
                dbh.DiaChiGHFull = "Mua trực tiếp";
            }

            return View(dbh);
        }

        // 3. SỬA HÀM DELETE: Để hiển thị thông tin trước khi xóa
        public IActionResult Delete(int id)
        {
            var dbh = _db.DonBanHang_GetById(id);
            if (dbh == null) return NotFound();

            // Cũng cần lấy tên để hiển thị lúc xác nhận xóa
            var kh = _db.KhachHang_GetById(dbh.MaKH);
            dbh.TenKHFull = kh != null ? (kh.HoKH + " " + kh.TenKH) : "Khách vãng lai";

            var tt = _db.TrangThaiDBH_GetById(dbh.MaTTDBH);
            dbh.TenTTDBH = tt?.TenTTDBH ?? "Chưa xác định";

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