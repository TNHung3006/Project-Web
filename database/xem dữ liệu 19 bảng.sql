
-- === 1. NHÓM ĐỊA LÝ & HÀNH CHÍNH ===
PRINT N'--- 1. Bảng Nuoc ---'
SELECT * FROM Nuoc;

PRINT N'--- 2. Bảng Tinh ---'
SELECT * FROM Tinh;

PRINT N'--- 3. Bảng Xa ---'
SELECT * FROM Xa;

-- === 2. NHÓM DANH MỤC & PHÂN LOẠI ===
PRINT N'--- 4. Bảng DonViTinh ---'
SELECT * FROM DonViTinh;

PRINT N'--- 5. Bảng NhomSP ---'
SELECT * FROM NhomSP;

PRINT N'--- 6. Bảng LoaiSP ---'
SELECT * FROM LoaiSP;

PRINT N'--- 7. Bảng HangSX ---'
SELECT * FROM HangSX;

PRINT N'--- 8. Bảng TrangThai (Sản phẩm) ---'
SELECT * FROM TrangThai;

PRINT N'--- 9. Bảng TrangThaiDBH (Đơn bán) ---'
SELECT * FROM TrangThaiDBH;

PRINT N'--- 10. Bảng TrangThaiDMH (Đơn mua) ---'
SELECT * FROM TrangThaiDMH;

PRINT N'--- 11. Bảng LoaiNV ---'
SELECT * FROM LoaiNV;

-- === 3. NHÓM CON NGƯỜI & ĐỐI TÁC ===
PRINT N'--- 12. Bảng NhanVien ---'
SELECT * FROM NhanVien;

PRINT N'--- 13. Bảng KhachHang ---'
SELECT * FROM KhachHang;

PRINT N'--- 14. Bảng NhaCC ---'
SELECT * FROM NhaCC;

-- === 4. NHÓM SẢN PHẨM ===
PRINT N'--- 15. Bảng SanPham ---'
SELECT * FROM SanPham;

-- === 5. NHÓM GIAO DỊCH (MUA & BÁN) ===
PRINT N'--- 16. Bảng DonMuaHang ---'
SELECT * FROM DonMuaHang;

PRINT N'--- 17. Bảng CTMH (Chi tiết mua) ---'
SELECT * FROM CTMH;

PRINT N'--- 18. Bảng DonBanHang ---'
SELECT * FROM DonBanHang;

PRINT N'--- 19. Bảng CTBH (Chi tiết bán) ---'
SELECT * FROM CTBH;