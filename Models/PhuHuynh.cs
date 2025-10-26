using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models
{
    [Table("PHUHUYNH")] // ✅ Tên bảng trùng SQL
    public class PhuHuynh
    {
        [Key]
        public int MaPH { get; set; }

        [Required, StringLength(100)]
        public string HoTen { get; set; } = string.Empty;

        [StringLength(50)]
        public string? QuanHe { get; set; } // Cha, Mẹ, Người giám hộ

        [StringLength(15)]
        public string? SoDienThoai { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(100)]
        public string? NgheNghiep { get; set; }

        // ❌ Không có cột TaiKhoanId trong bảng SQL nên bỏ đi
        // Nếu bạn muốn liên kết với bảng TaiKhoan thì phải ALTER TABLE SQL để thêm cột này

        // ✅ Quan hệ nhiều - nhiều với học sinh
        public ICollection<HOCSINH_PHUHUYNH>? HocSinhPhuHuynhs { get; set; }
    }
}
