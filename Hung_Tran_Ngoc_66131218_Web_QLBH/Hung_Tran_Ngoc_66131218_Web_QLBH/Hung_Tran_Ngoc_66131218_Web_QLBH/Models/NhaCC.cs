using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Models
{
    [Table("NhaCC")]
    public class NhaCC
    {
        [Key]
        [Display(Name = "Mã nhà cung cấp")]
        public int MaNCC { get; set; }

        [Display(Name = "Tên nhà cung cấp")]
        [Required(ErrorMessage = "Tên nhà cung cấp là bắt buộc")]
        [StringLength(200)]
        public string TenNCC { get; set; } = null!;

        [Display(Name = "Địa chỉ")]
        [StringLength(200)]
        public string DiaChiNCC { get; set; } = null!;

        [Display(Name = "Điện thoại")]
        [RegularExpression(@"^0\d{9,10}$", ErrorMessage = "SĐT phải bắt đầu bằng 0 và có 10-11 số")]
        public string DienThoaiNCC { get; set; } = null!;

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string EmailNCC { get; set; } = null!;

        [Display(Name = "Mã xã")]
        public int MaXa { get; set; }

        public string? DiaChiNCCFull { get; set; }
    }
}