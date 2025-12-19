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
        public DateOnly NgayMua { get; set; }

        [Display(Name = "Mã nhà cung cấp")]
        public int MaNCC { get; set; }
    }
}
