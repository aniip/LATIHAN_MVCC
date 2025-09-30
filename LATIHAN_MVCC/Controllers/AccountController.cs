using System.Security.Claims; // Untuk Claim dan ClaimsIdentity
using System.Threading.Tasks; // Untuk async Task
using Microsoft.AspNetCore.Authentication; // Untuk SignInAsync dan SignOutAsync
using Microsoft.AspNetCore.Mvc;

namespace LATIHAN_MVCC.Controllers
{
    public class AccountController : Controller
    {
        // Constructor tidak ada di sini untuk contoh ini, jika ada biarkan saja

        [HttpGet] // Method untuk menampilkan halaman Login (saat pertama kali diakses)
        public IActionResult Login()
        {
            // Jika user sudah login, langsung redirect ke dashboard
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(); // Menampilkan Views/Account/Login.cshtml
        }

        [HttpPost] // Method ini akan dipanggil saat form Login disubmit
        public async Task<IActionResult> Login(string email, string password, string returnUrl = null)
        {
            // --- LOGIKA AUTENTIKASI SEMENTARA (Ganti dengan Database/Identity di produksi) ---
            // Contoh sederhana: user "admin@example.com" dengan password "admin123"
            if (email == "admin@example.com" && password == "admin123")
            {
                // Buat daftar Claims (informasi tentang user yang berhasil login)
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, email), // Nama user
                    new Claim(ClaimTypes.Email, email), // Email user
                    new Claim(ClaimTypes.Role, "Administrator"), // Contoh Role
                    // Anda bisa menambahkan Claim lain seperti ID pengguna dari database
                };

                // Buat ClaimsIdentity
                var claimsIdentity = new ClaimsIdentity(
                    claims, "Cookies"); // "Cookies" adalah nama skema autentikasi yang Anda definisikan di Program.cs

                // Properti Autentikasi (opsional)
                var authProperties = new AuthenticationProperties
                {
                    // IsPersistent = true, // Jika ingin cookie bertahan setelah browser ditutup
                    // ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // Tentukan kapan cookie kedaluwarsa secara eksplisit
                };

                // Lakukan proses SignIn
                await HttpContext.SignInAsync(
                    "Cookies", // Skema autentikasi
                    new ClaimsPrincipal(claimsIdentity), // Principal yang berisi Claims
                    authProperties); // Properti autentikasi

                // Redirect ke URL yang dituju sebelumnya jika ada (returnUrl)
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home"); // Default redirect ke dashboard
            }

            // Jika otentikasi gagal
            ModelState.AddModelError(string.Empty, "Email atau Password salah.");
            return View(); // Kembali ke halaman login dengan pesan error
        }

        // Action untuk Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies"); // Hapus cookie autentikasi
            return RedirectToAction("Login", "Account"); // Redirect kembali ke halaman login
        }

        [HttpGet] // Action untuk menampilkan halaman Forgot Password
        public IActionResult ForgotPassword()
        {
            return View(); // Menampilkan Views/Account/ForgotPassword.cshtml
        }

        [HttpPost] // Contoh action untuk proses Forgot Password (Anda bisa tambahkan logika pengiriman email dll.)
        public IActionResult ForgotPassword(string email)
        {
            // Logika untuk mengirim link reset password ke email
            // Untuk demo, kita langsung redirect ke halaman recover password
            return RedirectToAction("RecoverPassword", "Account");
        }

        [HttpGet] // Action untuk menampilkan halaman Recover Password
        public IActionResult RecoverPassword()
        {
            return View(); // Menampilkan Views/Account/RecoverPassword.cshtml
        }

        [HttpPost] // Contoh action untuk proses Recover Password
        public IActionResult RecoverPassword(string newPassword, string confirmPassword)
        {
            // Logika untuk mengubah password di database
            // Untuk demo, kita redirect ke login setelah berhasil
            return RedirectToAction("Login", "Account");
        }

        [HttpGet] // Halaman jika akses ditolak (karena tidak punya Role yang sesuai)
        public IActionResult AccessDenied()
        {
            return View(); // Anda perlu membuat View Views/Account/AccessDenied.cshtml
        }
    }
}