
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models
{
    [Table("HOCSINH_PHUHUYNH")]
    public class HOCSINH_PHUHUYNH
    {
        [Key]
        public int MaHS { get; set; }
        public HocSinh HocSinh { get; set; } = null!;


        [Key]
        public int MaPH { get; set; }
        public PhuHuynh PhuHuynh { get; set; } = null!;

    }
}