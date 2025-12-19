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
        public string TenDN { get; set; }

        [Display(Name = "Mật khẩu")]
        public string? MatKhau { get; set; }

        [Display(Name = "Tên nhân viên")]
        public string TenNV { get; set; }

        [Display(Name = "số điện thoại")]
        public string DienThoai { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

    }
}
