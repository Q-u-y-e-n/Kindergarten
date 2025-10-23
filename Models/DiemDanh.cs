using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models
{
    [Table("DiemDanh")]
    public class DiemDanh
    {
        [Key]
        public int MaDD { get; set; }

        [ForeignKey("HocSinh")]
        public int MaHS { get; set; }

        public HocSinh HocSinh { get; set; }

        public DateTime Ngay { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string TrangThai { get; set; } // Đi học, Nghỉ phép...

        public string? GhiChu { get; set; }
    }
}
