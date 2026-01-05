using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Models
{
    [Table("NhaCC")]
    public class NhaCC
    {
        [Key]
        [Display(Name = "Mã nhà cung cấp")]
        public int MaNCC { get; set; }

        [Display(Name = "Tên nhà cung cấp")]
        public string TenNCC { get; set; }

        [Display(Name = "Địa chỉ nhà cung cấp")]
        public string DiaChiNCC { get; set; }

        [Display(Name = "Điện thoại nhà cung cấp")]
        public string DienThoaiNCC { get; set; }

        [Display(Name = "Email nhà cung cấp")]
        public string EmailNCC { get; set; }

        [Display(Name = "Mã xã")]
        public int MaXa { get; set; }

        public string? DiaChiNCCFull { get; set; }
    }
}
