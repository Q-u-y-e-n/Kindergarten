using System;
using System.ComponentModel.DataAnnotations;

namespace kindergarten.Models
{
    public class RegisterViewModel
    {
        // === Thông tin Phụ Huynh ===
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [StringLength(100)]
        [Display(Name = "Họ tên Phụ huynh")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        [Display(Name = "Số điện thoại")]
        public string? SoDienThoai { get; set; }

        [StringLength(200)]
        [Display(Name = "Địa chỉ")]
        public string? DiaChi { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [StringLength(50)]
        [Display(Name = "Tên đăng nhập")]
        public string TenDangNhap { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(100, ErrorMessage = "{0} phải có ít nhất {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu và mật khẩu xác nhận không khớp.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn giới tính của phụ huynh")]
        [StringLength(10)]
        [Display(Name = "Giới tính Phụ huynh")]
        public string GioiTinh { get; set; }  // <-- thêm property này

        // ======================================
        // === Thông tin của bé ===
        // ======================================
        [Required(ErrorMessage = "Vui lòng nhập họ tên của bé")]
        [StringLength(100)]
        [Display(Name = "Họ tên của bé")]
        public string HoTenBe { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày sinh của bé")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh của bé")]
        public DateTime NgaySinhBe { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn giới tính của bé")]
        [StringLength(10)]
        [Display(Name = "Giới tính của bé")]
        public string GioiTinhBe { get; set; }
    }
}
