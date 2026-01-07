using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Models
{
    [Table("KhachHang")]
    public class KhachHang
    {
        [Key]
        [Display(Name = "Mã khách hàng")]
        public int MaKH { get; set; }

        // --- CÁC TRƯỜNG BẮT BUỘC (Có Required, Không có dấu ?, SQL bỏ Allow Null) ---

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Tên đăng nhập từ 4-50 ký tự")]
        public string TenDN { get; set; } = null!;

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu ít nhất 6 ký tự")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; } = null!;

        [Display(Name = "Họ khách hàng")]
        [Required(ErrorMessage = "Họ đệm không được để trống")]
        [StringLength(50)]
        public string HoKH { get; set; } = null!;

        [Display(Name = "Tên khách hàng")]
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(50)]
        public string TenKH { get; set; } = null!;

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [RegularExpression(@"^0\d{9,10}$", ErrorMessage = "SĐT phải bắt đầu bằng 0 và có 10-11 số")]
        public string SDT { get; set; } = null!;


        // --- CÁC TRƯỜNG TÙY CHỌN (Không có Required, Có dấu ?, SQL tích Allow Null) ---

        [Display(Name = "Ảnh khách hàng")]
        [StringLength(200)]
        public string? AnhKH { get; set; } 

        [Display(Name = "Địa chỉ khách hàng")]
        [StringLength(200)]
        public string? DiaChi { get; set; } 

        [Display(Name = "Mã xã")]
        public int MaXa { get; set; } 

        public string? DiaChiFull { get; set; }
    }
}