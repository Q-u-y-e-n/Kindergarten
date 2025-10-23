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

            var user = await _context.TaiKhoans
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.TenDangNhap == model.TenDangNhap);

            if (user == null)
            {
                ViewBag.Error = "Tên đăng nhập không tồn tại.";
                return View(model);
            }

            if (user.MatKhau != hashedInput)
            {
                ViewBag.Error = "Sai mật khẩu. Vui lòng thử lại.";
                return View(model);
            }

            if (!user.TrangThai)
            {
                ViewBag.Error = "Tài khoản của bạn đã bị khóa.";
                return View(model);
            }

            // ✅ Tạo cookie xác thực
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.TenDangNhap),
                new Claim(ClaimTypes.Role, user.VaiTro ?? ""),
                new Claim("HoTen", user.HoTen ?? "")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            HttpContext.Session.SetString("VaiTro", user.VaiTro ?? "");
            HttpContext.Session.SetString("TenDangNhap", user.TenDangNhap);

            return RedirectToAction("RedirectByRole");
        }

        // ========== LOGOUT ==========
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
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
        public IActionResult AccessDenied() => View();

        // ========== HÀM HASH MẬT KHẨU ==========
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // ⚠️ Dùng Unicode để khớp với HASHBYTES trong SQL Server
                var bytes = sha256.ComputeHash(Encoding.Unicode.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
            }
        }

        // ========== [GET] REGISTER ==========
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        // ========== [POST] REGISTER ==========
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // ✅ Kiểm tra độ mạnh mật khẩu
            var passwordError = ValidatePasswordStrength(model.MatKhau);
            if (!string.IsNullOrEmpty(passwordError))
            {
                ViewBag.Error = passwordError;
                return View(model);
            }

            // ✅ Kiểm tra trùng tên đăng nhập hoặc email
            var existingUser = await _context.TaiKhoans
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.TenDangNhap == model.TenDangNhap || u.Email == model.Email);

            if (existingUser != null)
            {
                ViewBag.Error = "Tên đăng nhập hoặc email đã tồn tại.";
                return View(model);
            }

            // ✅ Hash mật khẩu
            string hashed = HashPassword(model.MatKhau);

            // ✅ Tạo tài khoản mới (mặc định là PhuHuynh)
            var taiKhoan = new TaiKhoan
            {
                TenDangNhap = model.TenDangNhap,
                MatKhau = hashed,
                Email = model.Email,
                HoTen = model.HoTen,
                VaiTro = "PhuHuynh",
                TrangThai = true,
                NgayTao = DateTime.Now
            };

            try
            {
                _context.TaiKhoans.Add(taiKhoan);
                await _context.SaveChangesAsync();
                Console.WriteLine($"✅ Đăng ký thành công: {taiKhoan.TenDangNhap}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi khi lưu DB: {ex.Message}");
                ViewBag.Error = "Có lỗi xảy ra khi lưu dữ liệu. Vui lòng thử lại.";
                return View(model);
            }

            ViewBag.Success = "🎉 Đăng ký thành công! Vui lòng đăng nhập.";
            return RedirectToAction("Login");
        }

        // ✅ HÀM KIỂM TRA ĐỘ MẠNH MẬT KHẨU
        private string? ValidatePasswordStrength(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return "Mật khẩu không được để trống.";

            if (password.Length < 8)
                return "Mật khẩu phải có ít nhất 8 ký tự.";

            if (!password.Any(char.IsUpper))
                return "Mật khẩu phải có ít nhất 1 chữ in hoa (A-Z).";

            if (!password.Any(char.IsLower))
                return "Mật khẩu phải có ít nhất 1 chữ thường (a-z).";

            if (!password.Any(char.IsDigit))
                return "Mật khẩu phải có ít nhất 1 chữ số (0-9).";

            if (!password.Any(ch => "!@#$%^&*()_+-=[]{}|;:,.<>?".Contains(ch)))
                return "Mật khẩu phải có ít nhất 1 ký tự đặc biệt (ví dụ: @, #, $, %).";

            return null; // ✅ Mật khẩu hợp lệ
        }
    }
}
