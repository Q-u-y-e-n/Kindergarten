using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models
{
    [Table("BuaAn")]
    public class BuaAn
    {
        [Key]
        public int MaBA { get; set; }

        [ForeignKey("ThucDon")]
        public int MaTD { get; set; }

        public DateTime NgayPhucVu { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string? Buoi { get; set; } // sáng, trưa, chiều

        // 🔹 Navigation
        public ThucDon? ThucDon { get; set; }
    }
}
