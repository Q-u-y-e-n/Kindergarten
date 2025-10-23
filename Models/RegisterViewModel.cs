using System.ComponentModel.DataAnnotations;

namespace kindergarten.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Họ tên phụ huynh là bắt buộc")]
        public string HoTen { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        public string TenDangNhap { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; } = string.Empty;   // ✅ Thêm dòng này

        [Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc")]
        [DataType(DataType.Password)]
        [Compare("MatKhau", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        public string XacNhanMatKhau { get; set; } = string.Empty; // ✅ Thêm dòng này

        [Required(ErrorMessage = "Giới tính là bắt buộc")]
        public string GioiTinh { get; set; } = string.Empty;

        [Required(ErrorMessage = "Họ tên bé là bắt buộc")]
        public string HoTenBe { get; set; } = string.Empty;

        [Required(ErrorMessage = "Giới tính bé là bắt buộc")]
        public string GioiTinhBe { get; set; } = string.Empty;
    }
}
