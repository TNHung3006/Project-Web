using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Models
{
    [Table("Nuoc")]
    public class Nuoc
    {
        [Key]
        [Display(Name = "Mã nước")]
        public string MaNuoc { get; set; } = null!;

        [Display(Name = "Tên nước")] 
        public string TenNuoc { get; set; } = null!;

    }
}
