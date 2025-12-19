using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Models
{
    [Table("CTBH")]
    public class CTBH
    {
        [Key]
        [Display(Name = "Mã đơn bán hàng")]
        public int MaDBH { get; set; }

        [Display(Name = "Mã sản phẩm")]
        public int MaSP { get; set; }

        [Display(Name = "Số lượng")]
        public int SoLuong { get; set; }

        [Display(Name = "Đơn giá")]
        public decimal DonGia { get; set; }

    }
}
