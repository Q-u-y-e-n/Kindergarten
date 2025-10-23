using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models
{
    [Table("LopHoc")]
    public class LopHoc
    {
        [Key]
        public int MaLop { get; set; }

        [Required, StringLength(100)]
        public string TenLop { get; set; } = string.Empty;

        [StringLength(50)]
        public string? KhoiLop { get; set; }

        [StringLength(50)]
        public string? NamHoc { get; set; }

        // Quan hệ: 1 lớp có nhiều học sinh
        public ICollection<HocSinh>? HocSinhs { get; set; }

        // Quan hệ: 1 lớp có thể có nhiều giáo viên chủ nhiệm (qua bảng trung gian)
        public ICollection<GiaoVienChuNhiem>? GiaoVienChuNhiems { get; set; }
    }
}
