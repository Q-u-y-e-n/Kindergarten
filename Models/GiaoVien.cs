using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models
{
    [Table("GIAOVIEN")]
    public class GiaoVien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaGV { get; set; }

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime? NgaySinh { get; set; }

        [StringLength(10)]
        public string? GioiTinh { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(15)]
        public string? SoDienThoai { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(100)]
        public string? BangCap { get; set; }

        [StringLength(255)]
        public string? KinhNghiem { get; set; }

        public bool TrangThai { get; set; } = true;

        // Nếu muốn quan hệ với lớp chủ nhiệm, có thể thêm:
        // public virtual ICollection<LopHoc>? LopChuNhiems { get; set; }
        public virtual ICollection<LopHoc>? LopHocs { get; set; }
    }
}
