
-- 1. NhomSP
CREATE TABLE NhomSP (
    MaNhomSP INT PRIMARY KEY,
    TenNhomSP NVARCHAR(100) NOT NULL
);

-- 2. LoaiSP
CREATE TABLE LoaiSP (
    MaLSP INT PRIMARY KEY,
    TenLSP NVARCHAR(100) NOT NULL,
    MaNhomSP INT,
    FOREIGN KEY (MaNhomSP) REFERENCES NhomSP(MaNhomSP)
);

-- 3. NhaCC
CREATE TABLE NhaCC (
    MaNCC INT PRIMARY KEY,
    TenNCC NVARCHAR(100),
    DiaChiNCC NVARCHAR(200),
    DienThoaiNCC NVARCHAR(12),
	EmailNCC nvarchar(320)
);

-- 4. Tinh
CREATE TABLE Tinh (
    MaTinh INT PRIMARY KEY,
    TenTinh NVARCHAR(100)
);

-- 5. Xa
CREATE TABLE Xa (
    MaXa INT PRIMARY KEY,
    TenXa NVARCHAR(100),
    MaTinh INT,
    FOREIGN KEY (MaTinh) REFERENCES Tinh(MaTinh)
);

-- 6. KhachHang
CREATE TABLE KhachHang (
    MaKH INT PRIMARY KEY,
    TenKH NVARCHAR(100),
    DiaChi NVARCHAR(200),
    SDT NVARCHAR(20),
    MaXa INT,
    FOREIGN KEY (MaXa) REFERENCES Xa(MaXa)
);

-- 7. GianHang
CREATE TABLE GianHang (
    MaGH INT PRIMARY KEY,
    TenGH NVARCHAR(100),
    MaXa INT,
    FOREIGN KEY (MaXa) REFERENCES Xa(MaXa)
);

-- 8. SanPham
CREATE TABLE SanPham (
    MaSP INT PRIMARY KEY,
    TenSP NVARCHAR(100),
    DonGia DECIMAL(18,2),
    SoLuong INT,
    MaLoaiSP INT,
    MaGH INT,
    FOREIGN KEY (MaLoaiSP) REFERENCES LoaiSP(MaLSP),
    FOREIGN KEY (MaGH) REFERENCES GianHang(MaGH)
);

-- 9. DonMuaHang
CREATE TABLE DonMuaHang (
    MaDMH INT PRIMARY KEY,
    NgayMua DATE,
    MaNCC INT,
    FOREIGN KEY (MaNCC) REFERENCES NhaCC(MaNCC)
);

-- 10. DonBanHang
CREATE TABLE DonBanHang (
    MaDBH INT PRIMARY KEY,
    NgayBan DATE,
    MaKH INT,
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH)
);

-- 11. CTMH
CREATE TABLE CTMH (
    MaDMH INT,
    MaSP INT,
    SoLuong INT,
    DonGia DECIMAL(18,2),
    PRIMARY KEY (MaDMH, MaSP),
    FOREIGN KEY (MaDMH) REFERENCES DonMuaHang(MaDMH),
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);

-- 12. CTBH
CREATE TABLE CTBH (
    MaDBH INT,
    MaSP INT,
    SoLuong INT,
    DonGia DECIMAL(18,2),
    PRIMARY KEY (MaDBH, MaSP),
    FOREIGN KEY (MaDBH) REFERENCES DonBanHang(MaDBH),
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);


INSERT INTO NhomSP VALUES
(1, N'Đồ uống'),
(2, N'Đồ ăn nhẹ'),
(3, N'Sữa & chế phẩm'),
(4, N'Gia vị'),
(5, N'Đồ dùng gia đình');

-- 2. LoaiSP (liên kết NhomSP)
INSERT INTO LoaiSP VALUES
(1, N'Nước ngọt', 1),
(2, N'Nước khoáng', 1),
(3, N'Bánh quy', 2),
(4, N'Sữa tươi', 3),
(5, N'Muối ăn', 4);

-- 3. NhaCC
INSERT INTO NhaCC VALUES
(1, N'Công ty TNHH ABC', N'123 Nguyễn Trãi, Hà Nội', N'0901234567', N'contact@abc.com.vn'),
(2, N'Công ty TNHH XYZ', N'12 Lê Lợi, TP.HCM', N'0987654321', N'info@xyz.com.vn'),
(3, N'Công ty CP Việt Hưng', N'45 Hai Bà Trưng, Đà Nẵng', N'0912345678', N'sales@viethung.vn'),
(4, N'Công ty TNHH Đại Phát', N'22 Nguyễn Văn Cừ, Cần Thơ', N'0932123456', N'office@daiphat.com.vn'),
(5, N'Công ty TNHH GreenFood', N'88 Lý Thường Kiệt, Huế', N'0977654321', N'support@greenfood.vn');


-- 4. Tinh
INSERT INTO Tinh VALUES
(1, N'Hà Nội'),
(2, N'TP.Hồ Chí Minh'),
(3, N'Đà Nẵng'),
(4, N'Cần Thơ'),
(5, N'Huế');

