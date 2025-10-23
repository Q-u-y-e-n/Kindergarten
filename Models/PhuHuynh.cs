using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models
{
    [Table("PhuHuynh")]
    public class PhuHuynh
    {
        [Key]
        public int MaPH { get; set; }

        [Required, StringLength(100)]
        public string HoTen { get; set; }

        [StringLength(50)]
        public string? QuanHe { get; set; } // Cha, Mẹ, Người giám hộ

        [StringLength(15)]
        public string? SoDienThoai { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(100)]
        public string? NgheNghiep { get; set; }

        // ✅ Liên kết với bảng TaiKhoan
        [ForeignKey("TaiKhoan")]
        public int TaiKhoanId { get; set; }
        public TaiKhoan TaiKhoan { get; set; }

        // ✅ Liên kết nhiều-nhiều với Học sinh
        public ICollection<HocSinhPhuHuynh>? HocSinhPhuHuynhs { get; set; }
    }
}
