using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using kindergarten.Data;
using kindergarten.Models;

var builder = WebApplication.CreateBuilder(args);

//
// =======================================
// 1️⃣ KẾT NỐI CSDL
// =======================================
builder.Services.AddDbContext<KindergartenContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Kindergarten")));
//
// ⚠️ Kiểm tra lại trong appsettings.json:
// "ConnectionStrings": {
//     "Kindergarten": "Server=.;Database=KindergartenPrivate;Trusted_Connection=True;TrustServerCertificate=True;"
// }
//

//
// =======================================
// 2️⃣ CẤU HÌNH COOKIE AUTHENTICATION
// =======================================
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.Name = "KinderAuthCookie";
    });

//
// =======================================
// 3️⃣ BẬT SESSION
// =======================================
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//
// =======================================
// 4️⃣ CẤU HÌNH MVC + RAZOR VIEW
// =======================================
builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        // ⚙️ Đường dẫn view mặc định
        options.ViewLocationFormats.Clear();
        options.ViewLocationFormats.Add("/Views/{1}/{0}.cshtml");
        options.ViewLocationFormats.Add("/Views/Admin/{0}.cshtml");
        options.ViewLocationFormats.Add("/Views/Account/{0}.cshtml");
        options.ViewLocationFormats.Add("/Views/Parent/{0}.cshtml");
        options.ViewLocationFormats.Add("/Views/Shared/{0}.cshtml");

        // ⚙️ Hỗ trợ tìm view trong thư mục Areas (Admin, Parent, Teacher, ...)
        options.AreaViewLocationFormats.Clear();
        options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
        options.AreaViewLocationFormats.Add("/Areas/{2}/Views/Shared/{0}.cshtml");
        options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
    });

//
// =======================================
// 5️⃣ LOGGING
// =======================================
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

//
// =======================================
// 6️⃣ TẠO APP
// =======================================
var app = builder.Build();

//
// =======================================
// 7️⃣ CẤU HÌNH PIPELINE
// =======================================
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ⚠️ Thứ tự quan trọng: Session trước Authentication
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

//
// =======================================
// 8️⃣ MAP CONTROLLER ROUTE
// =======================================

// Route cho các khu vực (Areas)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

// Route mặc định: Account/Login
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

//
// =======================================
// 9️⃣ CHẠY APP
// =======================================
app.Run();
