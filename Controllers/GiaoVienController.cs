using kindergarten.Data;
using kindergarten.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kindergarten.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GiaoVienController : Controller
    {
        private readonly KindergartenContext _context;

        public GiaoVienController(KindergartenContext context)
        {
            _context = context;
        }

        // ======================
        // Index: danh sách giáo viên
        // GET: /Admin/GiaoVien
        // ======================
        public async Task<IActionResult> Index()
        {
            var giaoViens = await _context.GiaoViens.ToListAsync();
            return View("~/Views/Admin/GiaoVien/Index.cshtml", giaoViens);
        }

        // ======================
        // Create - GET
        // ======================
        public IActionResult Create()
        {
            LoadGioiTinh();
            return View("~/Views/Admin/GiaoVien/Create.cshtml");
        }

        // ======================
        // Create - POST
        // ======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GiaoVien giaoVien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(giaoVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            LoadGioiTinh(giaoVien.GioiTinh);
            return View("~/Views/Admin/GiaoVien/Create.cshtml", giaoVien);
        }

        // ======================
        // Edit - GET
        // ======================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var giaoVien = await _context.GiaoViens.FindAsync(id);
            if (giaoVien == null) return NotFound();

            LoadGioiTinh(giaoVien.GioiTinh);
            return View("~/Views/Admin/GiaoVien/Edit.cshtml", giaoVien);
        }

        // ======================
        // Edit - POST
        // ======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GiaoVien giaoVien)
        {
            if (id != giaoVien.MaGV) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(giaoVien);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.GiaoViens.Any(e => e.MaGV == giaoVien.MaGV))
                        return NotFound();
                    else
                        throw;
                }
            }

            LoadGioiTinh(giaoVien.GioiTinh);
            return View("~/Views/Admin/GiaoVien/Edit.cshtml", giaoVien);
        }

        // ======================
        // Delete - GET
        // ======================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var giaoVien = await _context.GiaoViens.FirstOrDefaultAsync(g => g.MaGV == id);
            if (giaoVien == null) return NotFound();

            return View("~/Views/Admin/GiaoVien/Delete.cshtml", giaoVien);
        }

        // ======================
        // Delete - POST
        // ======================
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int MaGV)
        {
            // 1️⃣ Lấy giáo viên cần xóa
            var giaoVien = await _context.GiaoViens.FindAsync(MaGV);
            if (giaoVien == null) return NotFound();

            // 2️⃣ Lấy tất cả lớp mà giáo viên này đang dạy
            var lopChuNhiems = await _context.LopHocs
                .Where(l => l.MaGVCN == MaGV)
                .ToListAsync();

            // 3️⃣ Bỏ liên kết giáo viên chủ nhiệm
            foreach (var lop in lopChuNhiems)
            {
                lop.MaGVCN = null; // ⚠️ Đảm bảo cột MaGVCN cho phép NULL
                _context.Update(lop);
            }

            // 4️⃣ Xóa giáo viên
            _context.GiaoViens.Remove(giaoVien);

            // 5️⃣ Lưu thay đổi
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }




        // ======================
        // Helper: kiểm tra tồn tại
        // ======================
        private bool GiaoVienExists(int id)
        {
            return _context.GiaoViens.Any(e => e.MaGV == id);
        }

        // ======================
        // Helper: Load giới tính cho dropdown
        // ======================
        private void LoadGioiTinh(string selectedValue = null)
        {
            ViewBag.GioiTinhList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Nam", Value = "Nam", Selected = selectedValue == "Nam" },
                new SelectListItem { Text = "Nữ", Value = "Nữ", Selected = selectedValue == "Nữ" }
            };
        }
    }
}
