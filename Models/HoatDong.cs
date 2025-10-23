using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models;

[Table("HoatDong")]
public class HoatDong
{
    [Key]
    public int MaHD { get; set; }

    [Required, StringLength(100)]
    public string TenHD { get; set; }

    public string? ChuDe { get; set; }

    public string? DiaDiem { get; set; }

    public DateTime? NgayToChuc { get; set; }

    public string? NguoiPhuTrach { get; set; }

    public decimal? ChiPhi { get; set; }

    public string? MoTa { get; set; }
}