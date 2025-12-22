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
    public class NhaCCsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NhaCCsController(ApplicationDbContext db) => _db = db;

        // GET: NhaCCs
        public IActionResult Index() => View(_db.NhaCC_GetAll());
        // GET: NhaCCs/Details/5
        public IActionResult Details(int id)
        {
            var nhaCC = _db.NhaCC_GetById(id);
            if (nhaCC == null)
            {
                return NotFound();
            }

            // --- ĐOẠN CODE THÊM MỚI ---
            // 1. Tìm thông tin Xã dựa vào MaXa của Nhà cung cấp
            var xa = _db.Xa_GetAll().FirstOrDefault(x => x.MaXa == nhaCC.MaXa);

            // 2. Tìm thông tin Tỉnh dựa vào MaTinh của Xã đó (nếu tìm thấy xã)
            string tenXa = "Chưa cập nhật";
            string tenTinh = "Chưa cập nhật";

            if (xa != null)
            {
                tenXa = xa.TenXa;

                // Giả sử bạn có hàm lấy danh sách tỉnh giống các bước trước
                var tinh = _db.Tinh_GetAll().FirstOrDefault(t => t.MaTinh == xa.MaTinh);
                if (tinh != null)
                {
                    tenTinh = tinh.TenTinh;
                }
            }

            // 3. Truyền dữ liệu qua View bằng ViewBag
            ViewBag.TenXa = tenXa;
            ViewBag.TenTinh = tenTinh;

            return View(nhaCC);
        }

        // GET: NhaCCs/Create
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

        // POST: NhaCCs/Create
        [HttpPost]
        public IActionResult Create(NhaCC nhaCC)
        {
            _db.NhaCC_Insert(nhaCC);
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

        // GET: NhaCCs/Edit/5
        public IActionResult Edit(int id)
        {
            var nhaCC = _db.NhaCC_GetById(id);
            if (nhaCC == null) return NotFound();

            // A. Lấy tất cả Tỉnh để đổ vào Dropdown Tỉnh
            // Giả sử bạn có hàm _db.Tinh_GetAll(), nếu chưa có bạn phải tạo thêm
            var listTinh = _db.Tinh_GetAll() ?? new List<Tinh>();

            // B. Xác định Tỉnh hiện tại của Nhà cung cấp (để chọn sẵn khi mở form)
            // Chúng ta phải tìm xem MaXa hiện tại thuộc MaTinh nào.
            // Cách làm: Tìm thông tin xã hiện tại -> lấy MaTinh của nó
            var currentXa = _db.Xa_GetAll().FirstOrDefault(x => x.MaXa == nhaCC.MaXa);
            int selectedMaTinh = currentXa != null ? currentXa.MaTinh : 0;

            // C. Lấy danh sách Xã thuộc Tỉnh hiện tại (chứ không lấy hết tất cả xã)
            var listXaOfTinh = _db.Xa_GetAll().Where(x => x.MaTinh == selectedMaTinh).ToList();

            // D. Truyền dữ liệu qua View
            ViewBag.Tinhs = new SelectList(listTinh, "MaTinh", "TenTinh", selectedMaTinh);
            ViewBag.Xas = new SelectList(listXaOfTinh, "MaXa", "TenXa", nhaCC.MaXa);

            return View(nhaCC);
        }

        // POST: NhaCCs/Edit/5
        [HttpPost]
        public IActionResult Edit(NhaCC nhaCC)
        {
            _db.NhaCC_Update(nhaCC);
            return RedirectToAction(nameof(Index));
        }

        // GET: NhaCCs/Delete/5
        public IActionResult Delete(int id)
        {
            var nhaCC = _db.NhaCC_GetById(id);
            return View(nhaCC);
        }

        // POST: NhaCCs/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _db.NhaCC_Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
