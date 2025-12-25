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
            var nhanvien = _db.NhanVien_GetById(id);
            return View(nhanvien);
        }

        // GET: NhanVien/Create
        public IActionResult Create()
        {
            // 1. Lấy danh sách Tỉnh để hiển thị dropdown đầu tiên
            var listTinh = _db.Tinh_GetAll() ?? new List<Tinh>();
            ViewBag.Tinhs = new SelectList(listTinh, "MaTinh", "TenTinh");

            // 2. Khởi tạo danh sách Xã rỗng (vì chưa chọn Tỉnh nào)
            // Hoặc bạn có thể để null, nhưng new List<Xa>() sẽ an toàn hơn cho SelectList
            ViewBag.Xas = new SelectList(new List<Xa>(), "MaXa", "TenXa");

            return View();
        }

        // POST: NhanVien/Create
        [HttpPost]
        public IActionResult Create(NhanVien nv)
        {
            _db.NhanVien_Insert(nv);
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

        // GET: NhanVien/Edit/5
        public IActionResult Edit(int id)
        {
            var nv = _db.NhanVien_GetById(id);
            if (nv == null) return NotFound();

            // A. Lấy tất cả Tỉnh để đổ vào Dropdown Tỉnh
            // Giả sử bạn có hàm _db.Tinh_GetAll(), nếu chưa có bạn phải tạo thêm
            var listTinh = _db.Tinh_GetAll() ?? new List<Tinh>();

            // B. Xác định Tỉnh hiện tại của Nhà cung cấp (để chọn sẵn khi mở form)
            // Chúng ta phải tìm xem MaXa hiện tại thuộc MaTinh nào.
            // Cách làm: Tìm thông tin xã hiện tại -> lấy MaTinh của nó
            var currentXa = _db.Xa_GetAll().FirstOrDefault(x => x.MaXa == nv.MaXa);
            int selectedMaTinh = currentXa != null ? currentXa.MaTinh : 0;

            // C. Lấy danh sách Xã thuộc Tỉnh hiện tại (chứ không lấy hết tất cả xã)
            var listXaOfTinh = _db.Xa_GetAll().Where(x => x.MaTinh == selectedMaTinh).ToList();

            // D. Truyền dữ liệu qua View
            ViewBag.Tinhs = new SelectList(listTinh, "MaTinh", "TenTinh", selectedMaTinh);
            ViewBag.Xas = new SelectList(listXaOfTinh, "MaXa", "TenXa", nv.MaXa);

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