using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models
{
    [Table("HOATDONG")]
    public class HoatDong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaHD { get; set; }

        [StringLength(100)]
        public string? TenHD { get; set; }

        [StringLength(100)]
        public string? ChuDe { get; set; }

        [StringLength(255)]
        public string? DiaDiem { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayToChuc { get; set; }

        [StringLength(100)]
        public string? NguoiPhuTrach { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ChiPhi { get; set; }

        [StringLength(255)]
        public string? MoTa { get; set; }

        // 🔹 Quan hệ: 1 hoạt động có nhiều bản ghi tham gia
        public ICollection<ThamGiaHD>? ThamGiaHDs { get; set; }
    }
}
