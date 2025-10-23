using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models;

[Table("MonHoc")]
public class MonHoc
{
    [Key]
    public int MaMon { get; set; }

    [Required, StringLength(100)]
    public string TenMon { get; set; }

    public string? MoTa { get; set; }
}