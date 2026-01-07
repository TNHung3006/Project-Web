using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Models
{
    [Table("NhanVien")]
    public class NhanVien
    {
        [Key]
        [Display(Name = "Mã nhân viên")]
        public int MaNV { get; set; }

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [StringLength(20, ErrorMessage = "Tên đăng nhập tối đa 20 ký tự")]
        public string TenDN { get; set; } = null!;

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; } = null!;

        [Display(Name = "Họ nhân viên")]
        [Required(ErrorMessage = "Họ nhân viên là bắt buộc")]
        [StringLength(50)]
        public string HoNV { get; set; } = null!;

        [Display(Name = "Tên nhân viên")]
        [Required(ErrorMessage = "Tên nhân viên là bắt buộc")]
        [StringLength(50)]
        public string TenNV { get; set; } = null!;

        [Display(Name = "Giới tính")]
        [StringLength(10)] // Ví dụ: "Nam", "Nữ"
        public string? GioiTinh { get; set; }

        [Display(Name = "Số điện thoại")]
        [RegularExpression(@"^0\d{9,10}$", ErrorMessage = "SĐT phải bắt đầu bằng 0 và có 10-11 số")]
        public string? DienThoai { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Định dạng Email không hợp lệ")]
        public string Email { get; set; } = null!;

        [Display(Name = "Địa chỉ")]
        [StringLength(200)]
        public string? DiaChi { get; set; }

        [Display(Name = "Mã loại nhân viên")]
        public int MaLNV { get; set; }

        [Display(Name = "Mã Xã")]
        public int MaXa { get; set; }

        public string? DiaChiFull { get; set; }

        [Display(Name = "Tên loại nhân viên")]
        public string? TenLNV { get; set; }
    }
}