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

            // 1. Lấy danh sách Tỉnh để hiển thị dropdown đầu tiên
            var listTinh = _db.Tinh_GetAll() ?? new List<Tinh>();
            ViewBag.Tinhs = new SelectList(listTinh, "MaTinh", "TenTinh");

            // 2. Khởi tạo danh sách Xã rỗng (vì chưa chọn Tỉnh nào)
            // Hoặc có thể để null, nhưng new List<Xa>() sẽ an toàn hơn cho SelectList
            // nhưng trong trường hợp này new lish<Xa> sẽ không đảm bảo được việc chọn tỉnh trước rồi chọn xã sau.
            // vì nó sẽ hiện ra toàn bộ xã nếu new lish<xa>
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

        // 1. Thêm Action này để Ajax gọi lấy danh sách xã
        [HttpGet]
        public IActionResult GetXaByTinh(int maTinh)
        {
            // Giả sử Xa_GetAll trả về List và trong model Xa có thuộc tính MaTinh
            // Bạn cần lọc danh sách xã theo maTinh được gửi lên
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

            // Xác định Tỉnh hiện tại của Nhà cung cấp (để chọn sẵn khi mở form)
            // tìm xem MaXa hiện tại thuộc MaTinh nào.
            // Tìm thông tin xã hiện tại -> lấy MaTinh của nó
            var currentXa = _db.Xa_GetAll().FirstOrDefault(x => x.MaXa == dbh.MaXa);
            int selectedMaTinh = currentXa != null ? currentXa.MaTinh : 0;

            // Lấy danh sách Xã thuộc Tỉnh hiện tại (chứ không lấy hết tất cả xã)
            var listXaOfTinh = _db.Xa_GetAll().Where(x => x.MaTinh == selectedMaTinh).ToList();

            // Truyền dữ liệu qua View
            ViewBag.Tinhs = new SelectList(listTinh, "MaTinh", "TenTinh", selectedMaTinh);
            ViewBag.Xas = new SelectList(listXaOfTinh, "MaXa", "TenXa", dbh.MaXa);

            // Xử lý khách hàng
            var khList = _db.KhachHang_GetAll() ?? new List<KhachHang>();
            // Lấy tất cả khách hàng và truyền qua ViewBag cho View sử dụng
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