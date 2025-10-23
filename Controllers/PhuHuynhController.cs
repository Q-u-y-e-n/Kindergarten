using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kindergarten.Controllers
{
    [Authorize(Roles = "PhuHuynh")]
    public class ParentController : Controller
    {
        // Trang chính của phụ huynh
        public IActionResult Index()
        {
            ViewBag.HoTen = User.FindFirst("HoTen")?.Value ?? "Phụ huynh";
            return View();
        }

        // Trang xem thông tin học sinh của phụ huynh
        public IActionResult ThongTinCon()
        {
            return View();
        }

        // Trang xem thông báo, ví dụ
        public IActionResult ThongBao()
        {
            return View();
        }
    }
}
