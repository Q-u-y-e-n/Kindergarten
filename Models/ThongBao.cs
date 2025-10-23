using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models
{
    [Table("ThongBao")]
    public class ThongBao
    {
        [Key]
        public int MaTB { get; set; }

        [Required]
        [StringLength(200)]
        public string TieuDe { get; set; }

        [Required]
        public string NoiDung { get; set; } // NVARCHAR(MAX) sẽ được hiểu là string không giới hạn

        public DateTime NgayDang { get; set; } = DateTime.Now;

        [StringLength(100)]
        public string? NguoiDang { get; set; }

        [StringLength(20)]
        public string? DoiTuong { get; set; } = "TatCa"; // 'TatCa', 'PhuHuynh'
    }
}