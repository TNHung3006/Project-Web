using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Models
{
    [Table("Xa")]
    public class Xa
    {
        [Key]
        [Display(Name = "Mã xã")]
        public int MaXa { get; set; }

        [Display(Name = "Tên xã")]
        public string TenXa { get; set; } = null!;

        [Display(Name = "Mã tỉnh")]
        public int MaTinh { get; set; }
    }
}
