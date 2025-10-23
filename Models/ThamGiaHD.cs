
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models;

[Table("ThamGiaHD")]
public class ThamGiaHD
{
    [Key]
    public int MaHD { get; set; }

    public HoatDong HoatDong { get; set; }

    [Key]
    public int MaHS { get; set; }

    public HocSinh HocSinh { get; set; }

    public bool? CoThamGia { get; set; }

    public string? GhiChu { get; set; }
}