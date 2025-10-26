using kindergarten.Data;
using kindergarten.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace kindergarten.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LopHocController : Controller
    {
        private readonly KindergartenContext _context;

        public LopHocController(KindergartenContext context)
        {
            _context = context;
        }

        // ======================
        // 1️⃣ Danh sách lớp học
        // ======================
        public async Task<IActionResult> Index()
        {
            var dsLop = await _context.LopHocs
                .Include(l => l.GiaoVienChuNhiem)
                .ToListAsync();

            return View("~/Views/Admin/LopHoc/Index.cshtml", dsLop);
        }

        // ======================
        // 2️⃣ Thêm mới (GET)
        // ======================
        public IActionResult Create()
        {
            LoadGiaoVien(); // Load dropdown giáo viên
            return View("~/Views/Admin/LopHoc/Create.cshtml");
        }

        // ======================
        // 3️⃣ Thêm mới (POST)
        // ======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LopHoc lop)
        {
            if (ModelState.IsValid)
            {
                _context.LopHocs.Add(lop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            LoadGiaoVien(lop.MaGVCN);
            return View("~/Views/Admin/LopHoc/Create.cshtml", lop);
        }

        // ======================
        // 4️⃣ Chỉnh sửa (GET)
        // ======================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var lop = await _context.LopHocs.FindAsync(id);
            if (lop == null) return NotFound();

            LoadGiaoVien(lop.MaGVCN);
            return View("~/Views/Admin/LopHoc/Edit.cshtml", lop);
        }

        // ======================
        // 5️⃣ Chỉnh sửa (POST)
        // ======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LopHoc lop)
        {
            if (id != lop.MaLop) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lop);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.LopHocs.Any(e => e.MaLop == lop.MaLop))
                        return NotFound();
                    else
                        throw;
                }
            }

            LoadGiaoVien(lop.MaGVCN);
            return View("~/Views/Admin/LopHoc/Edit.cshtml", lop);
        }

        // ======================
        // 6️⃣ Xóa lớp học (GET)
        // ======================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var lop = await _context.LopHocs
                .Include(l => l.GiaoVienChuNhiem)
                .FirstOrDefaultAsync(m => m.MaLop == id);

            if (lop == null) return NotFound();

            return View("~/Views/Admin/LopHoc/Delete.cshtml", lop);
        }

        // ======================
        // 7️⃣ Xóa lớp học (POST)
        // ======================
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int MaLop)
        {
            // Tìm lớp cần xóa
            var lop = await _context.LopHocs.FindAsync(MaLop);
            if (lop == null) return NotFound();

            // Tìm tất cả học sinh có MaLop này
            var hocSinhs = await _context.HocSinhs
                .Where(h => h.MaLop == MaLop)
                .ToListAsync();

            // Set MaLop của học sinh về null
            foreach (var hs in hocSinhs)
            {
                hs.MaLop = null;
            }

            // Lưu thay đổi học sinh trước
            await _context.SaveChangesAsync();

            // Sau đó xóa lớp
            _context.LopHocs.Remove(lop);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // ======================
        // 🔹 Helper: Load danh sách giáo viên
        // ======================
        private void LoadGiaoVien(int? selectedId = null)
        {
            var dsGV = _context.GiaoViens
                .Select(g => new SelectListItem
                {
                    Text = g.HoTen,
                    Value = g.MaGV.ToString(),
                    Selected = g.MaGV == selectedId
                })
                .ToList();

            ViewBag.DSGiaoVien = dsGV;
        }
    }
}
