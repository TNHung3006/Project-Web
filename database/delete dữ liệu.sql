-- BƯỚC 1: Vô hiệu hóa toàn bộ khóa ngoại (Foreign Keys) trên tất cả các bảng
-- Giúp việc xóa không bị chặn bởi lỗi quan hệ cha-con
EXEC sp_MSforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all"
GO

-- BƯỚC 2: Xóa dữ liệu toàn bộ 19 bảng
-- (Dùng DELETE thay vì TRUNCATE để tránh lỗi nếu chưa drop hẳn khóa ngoại)
DELETE FROM CTBH
DELETE FROM CTMH
DELETE FROM DonBanHang
DELETE FROM DonMuaHang
DELETE FROM SanPham
DELETE FROM KhachHang
DELETE FROM NhanVien
DELETE FROM NhaCC
DELETE FROM HangSX
DELETE FROM Xa
DELETE FROM LoaiSP
DELETE FROM NhomSP
DELETE FROM DonViTinh
DELETE FROM LoaiNV
DELETE FROM TrangThai
DELETE FROM TrangThaiDBH
DELETE FROM TrangThaiDMH
DELETE FROM Nuoc
DELETE FROM Tinh
GO

-- BƯỚC 3: Kích hoạt lại toàn bộ khóa ngoại (Để đảm bảo tính toàn vẹn dữ liệu sau này)
EXEC sp_MSforeachtable "ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all"
GO