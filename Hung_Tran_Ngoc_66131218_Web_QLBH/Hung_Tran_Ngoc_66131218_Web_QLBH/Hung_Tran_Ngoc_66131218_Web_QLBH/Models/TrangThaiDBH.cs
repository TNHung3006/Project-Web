using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Models
{
    [Table("TrangThaiDBH")]
    public class TrangThaiDBH
    {
        [Key]
        [Display(Name = "Mã trạng thái đơn bán hàng")]
        public int MaTTDBH { get; set; }

        [Display(Name = "Tên trạng thái đơn bán hàng")]
        public string TenTTDBH { get; set; } = null!;

    }
}
