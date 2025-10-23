using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models;

[Table("DanhMucPhi")]
public class DanhMucPhi
{
    [Key]
    public int MaPhi { get; set; }

    [Required, StringLength(100)]
    public string TenPhi { get; set; }

    public decimal SoTien { get; set; }

    public string? GhiChu { get; set; }
}