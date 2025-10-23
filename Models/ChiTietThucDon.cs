using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models
{
    [Table("ChiTietThucDon")]
    public class ChiTietThucDon
    {
        [ForeignKey("ThucDon")]
        public int MaTD { get; set; }

        [ForeignKey("NguyenLieu")]
        public int MaNL { get; set; }

        public decimal SoLuong { get; set; }

        // 🔹 Navigation
        public ThucDon? ThucDon { get; set; }
        public NguyenLieu? NguyenLieu { get; set; }
    }
}
