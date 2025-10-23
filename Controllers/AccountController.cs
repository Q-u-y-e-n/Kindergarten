using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using kindergarten.Data;
using kindergarten.Models;

namespace kindergarten.Controllers
{
    public class AccountController : Controller
    {
        private readonly KindergartenContext _context;

        public AccountController(KindergartenContext context)
        {
            _context = context;
        }

        // ========== [GET] LOGIN ==========
        [HttpGet]
        public IActionResult Login()
        {
            // Nếu đã đăng nhập → điều hướng theo vai trò luôn
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("RedirectByRole");

            return View(new LoginViewModel());
        }

        // ========== [POST] LOGIN ==========
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string hashedInput = HashPassword(model.MatKhau);
            Console.WriteLine($"🔹 Hash nhập vào: {hashedInput}");

            var user = await _context.TaiKhoans
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.TenDangNhap == model.TenDangNhap);

            if (user == null)
            {
                ViewBag.Error = "Không tìm thấy tên đăng nhập.";
                return View(model);
            }

            Console.WriteLine($"🔹 DB Hash: {user.MatKhau}");

            if (user.MatKhau != hashedInput)
            {
                ViewBag.Error = "Sai mật khẩu.";
                return View(model);
            }

            if (!user.TrangThai)
            {
                ViewBag.Error = "Tài khoản bị khóa.";
                return View(model);
            }

            // ===== Đăng nhập thành công =====
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.TenDangNhap),
                new Claim(ClaimTypes.Role, user.VaiTro ?? ""),
                new Claim("HoTen", user.HoTen ?? "")
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // Giữ cookie sau khi đóng trình duyệt
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
            };

            // Tạo cookie đăng nhập
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            // Lưu session (nếu bạn muốn sử dụng)
            HttpContext.Session.SetString("VaiTro", user.VaiTro ?? "");
            HttpContext.Session.SetString("TenDangNhap", user.TenDangNhap);

            // Chuyển hướng theo vai trò
            return RedirectToAction("RedirectByRole");
        }

        // ========== LOGOUT ==========
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // ========== PHÂN QUYỀN & ĐIỀU HƯỚNG ==========
        [Authorize]
        public IActionResult RedirectByRole()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (string.IsNullOrEmpty(role))
                return RedirectToAction("Login");

            return role switch
            {
                "Admin" => RedirectToAction("Dashboard", "Admin"),
                "PhuHuynh" => RedirectToAction("Index", "PhuHuynh"),
                "GiaoVien" => RedirectToAction("Index", "GiaoVien"),
                "KeToan" => RedirectToAction("Index", "HocPhi"),
                "YTe" => RedirectToAction("Index", "SucKhoe"),
                _ => RedirectToAction("Index", "Home"),
            };
        }

        // ========== ACCESS DENIED ==========
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        // ========== HÀM HASH MẬT KHẨU ==========
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // ⚠️ Dùng Encoding.Unicode để khớp với HASHBYTES('SHA2_256', N'...')
                var bytes = sha256.ComputeHash(Encoding.Unicode.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}
