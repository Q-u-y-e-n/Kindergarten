using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models;

[Table("ChiTietHoaDon")]
public class ChiTietHoaDon
{
    [Key]
    public int MaCT { get; set; }

    [ForeignKey("HoaDon")]
    public int MaHD { get; set; }

    [ForeignKey("DanhMucPhi")]
    public int MaPhi { get; set; }

    public decimal SoTien { get; set; }

    public HoaDon HoaDon { get; set; }
    public DanhMucPhi DanhMucPhi { get; set; }
}