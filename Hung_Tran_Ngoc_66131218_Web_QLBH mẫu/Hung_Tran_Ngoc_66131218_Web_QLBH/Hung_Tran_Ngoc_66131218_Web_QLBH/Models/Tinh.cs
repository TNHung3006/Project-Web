using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Models
{
    [Table("Tinh")]
    public class Tinh
    {
        [Key]
        [Display(Name = "Mã tỉnh")]
        public int MaTinh { get; set; }

        [Display(Name = "Tên tỉnh")]
        public string TenTinh { get; set; } = null!;

    }
}
