using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace kindergarten.Models;

[Table("TaiKhoan")]
public class TaiKhoan
{
    [Key]
    public int MaTK { get; set; }

    [Required, StringLength(50)]
    public string TenDangNhap { get; set; }

    [Required, StringLength(255)]
    public string MatKhau { get; set; }

    [StringLength(100)]
    public string? HoTen { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(15)]
    public string? SoDienThoai { get; set; }

    [StringLength(50)]
    public string VaiTro { get; set; } // Admin, GiaoVien, KeToan, PhuHuynh, YTe

    public bool TrangThai { get; set; } = true;

    public DateTime NgayTao { get; set; } = DateTime.Now;
}

