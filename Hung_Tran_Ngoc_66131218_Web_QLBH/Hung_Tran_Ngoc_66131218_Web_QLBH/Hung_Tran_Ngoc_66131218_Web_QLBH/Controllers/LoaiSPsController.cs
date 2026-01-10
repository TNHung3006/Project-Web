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
    public class LoaiSPsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LoaiSPsController(ApplicationDbContext db) => _db = db;

        // 1. SỬA HÀM INDEX: Lấy danh sách Loại và điền Tên Nhóm vào
        public IActionResult Index()
        {
            // A. Lấy danh sách Loại sản phẩm
            var listLoai = _db.LoaiSP_GetAll();

            // B. Lấy danh sách Nhóm sản phẩm (để tra cứu tên)
            var listNhom = _db.NhomSP_GetAll();

            // C. Vòng lặp: Tìm tên nhóm tương ứng với mã và gán vào
            foreach (var item in listLoai)
            {
                // Tìm nhóm có MaNhomSP trùng với item.MaNhomSP
                var nhom = listNhom.FirstOrDefault(n => n.MaNhomSP == item.MaNhomSP);

                // Nếu tìm thấy thì lấy tên, không thấy thì ghi "Khác"
                item.TenNhomSP = nhom?.TenNhomSP ?? "Khác";
            }

            return View(listLoai);
        }

        // GET: LoaiSPs/Create
        public IActionResult Create()
        {
            var nspList = _db.NhomSP_GetAll() ?? new List<NhomSP>();
            ViewBag.NhomSPs = new SelectList(nspList, "MaNhomSP", "TenNhomSP");
            return View();
        }

        [HttpPost]
        public IActionResult Create(LoaiSP loai)
        {
            _db.LoaiSP_Insert(loai);
            return RedirectToAction(nameof(Index));
        }

        // GET: LoaiSPs/Edit/5
        public IActionResult Edit(int id)
        {
            var loai = _db.LoaiSP_GetById(id);
            if (loai == null) return NotFound();

            var nspList = _db.NhomSP_GetAll() ?? new List<NhomSP>();
            ViewBag.NhomSPs = new SelectList(nspList, "MaNhomSP", "TenNhomSP", loai.MaNhomSP);
            return View(loai);
        }

        [HttpPost]
        public IActionResult Edit(LoaiSP loai)
        {
            _db.LoaiSP_Update(loai);
            return RedirectToAction(nameof(Index));
        }

        // 2. SỬA HÀM DETAILS: Lấy thông tin chi tiết và điền Tên Nhóm
        public IActionResult Details(int id)
        {
            var loai = _db.LoaiSP_GetById(id);
            if (loai == null) return NotFound();

            // Tìm tên nhóm sản phẩm để hiển thị
            var nhom = _db.NhomSP_GetById(loai.MaNhomSP);
            loai.TenNhomSP = nhom?.TenNhomSP ?? "Khác";

            return View(loai);
        }

        // GET: Delete
        public IActionResult Delete(int id)
        {
            var loai = _db.LoaiSP_GetById(id);
            if (loai == null) return NotFound();

            // Cũng cần lấy tên nhóm để hiển thị lúc hỏi xác nhận xóa
            var nhom = _db.NhomSP_GetById(loai.MaNhomSP);
            loai.TenNhomSP = nhom?.TenNhomSP ?? "Khác";

            return View(loai);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _db.LoaiSP_Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}