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

        [Display(Name = "Tên đăng nhập")]
        public string? TenDN { get; set; }

        [Display(Name = "mật khẩu")]
        public string? MatKhau { get; set; }

        [Display(Name = "Họ khách hàng")]
        public string? HoKH { get; set; }

        [Display(Name = "Tên khách hàng")]
        public string? TenKH { get; set; }

        [Display(Name = "Ảnh khách hàng")]
        public string? AnhKH { get; set; }

        [Display(Name = "Địa chỉ khách hàng")]
        public string? DiaChi { get; set; }

        [Display(Name = "Số điện thoại khách hàng")]
        public string? SDT { get; set; }

        [Display(Name = "Mã xã")]
        public int MaXa { get; set; }

        public string? DiaChiFull { get; set; }

    }
}
