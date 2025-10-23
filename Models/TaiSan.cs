using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models
{
    [Table("TaiSan")]
    public class TaiSan
    {
        [Key]
        public int MaTS { get; set; }

        [Required, StringLength(100)]
        public string TenTS { get; set; }

        [StringLength(100)]
        public string? LoaiTS { get; set; }

        public DateTime? NgayMua { get; set; }

        public decimal? GiaTri { get; set; }

        [StringLength(50)]
        public string? TinhTrang { get; set; }

        [StringLength(100)]
        public string? ViTri { get; set; }
    }
}
