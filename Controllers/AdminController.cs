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
    }
}
