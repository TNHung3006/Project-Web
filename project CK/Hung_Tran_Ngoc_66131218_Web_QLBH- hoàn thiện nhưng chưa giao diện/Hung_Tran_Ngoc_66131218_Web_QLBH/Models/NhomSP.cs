using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Models
{
    [Table("NhomSP")]
    public class NhomSP
    {
        [Key]
        [Display(Name = "Mã nhóm sản phẩm")] // thuộc tính hiển thị
        public int MaNhomSP { get; set; }   

        [Display(Name = "Tên nhóm sản phẩm")] // thuộc tính hiển thị
        [StringLength(200, ErrorMessage = "Tên Nhóm tối đa 200 ký tự")]
        public string TenNhomSP { get; set; } = null!;

    }
}
