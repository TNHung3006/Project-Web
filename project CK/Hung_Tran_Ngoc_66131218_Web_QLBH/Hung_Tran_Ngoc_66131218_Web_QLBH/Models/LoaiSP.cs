using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Models
{
    [Table("LoaiSP")]
    public class LoaiSP
    {
        [Key]
        [Display(Name = "Mã loại sản phẩm")]
        public int MaLSP { get; set; }

        [Display(Name = "Tên loại sản phẩm")]
        public string TenLSP { get; set; } = null!;

        [Display(Name = "Mã nhóm sản phẩm")]
        public int MaNhomSP { get; set; }
    }
}
