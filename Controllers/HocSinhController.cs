using kindergarten.Data;
using kindergarten.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace kindergarten.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HocSinhController : Controller
    {
        private readonly KindergartenContext _context;

        public HocSinhController(KindergartenContext context)
        {
            _context = context;
        }

        // 🟢 1️⃣ Danh sách học sinh
        public async Task<IActionResult> Index()
        {
            var dsHocSinh = await _context.HocSinhs
                .Include(h => h.LopHoc)
                .ToListAsync();

            return View("~/Views/Admin/HocSinh/Index.cshtml", dsHocSinh);
        }

        // 🟢 2️⃣ GET - Thêm học sinh
        public IActionResult Create()
        {
            LoadDanhSachLop();
            return View("~/Views/Admin/HocSinh/Create.cshtml");
        }

        // 🟢 3️⃣ POST - Thêm học sinh
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HocSinh hocSinh)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hocSinh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            LoadDanhSachLop(hocSinh.MaLop);
            return View("~/Views/Admin/HocSinh/Create.cshtml", hocSinh);
        }

        // 🟡 4️⃣ GET - Sửa học sinh
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var hocSinh = await _context.HocSinhs.FindAsync(id);
            if (hocSinh == null) return NotFound();

            LoadDanhSachLop(hocSinh.MaLop);
            return View("~/Views/Admin/HocSinh/Edit.cshtml", hocSinh);
        }

        // 🟡 5️⃣ POST - Sửa học sinh
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HocSinh hocSinh)
        {
            if (id != hocSinh.MaHS) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hocSinh);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.HocSinhs.Any(e => e.MaHS == hocSinh.MaHS))
                        return NotFound();
                    else
                        throw;
                }
            }

            LoadDanhSachLop(hocSinh.MaLop);
            return View("~/Views/Admin/HocSinh/Edit.cshtml", hocSinh);
        }

        // 🔴 6️⃣ GET - Xóa học sinh
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var hocSinh = await _context.HocSinhs
                .Include(h => h.LopHoc)
                .FirstOrDefaultAsync(m => m.MaHS == id);

            if (hocSinh == null) return NotFound();

            return View("~/Views/Admin/HocSinh/Delete.cshtml", hocSinh);
        }

        // 🔴 7️⃣ POST - Xác nhận xóa
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int MaHS)
        {
            var hocSinh = await _context.HocSinhs
                .Include(h => h.LopHoc)
                .Include(h => h.DiemDanhs)
                .Include(h => h.KetQuas)
                .Include(h => h.HoSoSucKhoes)
                .Include(h => h.HocSinhPhuHuynhs!)
                    .ThenInclude(hp => hp.PhuHuynh)
                .FirstOrDefaultAsync(m => m.MaHS == MaHS);

            if (hocSinh == null)
                return NotFound();

            // Xóa các bản ghi liên quan trong THAMGIA_HD
            var thamGiaList = await _context.ThamGiaHDs
                .Where(t => t.MaHS == MaHS)
                .ToListAsync();

            if (thamGiaList.Any())
                _context.ThamGiaHDs.RemoveRange(thamGiaList);

            if (hocSinh.DiemDanhs?.Any() == true)
                _context.DiemDanhs.RemoveRange(hocSinh.DiemDanhs);

            if (hocSinh.KetQuas?.Any() == true)
                _context.KetQuas.RemoveRange(hocSinh.KetQuas);

            if (hocSinh.HoSoSucKhoes?.Any() == true)
                _context.HoSoSucKhoes.RemoveRange(hocSinh.HoSoSucKhoes);

            if (hocSinh.HocSinhPhuHuynhs?.Any() == true)
            {
                foreach (var hsp in hocSinh.HocSinhPhuHuynhs)
                {
                    var phuHuynh = hsp.PhuHuynh;

                    bool conHocSinhKhac = await _context.HocSinhPhuHuynhs
                        .AnyAsync(x => x.MaPH == phuHuynh.MaPH && x.MaHS != MaHS);

                    if (!conHocSinhKhac)
                        _context.PhuHuynhs.Remove(phuHuynh);
                }

                _context.HocSinhPhuHuynhs.RemoveRange(hocSinh.HocSinhPhuHuynhs);
            }

            hocSinh.MaLop = null;
            _context.HocSinhs.Remove(hocSinh);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // 🧩 Helper: Load danh sách lớp học cho dropdown
        private void LoadDanhSachLop(int? selectedId = null)
        {
            var dsLop = _context.LopHocs
                .Select(l => new SelectListItem
                {
                    Text = l.TenLop,
                    Value = l.MaLop.ToString(),
                    Selected = (selectedId != null && l.MaLop == selectedId)
                })
                .ToList();

            ViewBag.DSLop = dsLop;
        }
    }
}
