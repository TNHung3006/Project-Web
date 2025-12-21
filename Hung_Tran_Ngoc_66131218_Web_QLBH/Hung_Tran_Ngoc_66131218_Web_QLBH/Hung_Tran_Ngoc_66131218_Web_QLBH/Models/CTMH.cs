using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Models
{
    [Table("CTMH")]
    public class CTMH
    {
        [Key]
        [Display(Name = "Mã đơn mua hàng")]
        public int MaDMH { get; set; }

        [Display(Name = "Mã sản phẩm")]
        public int MaSP { get; set; }

        [Display(Name = "Số lượng mua")]
        public int SLM { get; set; }

        [Display(Name = "Đơn giá mua")]
        public decimal DGM { get; set; }

    }
}
