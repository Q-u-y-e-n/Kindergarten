using Microsoft.AspNetCore.Mvc;

namespace kindergarten.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("Login", "Account");

            ViewBag.User = HttpContext.Session.GetString("Username");
            return View();
        }
        public IActionResult Dashboard()
        {
            return View(); // /Views/Admin/Dashboard.cshtml
        }

        public IActionResult HocSinh() => View();
        public IActionResult LopHoc() => View();
        public IActionResult BaoCao() => View();
        public IActionResult GiaoVien() => View();
        public IActionResult SucKhoe() => View();
        public IActionResult HoatDong() => View();
        public IActionResult ThucDon() => View();
        public IActionResult DiemDanh() => View();

    }
}
