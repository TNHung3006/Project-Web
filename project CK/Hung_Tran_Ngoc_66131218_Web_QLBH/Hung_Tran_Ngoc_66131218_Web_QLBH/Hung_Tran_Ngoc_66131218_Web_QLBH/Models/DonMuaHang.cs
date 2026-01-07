using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Models
{
    [Table("DonMuaHang")]
    public class DonMuaHang
        {
        [Key]
        [Display(Name = "Mã đơn mua hàng")]
        public int MaDMH { get; set; }

        [Display(Name = "Ngày mua")]
        public DateTime NgayMua { get; set; }

        [Display(Name = "Mã nhà cung cấp")]
        public int MaNCC { get; set; }
        [Display(Name = "Mã nhân viên")]
        public int MaNV { get; set; }
        [Display(Name = "Mã TTDMH")]
        public int MaTTDMH { get; set; }

        [Display(Name = "Tên nhân viên")]
        public string? TenNVFull { get; set; }
        [Display(Name = "Tên nhà cung cấp")]
        public string? TenNCC { get; set; }
        [Display(Name = "Tên TTDMH")]
        public string? TenTTDMH { get; set; }

    }
}
