using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace kindergarten.Models
{
    [Table("NguyenLieu")]
    public class NguyenLieu
    {
        [Key]
        public int MaNL { get; set; }

        [Required, StringLength(100)]
        public string TenNguyenLieu { get; set; } = string.Empty;

        [StringLength(50)]
        public string? DonViTinh { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SoLuongTon { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal GiaNhap { get; set; }

        [StringLength(200)]
        public string? NhaCungCap { get; set; }

        [StringLength(300)]
        public string? GhiChu { get; set; }

        // Quan hệ với ChiTietThucDon
        public ICollection<ChiTietThucDon>? ChiTietThucDons { get; set; }
    }
}
