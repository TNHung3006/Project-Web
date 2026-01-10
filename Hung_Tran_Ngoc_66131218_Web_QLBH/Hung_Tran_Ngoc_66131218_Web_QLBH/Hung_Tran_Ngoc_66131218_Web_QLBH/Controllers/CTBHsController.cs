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
    public class CTBHsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CTBHsController(ApplicationDbContext db) => _db = db;

        // GET: CTBHs
        public IActionResult Index()
        {
            // 1. Lấy danh sách chi tiết thô
            var listCTBH = _db.CTBH_GetAll();

            // 2. Lấy các danh sách tham chiếu để tra cứu (Sản phẩm & Đơn hàng)
            // Lưu ý: SanPham_GetAll truyền null để lấy tất cả
            var listSP = _db.SanPham_GetAll(null);
            var listDonHang = _db.DonBanHang_GetAll(); // Hàm này cần có trong DbContext của bạn

            // 3. MAP DỮ LIỆU THỦ CÔNG cho các thuộc tính [NotMapped]
            foreach (var item in listCTBH)
            {
                // a. Tìm Tên Sản Phẩm
                var sp = listSP.FirstOrDefault(x => x.MaSP == item.MaSP);
                item.TenSP = sp != null ? sp.TenSP : "SP đã xóa";

                // b. Tìm Ngày Bán (từ bảng Đơn Bán Hàng)
                var donHang = listDonHang.FirstOrDefault(x => x.MaDBH == item.MaDBH);
                // Giả sử DonBanHang có thuộc tính NgayBan
                item.NgayBan = donHang != null ? donHang.NgayBan : DateTime.MinValue;

                // c. Tính Thành Tiền (Số lượng * Đơn giá)
                item.ThanhTien = item.SLB * item.DGB;
            }

            // 4. Logic tính tổng tiền theo nhóm (Giữ nguyên code cũ của bạn)
            var tongTienDict = new Dictionary<int, decimal>();
            var listMaDonHang = listCTBH.Select(x => x.MaDBH).Distinct().ToList();

            foreach (var maDBH in listMaDonHang)
            {
                decimal tongTien = _db.CTBH_TongThanhTienTheoID(maDBH);
                tongTienDict.Add(maDBH, tongTien);
            }

            ViewBag.TongTienMap = tongTienDict;

            return View(listCTBH);
        }

        // GET: CTBHs/Create
        public IActionResult Create()
        {
            // Xử lý MaDBH (Đơn Bán Hàng)
            var dbhList = _db.DonBanHang_GetAll() ?? new List<DonBanHang>();
            ViewBag.DonBanHangs = new SelectList(dbhList, "MaDBH", "MaDBH");

            // Xử lý MaSP (Sản Phẩm)
            var spList = _db.SanPham_GetAll(null) ?? new List<SanPham>();
            ViewBag.SanPhams = new SelectList(spList, "MaSP", "TenSP");

            return View();
        }

        [HttpPost]
        public IActionResult Create(CTBH ctbh)
        {
            // 1. Kiểm tra xem bản ghi (MaDBH, MaSP) này đã tồn tại chưa
            var existingItem = _db.CTBH_GetById(ctbh.MaDBH, ctbh.MaSP);

            if (existingItem != null)
            {
                // 2. Nếu tồn tại: Cập nhật Số Lượng và Đơn Giá
                existingItem.SLB += ctbh.SLB;
                existingItem.DGB = ctbh.DGB;

                _db.CTBH_Update(existingItem);
            }
            else
            {
                // 3. Nếu chưa tồn tại: Thực hiện INSERT
                _db.CTBH_Insert(ctbh);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: CTBHs/Edit?
        public IActionResult Edit(int maDBH, int maSP)
        {
            var ctbh = _db.CTBH_GetById(maDBH, maSP);
            if (ctbh == null) return NotFound();

            // Xử lý MaDBH (Đơn Bán Hàng)
            var dbhList = _db.DonBanHang_GetAll() ?? new List<DonBanHang>();
            ViewBag.DonBanHangs = new SelectList(dbhList, "MaDBH", "MaDBH", ctbh.MaDBH);

            // Xử lý MaSP (Sản Phẩm)
            var spList = _db.SanPham_GetAll(null) ?? new List<SanPham>();
            ViewBag.SanPhams = new SelectList(spList, "MaSP", "TenSP", ctbh.MaSP);

            return View(ctbh);
        }

        [HttpPost]
        public IActionResult Edit(CTBH ctbh)
        {
            _db.CTBH_Update(ctbh);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int maDBH, int maSP)
        {
            var ctbh = _db.CTBH_GetById(maDBH, maSP);

            // --- BỔ SUNG: Hiển thị tên SP và Thành tiền trong trang Details ---
            if (ctbh != null)
            {
                var sp = _db.SanPham_GetAll(null).FirstOrDefault(x => x.MaSP == ctbh.MaSP);
                ctbh.TenSP = sp?.TenSP;
                ctbh.ThanhTien = ctbh.SLB * ctbh.DGB;
            }
            // ----------------------------------------------------------------

            return View(ctbh);
        }

        public IActionResult Delete(int maDBH, int maSP)
        {
            var ctbh = _db.CTBH_GetById(maDBH, maSP);

            // --- BỔ SUNG: Hiển thị tên SP khi xóa để dễ nhìn ---
            if (ctbh != null)
            {
                var sp = _db.SanPham_GetAll(null).FirstOrDefault(x => x.MaSP == ctbh.MaSP);
                ctbh.TenSP = sp?.TenSP;
            }
            // --------------------------------------------------

            return View(ctbh);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int maDBH, int maSP)
        {
            _db.CTBH_Delete(maDBH, maSP);
            return RedirectToAction(nameof(Index));
        }
    }
}