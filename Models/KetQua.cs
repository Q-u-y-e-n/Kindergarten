
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models
{
    [Table("KetQua")]
    public class KetQua
    {
        [Key]
        public int MaKQ { get; set; }

        [ForeignKey("HocSinh")]
        public int MaHS { get; set; }

        public HocSinh HocSinh { get; set; }

        [ForeignKey("MonHoc")]
        public int MaMon { get; set; }

        public MonHoc MonHoc { get; set; }

        [StringLength(20)]
        public string HocKy { get; set; }

        [StringLength(20)]
        public string NamHoc { get; set; }

        [StringLength(50)]
        public string? DanhGia { get; set; }

        public string? NhanXet { get; set; }
    }
}