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
        public string? TenSP { get; set; }

        [Display(Name = "Đơn giá")]
        public decimal DonGia { get; set; }

        [Display(Name = "Ảnh sản phẩm")]
        public string? AnhSP { get; set; }

        [Display(Name = "Mô tả sản phẩm")]
        public string? MoTaSP { get; set; }

        [Display(Name = "mã loại sản phẩm")]
        public int MaLSP { get; set; }

        [Display(Name = "Mã đơn vị tính")]
        public int MaDVT { get; set; }

        [Display(Name = "Mã trạng thái")]
        public int MaTT { get; set; }

        [Display(Name = "Mã hãng sản xuất")]
        public int MaHSX { get; set; }

        [Display(Name = "Hãng sản xuất")]
        public string? HangSX { get; set; }

        [Display(Name = "Loại sản phẩm")]
        public string? LoaiSP { get; set; }

        [Display(Name = "Trạng thái")]
        public string? TenTT { get; set; }

        [Display(Name = "Đơn vị tính")]
        public string? TenDVT { get; set; }

    }
}
