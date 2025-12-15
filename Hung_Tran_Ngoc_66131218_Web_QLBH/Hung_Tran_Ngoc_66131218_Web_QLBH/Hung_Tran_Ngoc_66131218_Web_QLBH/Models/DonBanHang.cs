using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Models
{
    [Table("DonBanHang")]
    public class DonBanHang
    {
        [Key]
        [Display(Name = "Mã đơn bán hàng")]
        public int MaDBH { get; set; }

        [Display(Name = "Ngày bán")]
        public DateOnly NgayBan { get; set; }

        [Display(Name = "Mã Khách Hàng")]
        public int MaKH { get; set; }
    }
}