-- 5. Xa (liên kết Tinh)
INSERT INTO Xa VALUES
(1, N'Phường Trung Hòa', 1),
(2, N'Phường Bến Nghé', 2),
(3, N'Phường Hải Châu', 3),
(4, N'Phường Ninh Kiều', 4),
(5, N'Phường Phú Hội', 5);

-- 6. KhachHang (liên kết Xa)
INSERT INTO KhachHang VALUES
(1, N'Nguyễn Văn A', N'12 Láng Hạ', N'0911111111', 1),
(2, N'Trần Thị B', N'55 Nguyễn Huệ', N'0922222222', 2),
(3, N'Lê Văn C', N'78 Lê Duẩn', N'0933333333', 3),
(4, N'Phạm Thị D', N'22 3/2', N'0944444444', 4),
(5, N'Hoàng Văn E', N'9 Lý Thường Kiệt', N'0955555555', 5);

-- 7. GianHang (liên kết Xa)
INSERT INTO GianHang VALUES
(1, N'Gian hàng A', 1),
(2, N'Gian hàng B', 2),
(3, N'Gian hàng C', 3),
(4, N'Gian hàng D', 4),
(5, N'Gian hàng E', 5);

-- 8. SanPham (liên kết LoaiSP, GianHang)
INSERT INTO SanPham VALUES
(1, N'Coca Cola 330ml', 10000, 500, 1, 1),
(2, N'Aquafina 500ml', 8000, 400, 2, 2),
(3, N'Bánh Oreo 100g', 15000, 300, 3, 3),
(4, N'Sữa Vinamilk 1L', 35000, 200, 4, 4),
(5, N'Muối Iốt 1kg', 7000, 250, 5, 5);

-- 9. DonMuaHang (liên kết NhaCC)
INSERT INTO DonMuaHang VALUES
(1, '2025-10-01', 1),
(2, '2025-10-05', 2),
(3, '2025-10-10', 3),
(4, '2025-10-15', 4),
(5, '2025-10-20', 5);

-- 10. DonBanHang (liên kết KhachHang)
INSERT INTO DonBanHang VALUES
(1, '2025-10-02', 1),
(2, '2025-10-06', 2),
(3, '2025-10-11', 3),
(4, '2025-10-16', 4),
(5, '2025-10-21', 5);

-- 11. CTMH (liên kết DonMuaHang, SanPham)
INSERT INTO CTMH VALUES
(1, 1, 100, 9000),
(2, 2, 80, 7000),
(3, 3, 60, 12000),
(4, 4, 50, 30000),
(5, 5, 40, 6000);

-- 12. CTBH (liên kết DonBanHang, SanPham)
INSERT INTO CTBH VALUES
(1, 1, 10, 12000),
(2, 2, 8, 9000),
(3, 3, 6, 18000),
(4, 4, 5, 40000),
(5, 5, 4, 8000);


-- Thủ tục lấy tất cả
CREATE PROCEDURE sp_NhaCC_GetAll
AS
BEGIN
    SELECT * FROM NhaCC ORDER BY MaNCC
END
GO
-- Thủ tục lấy theo ID
CREATE PROCEDURE sp_NhaCC_GetById
    @MaNCC INT
AS
BEGIN
    SELECT * FROM NhaCC WHERE MaNCC = @MaNCC
END
GO

-- Thủ tục thêm
CREATE PROCEDURE sp_NhaCC_Insert
    @TenNCC NVARCHAR(100),
	@DiaChiNCC NVARCHAR(200),
	@DienThoaiNCC NVARCHAR(12),
	@EmailNCC Nvarchar(320)
AS
BEGIN
    DECLARE @MaNCC INT
	set @MaNCC=1
	while(@MaNCC in (select MaNCC from NhaCC)) set @MaNCC=@MaNCC+1
    INSERT INTO NhaCC(MaNCC, TenNCC, DiaChiNCC, DienThoaiNCC, EmailNCC)
    VALUES(@MaNCC, @TenNCC, @DiaChiNCC, @DienThoaiNCC, @EmailNCC)
END
GO

-- Thủ tục cập nhật
CREATE PROCEDURE sp_NhaCC_Update
    @MaNCC INT,
    @TenNCC NVARCHAR(100),
	@DiaChiNCC NVARCHAR(200),
	@DienThoaiNCC NVARCHAR(12),
	@EmailNCC Nvarchar(320)
AS
BEGIN
    UPDATE NhaCC SET TenNCC=@TenNCC, DiaChiNCC=@DiaChiNCC, DienThoaiNCC=@DienThoaiNCC, EmailNCC=@EmailNCC WHERE MaNCC=@MaNCC
END
GO

-- Thủ tục xóa
CREATE PROCEDURE sp_NhaCC_Delete
    @MaNCC INT
AS
BEGIN
    DELETE FROM NhaCC WHERE MaNCC=@MaNCC
END
GO