using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kindergarten.Controllers
{
    [Authorize(Roles = "PhuHuynh")]
    public class PhuHuynhController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Trang phụ huynh";
            ViewBag.TenPhuHuynh = User.Identity?.Name ?? "Phụ huynh";
            return View();
        }

        public IActionResult ThongTinCon()
        {
            return View();
        }
        // Trang chủ phụ huynh


        // Điểm danh
        public IActionResult DiemDanh()
        {
            return View();
        }

        // Kết quả học tập
        public IActionResult KetQua()
        {
            return View();
        }

        // Sức khỏe
        public IActionResult SucKhoe()
        {
            return View();
        }

        // Hoạt động ngoại khóa
        public IActionResult HoatDong()
        {
            return View();
        }

        // Học phí
        public IActionResult HocPhi()
        {
            return View();
        }

        // Thông báo
        public IActionResult ThongBao()
        {
            return View();
        }
    }
}
