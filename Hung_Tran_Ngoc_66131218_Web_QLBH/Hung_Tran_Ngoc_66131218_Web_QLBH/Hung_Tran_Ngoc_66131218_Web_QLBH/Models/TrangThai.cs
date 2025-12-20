using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Models
{
    [Table("TrangThai")]
    public class TrangThai
    {
        [Key]
        [Display(Name = "Mã trạng thái")]
        public int MaTT { get; set; }

        [Display(Name = "Tên trạng thái")]
        public string TenTT { get; set; } = null!;

    }
}
