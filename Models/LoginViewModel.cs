using System.ComponentModel.DataAnnotations;
// Không cần using System.ComponentModel.DataAnnotations.Schema;

namespace kindergarten.Models // Đảm bảo namespace đúng
{
    // XÓA DÒNG [Table("...")] Ở ĐÂY
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        public string TenDangNhap { get; set; } = string.Empty; // Thêm = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; } = string.Empty; // Thêm = string.Empty;

        public bool RememberMe { get; set; } // bool mặc định là false, không cần khởi tạo
    }
}