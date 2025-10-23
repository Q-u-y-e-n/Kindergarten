using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using kindergarten.Data; // ⚠️ Sửa namespace cho khớp project của bạn
using kindergarten.Models;

var builder = WebApplication.CreateBuilder(args);

// =======================================
// 1️⃣ KẾT NỐI CƠ SỞ DỮ LIỆU
// =======================================
builder.Services.AddDbContext<KindergartenContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("kindergarten")));

// =======================================
// 2️⃣ XÁC THỰC COOKIE
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

// =======================================
// 4️⃣ CẤU HÌNH MVC & VIEW LOCATION
// =======================================
builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Clear();
        options.ViewLocationFormats.Add("/Views/{1}/{0}.cshtml");
        options.ViewLocationFormats.Add("/Views/Account/{0}.cshtml");
        options.ViewLocationFormats.Add("/Views/Admin/{0}.cshtml");
        options.ViewLocationFormats.Add("/Views/Parent/{0}.cshtml");
        options.ViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
    });

// =======================================
// 5️⃣ LOGGING (tuỳ chọn)
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// =======================================
// 6️⃣ BUILD APP
// =======================================
var app = builder.Build();

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

// ⚠️ Thứ tự quan trọng:
app.UseSession();         // Trước Authentication
app.UseAuthentication();  // Trước Authorization
app.UseAuthorization();

// =======================================
// 8️⃣ ĐỊNH NGHĨA ROUTES
// =======================================
app.MapControllerRoute(
    name: "admin",
    pattern: "Admin/{action=Index}/{id?}",
    defaults: new { controller = "Admin" }
);

app.MapControllerRoute(
    name: "phuhuynh",
    pattern: "PhuHuynh/{action=Index}/{id?}",
    defaults: new { controller = "PhuHuynh" }
);


app.MapControllerRoute(
    name: "account",
    pattern: "Account/{action=Login}/{id?}",
    defaults: new { controller = "Account" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"); // Mặc định vào trang login

// =======================================
app.Run();
