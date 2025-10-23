using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models
{
    [Table("ThucDon")]
    public class ThucDon
    {
        [Key]
        public int MaTD { get; set; }

        [Required]
        [StringLength(100)]
        public string TenThucDon { get; set; }

        public DateTime NgayApDung { get; set; } = DateTime.Now;

        // 🔹 Quan hệ 1-n với ChiTietThucDon
        public ICollection<ChiTietThucDon>? ChiTietThucDons { get; set; }

        // 🔹 Quan hệ 1-n với BuaAn
        public ICollection<BuaAn>? BuaAns { get; set; }
        public string? GhiChu { get; set; }
    }
}
