var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// --- START: Tambahkan Konfigurasi Authentication Services di sini ---
builder.Services.AddAuthentication("Cookies") // Nama skema autentikasi
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Account/Login"; // Halaman login Anda
        options.AccessDeniedPath = "/Account/AccessDenied"; // Halaman jika akses ditolak (opsional)
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Durasi cookie login (misal 30 menit)
        options.SlidingExpiration = true; // Cookie akan refresh jika user aktif
    });

builder.Services.AddAuthorization(); // Tetap butuh ini untuk otorisasi
// --- END: Tambahkan Konfigurasi Authentication Services di sini ---


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// --- START: Tambahkan Middleware Authentication di sini, sebelum app.UseAuthorization() ---
app.UseAuthentication(); // Sangat penting: Mengaktifkan middleware autentikasi
// --- END: Tambahkan Middleware Authentication di sini ---

app.UseRouting();

app.UseAuthorization(); // Ini sudah ada, biarkan

// Jika app.MapStaticAssets() Anda melakukan hal yang sama dengan app.UseStaticFiles(),
// maka app.UseStaticFiles() harusnya ditaruh DI AWAL, sebelum UseRouting().
// Jika app.MapStaticAssets() adalah sesuatu yang khusus, pertahankan lokasinya.
// Jika itu hanya alias untuk UseStaticFiles(), pindahkan ke atas.
// Contoh: app.UseStaticFiles(); // Jika Anda tidak punya MapStaticAssets kustom
app.MapStaticAssets(); // Asumsi ini adalah extension method Anda untuk static files

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets(); // Asumsi ini juga extension method Anda

app.Run();