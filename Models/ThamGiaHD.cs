using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models
{
    [Table("THAMGIA_HD")]
    public class ThamGiaHD
    {
        [Key, Column(Order = 0)]
        public int MaHD { get; set; }

        [Key, Column(Order = 1)]
        public int MaHS { get; set; }

        public bool? CoThamGia { get; set; }
        public string? GhiChu { get; set; }

        [ForeignKey("MaHD")]
        public HoatDong HoatDong { get; set; }

        [ForeignKey("MaHS")]
        public HocSinh HocSinh { get; set; }
    }
}
