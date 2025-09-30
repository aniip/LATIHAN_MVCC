using Microsoft.AspNetCore.Mvc;
using LATIHAN_MVCC.Models; // Sudah DIKOREKSI

namespace LATIHAN_MVCC.Controllers
{
    public class ProfileController : Controller
    {
        // ACTION: TAMPILKAN DETAIL PROFIL (GET)
        public IActionResult Profile()
        {
            // SIMULASI MENGAMBIL DATA DARI DATABASE
            var userProfile = new ProfileViewModel
            {
                Id = 1,
                Username = "alexander_pierce",
                Email = "alex@adminlte.io",
                FullName = "Alexander Pierce",
                JoinDate = new System.DateTime(2023, 10, 25)
            };

            return View(userProfile);
        }

        // ACTION: TAMPILKAN FORM EDIT (GET)
        public IActionResult Edit()
        {
            // SIMULASI MENGAMBIL DATA YANG ADA UNTUK DIISI DI FORM
            var userProfile = new ProfileViewModel
            {
                Id = 1,
                Username = "alexander_pierce",
                Email = "alex@adminlte.io",
                FullName = "Alexander Pierce",
            };

            return View(userProfile);
        }

        // ACTION: PROSES SIMPAN FORM EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProfileViewModel model)
        {
            if (ModelState.IsValid) // Cek validasi data
            {
                // *** LOGIKA SIMPAN PERUBAHAN KE DATABASE DI SINI ***

                // Setelah berhasil, arahkan kembali ke halaman detail profil
                return RedirectToAction(nameof(Profile));
            }

            // Jika validasi gagal, kembalikan ke form
            return View(model);
        }
    }
}