using System;
using System.ComponentModel.DataAnnotations;

namespace LATIHAN_MVCC.Models // Sudah DIKOREKSI
{
    // Ini adalah data yang akan ditransfer antara Controller dan View
    public class ProfileViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama Pengguna wajib diisi.")]
        [Display(Name = "Nama Pengguna")]
        public string Username { get; set; } // Biasanya tidak bisa diedit

        [Required(ErrorMessage = "Alamat email wajib diisi.")]
        [EmailAddress(ErrorMessage = "Format alamat email tidak valid.")]
        public string Email { get; set; }

        [Display(Name = "Nama Lengkap")]
        [StringLength(100, ErrorMessage = "Nama lengkap maksimal 100 karakter.")]
        public string FullName { get; set; }

        [Display(Name = "Tanggal Bergabung")]
        public DateTime JoinDate { get; set; } // Hanya untuk tampilan
    }
}