using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models
{

    [Table("HoSoSucKhoe")]
    public class HoSoSucKhoe
    {
        [Key]
        public int MaHoSo { get; set; }

        [ForeignKey("HocSinh")]
        public int MaHS { get; set; }

        public DateTime NgayKiemTra { get; set; } = DateTime.Now;

        [Range(0, 300)]
        public double? ChieuCao { get; set; }

        [Range(0, 200)]
        public double? CanNang { get; set; }

        [StringLength(255)]
        public string? TinhTrangSucKhoe { get; set; }

        [StringLength(255)]
        public string? GhiChu { get; set; }

        // 🔹 Navigation property
        public HocSinh? HocSinh { get; set; }
    }
}
