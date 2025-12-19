using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Models
{
    [Table("SanPham")]
    public class SanPham
    {
        [Key]
        [Display(Name = "Mã sản phẩm")]
        public int MaSP { get; set; }

        [Display(Name = "Tên sản phẩm")]
        public string TenSP { get; set; }

        [Display(Name = "Đơn giá")]
        public decimal DonGia { get; set; }

        [Display(Name = "Số lượng")]
        public int SoLuong { get; set; }

        [Display(Name = "Ảnh sản phẩm")]
        public string? AnhSP { get; set; }

        [Display(Name = "mã loại sản phẩm")]
        public int MaLSP { get; set; }

        [Display(Name = "Mã gian hàng")]
        public int MaGH { get; set; }
    }
}
