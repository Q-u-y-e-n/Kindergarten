using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models
{
    [Table("HOCPHI")]
    public class HocPhi
    {
        [Key]
        public int MaHP { get; set; }

        [Required]
        [ForeignKey("HocSinh")]
        public int MaHS { get; set; }

        public decimal SoTien { get; set; }

        public DateTime ThangNam { get; set; }  // tháng tính học phí

        public bool DaDong { get; set; } = false;

        public HocSinh? HocSinh { get; set; }
    }
}
