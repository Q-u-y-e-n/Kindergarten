using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models
{
    [Table("GiaoVien")]
    public class GiaoVien
    {
        [Key]
        public int MaGV { get; set; }

        [Required, StringLength(100)]
        public string HoTen { get; set; } = string.Empty;

        [StringLength(10)]
        public string? GioiTinh { get; set; }

        public DateTime? NgaySinh { get; set; }

        [StringLength(255)]
        public string? DiaChi { get; set; }

        [StringLength(20)]
        public string? SoDienThoai { get; set; }

        [StringLength(100)]
        public string? TrinhDo { get; set; }

        // 🔹 Navigation: 1 giáo viên có thể làm chủ nhiệm nhiều lớp
        public ICollection<GiaoVienChuNhiem>? LopChuNhiem { get; set; }
    }
}
