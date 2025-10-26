using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models
{
    [Table("LOPHOC")]
    public class LopHoc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaLop { get; set; }

        [Required]
        [StringLength(50)]
        public string TenLop { get; set; } = string.Empty;

        [StringLength(20)]
        public string? NamHoc { get; set; }

        public int? SiSoToiDa { get; set; }

        // ====== Khóa ngoại: Giáo viên chủ nhiệm ======
        [ForeignKey(nameof(GiaoVienChuNhiem))]
        public int? MaGVCN { get; set; }

        // Liên kết tới bảng GIAOVIEN
        public virtual GiaoVien? GiaoVienChuNhiem { get; set; }

        [StringLength(255)]
        public string? GhiChu { get; set; }

        // ====== Quan hệ: 1 lớp có nhiều học sinh ======
        public virtual ICollection<HocSinh>? HocSinhs { get; set; }
    }
}
