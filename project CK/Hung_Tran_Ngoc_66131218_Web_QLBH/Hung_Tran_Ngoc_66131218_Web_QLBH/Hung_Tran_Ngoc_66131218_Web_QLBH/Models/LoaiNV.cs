using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Models
{
    [Table("LoaiNV")]
    public class LoaiNV
    {
        [Key]
        [Display(Name = "Mã loại nhân viên")]
        public int MaLNV { get; set; }

        [Display(Name = "Tên loại nhân viên")]
        public string TenLNV { get; set; } = null!;

    }
}
