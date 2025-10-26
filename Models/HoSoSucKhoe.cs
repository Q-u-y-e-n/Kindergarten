using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models
{
    [Table("HOSOSUCKHOE")]
    public class HoSoSucKhoe
    {
        [Key]
        public int MaSK { get; set; }

        [ForeignKey("HocSinh")]
        public int MaHS { get; set; }

        public HocSinh? HocSinh { get; set; }

        public double? ChieuCao { get; set; }
        public double? CanNang { get; set; }

        // 🔹 Phải dùng [Column] để khớp chính xác tên có dấu “ị”


        [StringLength(100)]
        public string? DiUng { get; set; }

        [StringLength(255)]
        public string? BenhNen { get; set; }

        public DateTime? NgayKham { get; set; }

        [StringLength(255)]
        public string? GhiChu { get; set; }
    }
}
