using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Models
{
    [Table("GianHang")]
    public class GianHang
    {
        [Key]
        [Display(Name = "Mã gian hàng")]
        public int MaGH { get; set; }

        [Display(Name = "Tên gian hàng")]
        public string TenGH { get; set; } = null!;

        [Display(Name = "Mã xa")]
        public int MaXa { get; set; }

    }
}
