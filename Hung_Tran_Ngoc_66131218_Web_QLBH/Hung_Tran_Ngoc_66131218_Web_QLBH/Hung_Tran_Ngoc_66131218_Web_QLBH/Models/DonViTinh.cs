using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Models
{
    [Table("DonViTinh")]
    public class DonViTinh
    {
        [Key]
        [Display(Name = "Mã đơn vị tính")]
        public int MaDVT { get; set; }

        [Display(Name = "Tên nước")]
        public string TenDVT { get; set; } = null!;

    }
}
