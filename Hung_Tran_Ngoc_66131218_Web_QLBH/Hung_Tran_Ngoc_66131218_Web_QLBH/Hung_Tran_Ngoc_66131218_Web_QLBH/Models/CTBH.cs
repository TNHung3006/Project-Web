using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Models
{
    [Table("CTBH")]
    public class CTBH
    {
        [Key]
        [Display(Name = "Mã đơn bán hàng")]
        public int MaDBH { get; set; }

        [Key]
        [Display(Name = "Mã sản phẩm")]
        public int MaSP { get; set; }

        [Display(Name = "Số lượng bán")]
        public int SLB { get; set; }

        [Display(Name = "Đơn giá bán")]
        public decimal DGB { get; set; }

        [NotMapped]

        [Display(Name = "Tên sản phẩm")]
        public string? TenSP { get; set; }
        [NotMapped]
        [Display(Name = "Thành tiền")]
        public decimal ThanhTien { get; set; }
        [NotMapped]
        [Display(Name = "Ngày bán")]
        public DateTime NgayBan { get; set; }


        [NotMapped] // Không tạo cột này trong Database
        public string? AnhSP { get; set; }

    }
}
