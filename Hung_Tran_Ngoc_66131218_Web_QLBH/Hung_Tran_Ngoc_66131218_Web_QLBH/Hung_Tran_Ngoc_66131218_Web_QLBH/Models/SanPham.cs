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
        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        [StringLength(200, ErrorMessage = "Tên sản phẩm không quá 200 ký tự")]
        public string TenSP { get; set; } = null!;

        [Display(Name = "Đơn giá")]
        [Required(ErrorMessage = "Đơn giá là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn hoặc bằng 0")]
        public decimal DonGia { get; set; }

        [Display(Name = "Ảnh sản phẩm")]
        [StringLength(200)]
        public string? AnhSP { get; set; }

        [Display(Name = "Mô tả sản phẩm")]
        [Required(ErrorMessage = "Mô tả sản phẩm là bắt buộc")]
        [StringLength(200, ErrorMessage = "không quá 200 ký tự")]
        public string MoTaSP { get; set; } = null!;

        [Display(Name = "Mã loại sản phẩm")]
        public int MaLSP { get; set; }

        [Display(Name = "Mã đơn vị tính")]
        public int MaDVT { get; set; }

        [Display(Name = "Mã trạng thái")]
        public int MaTT { get; set; }

        [Display(Name = "Mã hãng sản xuất")]
        public int MaHSX { get; set; }

        // --- CÁC THUỘC TÍNH PHỤ (KHÔNG CÓ TRONG CSDL) ---
        // Phải thêm [NotMapped] để tránh lỗi "Invalid column name"

        [NotMapped]
        [Display(Name = "Hãng sản xuất")]
        public string? HangSX { get; set; }

        [NotMapped]
        [Display(Name = "Loại sản phẩm")]
        public string? LoaiSP { get; set; } // Đây là string lưu Tên Loại

        [NotMapped]
        [Display(Name = "Trạng thái")]
        public string? TenTT { get; set; }

        [NotMapped]
        [Display(Name = "Đơn vị tính")]
        public string? TenDVT { get; set; }
    }
}