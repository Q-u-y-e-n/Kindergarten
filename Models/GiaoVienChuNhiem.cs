using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace kindergarten.Models
{
    [Table("GiaoVienChuNhiem")]
    public class GiaoVienChuNhiem
    {
        [ForeignKey("GiaoVien")]
        public int MaGV { get; set; }

        [ForeignKey("LopHoc")]
        public int MaLop { get; set; }

        // Quan hệ
        public GiaoVien? GiaoVien { get; set; }
        public LopHoc? LopHoc { get; set; }
    }
}
