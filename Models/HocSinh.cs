using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models
{
    [Table("HocSinh")]
    public class HocSinh
    {
        [Key]
        public int MaHS { get; set; }

        [Required, StringLength(100)]
        public string HoTen { get; set; } = string.Empty;

        // nullable để dùng ? trong view (Model?.NgaySinh)
        public DateTime? NgaySinh { get; set; }

        [StringLength(10)]
        public string? GioiTinh { get; set; }

        // Tên chính xác: DiaChi (chữ hoa C)
        [StringLength(255)]
        public string? DiaChi { get; set; }

        public string? AnhDaiDien { get; set; }

        [StringLength(50)]
        public string TinhTrang { get; set; } = "Đang học";

        // Khóa ngoại lớp
        public int? MaLop { get; set; }
        public LopHoc? LopHoc { get; set; }

        // Navigation properties
        public ICollection<HOCSINH_PHUHUYNH>? HocSinhPhuHuynhs { get; set; }
        public ICollection<DiemDanh>? DiemDanhs { get; set; }
        public ICollection<KetQua>? KetQuas { get; set; }
        // public ICollection<HocPhi>? HocPhis { get; set; }
        public ICollection<HoSoSucKhoe>? HoSoSucKhoes { get; set; }
        public ICollection<ThamGiaHD>? ThamGiaHDs { get; set; }

    }
}
