using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using kindergarten.Models;

namespace kindergarten.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult ChuongTrinhHoc()
    {
        return View();
    }
    public IActionResult CoSoVatChat()
    {
        return View();
    }
    public IActionResult GioiThieu()
    {
        return View();
    }
    public IActionResult LienHe()
    {
        return View();
    }
    public IActionResult News()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    [HttpGet]
    public IActionResult DangKyTuVan()
    {
        return View();
    }

    [HttpPost]
    public IActionResult DangKyTuVan(string HoTen, string Email, string SoDienThoai, string NoiDung)
    {
        // 🔹 Tạm thời chỉ hiển thị thông báo thành công (sau có thể lưu DB)
        // Có thể ghi log hoặc lưu vào bảng DangKyTuVan trong database.
        Console.WriteLine($"Tư vấn từ: {HoTen} - {Email} - {SoDienThoai} - {NoiDung}");
        return RedirectToAction("DangKyTuVan", new { success = true });
    }

}
