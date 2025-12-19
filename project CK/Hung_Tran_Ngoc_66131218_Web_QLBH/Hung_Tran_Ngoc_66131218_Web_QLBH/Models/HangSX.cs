using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Models
{
    [Table("HangSX")]
    public class HangSX
    {
        [Key]
        [Display(Name = "Mã hãng sản xuất")]
        public int MaHSX { get; set; }

        [Display(Name = "Tên Hãng sản xuất")]
        public string TenHSX { get; set; } = null!;

        [Display(Name = "Mã nước")]
        public string MaNuoc { get; set; } = null!;
    }
}
