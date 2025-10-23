
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models;

[Table("ThanhToan")]
public class ThanhToan
{
    [Key]
    public int MaTT { get; set; }

    [ForeignKey("HoaDon")]
    public int MaHD { get; set; }

    public HoaDon HoaDon { get; set; }

    public DateTime? NgayThanhToan { get; set; }

    public decimal? SoTien { get; set; }

    [StringLength(50)]
    public string? HinhThuc { get; set; }

    [StringLength(50)]
    public string? TrangThai { get; set; }
}
