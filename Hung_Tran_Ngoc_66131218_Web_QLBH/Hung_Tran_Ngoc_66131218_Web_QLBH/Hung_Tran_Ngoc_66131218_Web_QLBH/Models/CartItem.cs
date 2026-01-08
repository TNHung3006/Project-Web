namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Models
{
    public class CartItem
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public string AnhSP { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien => SoLuong * DonGia;
    }
}