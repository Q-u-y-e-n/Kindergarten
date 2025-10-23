
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models
{
    [Table("HocSinhPhuHuynh")]
    public class HocSinhPhuHuynh
    {
        [Key]
        public int MaHS { get; set; }
        public HocSinh HocSinh { get; set; } = null!;


        [Key]
        public int MaPH { get; set; }
        public PhuHuynh PhuHuynh { get; set; } = null!;

    }
}