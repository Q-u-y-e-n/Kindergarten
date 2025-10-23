using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models;

[Table("HoaDon")]
public class HoaDon
{
    [Key]
    public int MaHD { get; set; }

    [ForeignKey("HocSinh")]
    public int MaHS { get; set; }

    public HocSinh HocSinh { get; set; }

    public int Thang { get; set; }

    public int Nam { get; set; }

    public DateTime NgayLap { get; set; } = DateTime.Now;

    public decimal TongTien { get; set; }

    [StringLength(50)]
    public string TrangThai { get; set; } = "Chưa thanh toán";

    public ICollection<ChiTietHoaDon>? ChiTietHoaDons { get; set; }
}