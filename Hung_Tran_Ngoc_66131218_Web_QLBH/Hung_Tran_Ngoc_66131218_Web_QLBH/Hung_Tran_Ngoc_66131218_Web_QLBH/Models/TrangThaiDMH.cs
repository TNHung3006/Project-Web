using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Models
{
    [Table("TrangThaiDMH")]
    public class TrangThaiDMH
    {
        [Key]
        [Display(Name = "Mã trạng thái đơn mua hàng")]
        public int MaTTDMH { get; set; }

        [Display(Name = "Tên trạng thái đơn mua hàng")]
        public string TenTTDMH { get; set; } = null!;

    }
}
