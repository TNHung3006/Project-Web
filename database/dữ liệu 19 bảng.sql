-- 1. NHÓM BẢNG DANH MỤC ĐỊA LÝ & CƠ BẢN
-- Bảng Nuoc (Giữ nguyên VARCHAR theo yêu cầu)
INSERT INTO Nuoc (MaNuoc, TenNuoc) VALUES 
('VN', N'Việt Nam'), ('US', N'Mỹ'), ('TH', N'Thái Lan'), ('JP', N'Nhật Bản'), ('KR', N'Hàn Quốc'), ('TW', N'Đài Loan'), ('CN', N'Trung Quốc');

-- Sử dụng mã hành chính/biển số phổ biến để dễ nhận diện
INSERT INTO Tinh (MaTinh, TenTinh) VALUES 
(79, N'Hồ Chí Minh'),  -- Mã 79
(58, N'Đà Nẵng'),      -- Mã 58 (theo dữ liệu cũ của bạn)
(1,  N'Hà Nội'),       -- Mã 01 chuyển thành 1
(56, N'Khánh Hòa'),    -- Mã 56
(48, N'Bình Dương'),   -- Mã 48 (theo dữ liệu cũ của bạn)
(60, N'Đồng Nai'),     -- Mã 60
(92, N'Cần Thơ');      -- Mã 92

-- Lưu ý: MaXa vẫn giữ là 1, 2, 3... để không ảnh hưởng đến các bảng Nhân Viên, Khách Hàng...
INSERT INTO Xa (MaXa, TenXa, MaTinh) VALUES 
(1, N'Phường Bến Nghé', 79),    -- Thuộc HCM (79)
(2, N'Phường Thạch Thang', 58), -- Thuộc Đà Nẵng (58)
(3, N'Phường Láng Hạ', 1),      -- Thuộc Hà Nội (1)
(4, N'Phường Lộc Thọ', 56),     -- Thuộc Khánh Hòa (56)
(5, N'Phường Dĩ An', 48),       -- Thuộc Bình Dương (48)
(6, N'Phường Tam Hiệp', 60),    -- Thuộc Đồng Nai (60)
(7, N'Phường Hưng Lợi', 92);    -- Thuộc Cần Thơ (92)
INSERT INTO Xa (MaXa, TenXa, MaTinh)
VALUES 
-- Tỉnh 79 (TP. Hồ Chí Minh)
(11, N'Phường Đa Kao', 79), (12, N'Phường Tân Định', 79), (13, N'Phường Võ Thị Sáu', 79),
-- Tỉnh 58 (Đà Nẵng)
(14, N'Phường Hải Châu I', 58), (15, N'Phường Hòa Cường Bắc', 58), (16, N'Phường Nam Dương', 58),
-- Tỉnh 1 (Hà Nội)
(17, N'Phường Thành Công', 1), (18, N'Phường Giảng Võ', 1), (19, N'Phường Kim Mã', 1),
-- Tỉnh 56 (Khánh Hòa)
(20, N'Phường Vĩnh Nguyên', 56), (21, N'Phường Phước Tiến', 56), (22, N'Xã Vĩnh Thái', 56),
-- Tỉnh 48 (Bình Dương)
(23, N'Phường An Bình', 48), (24, N'Phường Tân Đông Hiệp', 48), (25, N'Phường Bình Thắng', 48),
-- Tỉnh 60 (Đồng Nai)
(26, N'Phường Quyết Thắng', 60), (27, N'Phường Thống Nhất', 60), (28, N'Phường Trung Dũng', 60),
-- Tỉnh 92 (Cần Thơ)
(29, N'Phường An Khánh', 92), (30, N'Phường Xuân Khánh', 92), (31, N'Phường Cái Khế', 92),
-- Tỉnh 31 (Hải Phòng)
(32, N'Phường Máy Tơ', 31), (33, N'Phường Cầu Đất', 31), (34, N'Phường Lạch Tray', 31),
-- Tỉnh 68 (Lâm Đồng)
(35, N'Phường 2', 68), (36, N'Phường 9', 68), (37, N'Xã Xuân Thọ', 68),
-- Tỉnh 77 (Bà Rịa - Vũng Tàu)
(38, N'Phường 2', 77), (39, N'Phường Rạch Dừa', 77), (40, N'Phường Nguyễn An Ninh', 77);

-- Bảng DonViTinh (MaDVT: INT)
INSERT INTO DonViTinh (MaDVT, TenDVT) VALUES 
(1, N'Lon'), (2, N'Chai'), (3, N'Thùng'), (4, N'Lốc 6'), (5, N'Két'), (6, N'Hộp'), (7, N'Ly');

-- Bảng TrangThai (MaTT: INT)
INSERT INTO TrangThai (MaTT, TenTT) VALUES 
(1, N'Đang kinh doanh'), (2, N'Ngừng kinh doanh'), (3, N'Hết hàng'), (4, N'Sắp về hàng'), (5, N'Hàng khuyến mãi'), (6, N'Hàng trưng bày'), (7, N'Hàng lỗi');

-- Bảng TrangThaiDBH (MaTTDBH: INT)
INSERT INTO TrangThaiDBH (MaTTDBH, TenTTDBH) VALUES 
(1, N'Chờ xác nhận'), (2, N'Đang đóng gói'), (3, N'Đang giao hàng'), (4, N'Đã giao thành công'), (5, N'Khách hủy'), (6, N'Hoàn trả'), (7, N'Thất lạc');

-- Bảng TrangThaiDMH (MaTTDMH: INT)
INSERT INTO TrangThaiDMH (MaTTDMH, TenTTDMH) VALUES 
(1, N'Mới tạo'), (2, N'Đã gửi NCC'), (3, N'Đang vận chuyển'), (4, N'Đã nhập kho'), (5, N'Công nợ'), (6, N'Đã thanh toán'), (7, N'Hủy đơn');

-- 2. NHÓM BẢNG PHÂN LOẠI SẢN PHẨM
-- Bảng HangSX (MaHSX: INT, MaNuoc: VARCHAR)
INSERT INTO HangSX (MaHSX, TenHSX, MaNuoc) VALUES 
(1, N'Suntory Pepsico', 'VN'), (2, N'Coca-Cola', 'US'), (3, N'Tân Hiệp Phát', 'VN'), (4, N'URC', 'TH'), (5, N'Red Bull', 'TH'), (6, N'Masan Consumer', 'VN'), (7, N'Vinamilk', 'VN');

-- Bảng NhomSP (MaNhomSP: INT)
INSERT INTO NhomSP (MaNhomSP, TenNhomSP) VALUES 
(1, N'Nước ngọt có gas'), (2, N'Nước tăng lực'), (3, N'Trà đóng chai'), (4, N'Nước suối'), (5, N'Nước trái cây'), (6, N'Sữa trái cây'), (7, N'Cà phê đóng lon');

-- Bảng LoaiSP (MaLSP: INT, MaNhomSP: INT)
INSERT INTO LoaiSP (MaLSP, TenLSP, MaNhomSP) VALUES 
(1, N'Cola', 1), (2, N'Chanh', 1), (3, N'Dâu', 1), (4, N'Tăng lực thường', 2), (5, N'Trà xanh', 3), (6, N'Trà ô long', 3), (7, N'Khoáng thiên nhiên', 4);

-- 3. NHÓM CON NGƯỜI
-- Bảng LoaiNV (MaLNV: INT)
INSERT INTO LoaiNV (MaLNV, TenLNV) VALUES 
(1, N'Quản lý cửa hàng'), (2, N'Thu ngân'), (3, N'Nhân viên kho'), (4, N'Bảo vệ'), (5, N'Nhân viên giao hàng'), (6, N'Kế toán'), (7, N'Tạp vụ');

-- Bảng NhanVien (MaNV: INT, các FK là INT)
INSERT INTO NhanVien (MaNV, TenDN, MatKhau, HoNV, TenNV, GioiTinh, DienThoai, Email, DiaChi, MaLNV, MaXa) VALUES 
(1, 'admin', '123456', N'Nguyễn', N'Văn A', N'Nam', '0909000111', 'a@gmail.com', N'123 Lê Lợi', 1, 1),
(2, 'thungan1', '123456', N'Trần', N'Thị B', N'Nữ', '0909000222', 'b@gmail.com', N'45 Nguyễn Huệ', 2, 1),
(3, 'kho1', '123456', N'Lê', N'Văn C', N'Nam', '0909000333', 'c@gmail.com', N'78 Pasteur', 3, 2),
(4, 'baove1', '123456', N'Phạm', N'Văn D', N'Nam', '0909000444', 'd@gmail.com', N'90 Hai Bà Trưng', 4, 3),
(5, 'shipper1', '123456', N'Hoàng', N'Văn E', N'Nam', '0909000555', 'e@gmail.com', N'12 Trần Phú', 5, 4),
(6, 'ketoan1', '123456', N'Võ', N'Thị F', N'Nữ', '0909000666', 'f@gmail.com', N'34 Hùng Vương', 6, 5),
(7, 'tapvu1', '123456', N'Đặng', N'Thị G', N'Nữ', '0909000777', 'g@gmail.com', N'56 Lý Thường Kiệt', 7, 6);

-- Bảng KhachHang (MaKH: INT)
INSERT INTO KhachHang (MaKH, TenDN, MatKhau, HoKH, TenKH, AnhKH, DiaChi, SDT, MaXa) VALUES 
(1, 'khach1', '123', N'Lý', N'Tiểu Long', NULL, N'111 Võ Văn Tần', '0912345678', 1),
(2, 'khach2', '123', N'Châu', N'Tinh Trì', NULL, N'222 Cách Mạng', '0912345679', 2),
(3, 'khach3', '123', N'Thành', N'Long', NULL, N'333 Điện Biên Phủ', '0912345680', 3),
(4, NULL, NULL, N'Lưu', N'Đức Hoa', NULL, N'444 Nguyễn Trãi', '0912345681', 4),
(5, NULL, NULL, N'Quách', N'Phú Thành', NULL, N'555 Lê Hồng Phong', '0912345682', 5),
(6, NULL, NULL, N'Trương', N'Học Hữu', NULL, N'666 Trần Hưng Đạo', '0912345683', 6),
(7, NULL, NULL, N'Lê', N'Minh', NULL, N'777 An Dương Vương', '0912345684', 7);

-- Bảng NhaCC (MaNCC: INT)
INSERT INTO NhaCC (MaNCC, TenNCC, DiaChiNCC, DienThoaiNCC, EmailNCC, MaXa) VALUES 
(1, N'Đại lý Bia Nước Ngọt Thành Đạt', N'Số 1 KCN Tân Bình', '02838111111', 'thanhdat@gmail.com', 5),
(2, N'Công ty CP TMDV Sài Gòn', N'Số 5 Nguyễn Văn Linh', '02838222222', 'saigon@gmail.com', 1),
(3, N'Nhà phân phối Minh Hưng', N'Số 10 Quốc Lộ 1A', '02838333333', 'minhhung@gmail.com', 6),
(4, N'Đại lý Nước giải khát Phương Trang', N'Số 20 Xa Lộ Hà Nội', '02838444444', 'phuongtrang@gmail.com', 2),
(5, N'Công ty TNHH Hoàng Long', N'Số 30 Phạm Văn Đồng', '02838555555', 'hoanglong@gmail.com', 3),
(6, N'NPP Khánh Hòa', N'Số 40 Đường 2/4', '05838666666', 'khanhhoa@gmail.com', 4),
(7, N'Tổng kho Cần Thơ', N'Số 50 Đường 3/2', '02923877777', 'cantho@gmail.com', 7);

-- 4. BẢNG SẢN PHẨM
-- MaSP: INT. Các FK (Loai, DVT, TT, HSX) đều là INT
INSERT INTO SanPham (MaSP, TenSP, Dongia, AnhSP, MoTaSP, MaLSP, MaDVT, MaTT, MaHSX) VALUES 
(1, N'Nước ngọt Coca Cola', 10500, NULL, N'Vị nguyên bản, ngon hơn khi uống lạnh', 1, 1, 1, 2),
(2, N'Nước ngọt Pepsi', 10000, NULL, N'Thích thì uống không thích thì uống', 1, 1, 1, 1),
(3, N'Nước ngọt 7Up', 11000, NULL, N'Vị chanh tươi mát lạnh', 2, 2, 1, 1),
(4, N'Nước tăng lực Sting Dâu', 12000, NULL, N'Bật năng lượng sống bứt phá', 3, 2, 1, 1),
(5, N'Trà xanh C2 hương chanh', 8500, NULL, N'Mát lạnh thanh khiết', 5, 2, 1, 4),
(6, N'Nước tăng lực RedBull', 13000, NULL, N'Húc tung thách thức', 4, 1, 1, 5),
(7, N'Trà Ô long Tea+ Plus', 10000, NULL, N'Giúp hạn chế hấp thu chất béo', 6, 2, 1, 1);

-- 5. NHÓM GIAO DỊCH
-- Bảng DonMuaHang (MaDMH: INT, FK đều là INT)
INSERT INTO DonMuaHang (MaDMH, NgayMua, MaNCC, MaNV, MaTTDMH) VALUES 
(1, '2023-10-01 08:00:00', 1, 1, 4),
(2, '2023-10-02 09:30:00', 2, 3, 4),
(3, '2023-10-03 10:15:00', 3, 1, 6),
(4, '2023-10-04 14:00:00', 4, 3, 3),
(5, '2023-10-05 16:45:00', 5, 1, 1),
(6, '2023-10-06 08:30:00', 6, 3, 5),
(7, '2023-10-07 11:00:00', 7, 1, 7);

-- Bảng DonBanHang (MaDBH: INT, FK đều là INT)
INSERT INTO DonBanHang (MaDBH, NgayBan, DiaChiGH, MaKH, MaXa, MaTTDBH) VALUES 
(1, '2023-10-10 09:00:00', N'Nhà riêng', 1, 1, 4),
(2, '2023-10-10 10:30:00', N'Công ty', 2, 2, 4),
(3, '2023-10-11 12:00:00', NULL, 3, 3, 4),
(4, '2023-10-12 15:30:00', N'Giao cổng sau', 4, 4, 3),
(5, '2023-10-13 18:00:00', N'Gọi trước khi giao', 5, 5, 2),
(6, '2023-10-14 08:15:00', N'Văn phòng', 6, 6, 1),
(7, '2023-10-15 20:00:00', NULL, 7, 7, 5);

-- 6. NHÓM CHI TIẾT
-- Bảng CTMH (FK MaDMH và MaSP là INT)
INSERT INTO CTMH (MaDMH, MaSP, SLM, DGM) VALUES 
(1, 1, 100, 8000),
(2, 2, 200, 7500),
(3, 3, 150, 8500),
(4, 4, 100, 9000),
(5, 5, 300, 6000),
(6, 6, 120, 10000),
(7, 7, 80, 7500);

-- Bảng CTBH (FK MaDBH và MaSP là INT)
INSERT INTO CTBH (MaDBH, MaSP, SLB, DGB) VALUES 
(1, 1, 5, 10500),
(2, 2, 10, 10000),
(3, 3, 2, 11000),
(4, 4, 24, 12000),
(5, 5, 6, 8500),
(6, 6, 4, 13000),
(7, 7, 3, 10000);

-- thêm dữ liệu
-- 1. NHÓM BẢNG DANH MỤC ĐỊA LÝ & CƠ BẢN
-- Bảng Nuoc (Mã vẫn là VARCHAR)
INSERT INTO Nuoc (MaNuoc, TenNuoc) VALUES 
('SG', N'Singapore'), 
('DE', N'Đức'), 
('FR', N'Pháp');

-- Bảng Tinh (Dùng mã hành chính thực tế)
-- 31: Hải Phòng, 68: Lâm Đồng, 77: Bà Rịa - Vũng Tàu
INSERT INTO Tinh (MaTinh, TenTinh) VALUES 
(31, N'Hải Phòng'), 
(68, N'Lâm Đồng'), 
(77, N'Bà Rịa - Vũng Tàu');

-- Bảng Xa (Tiếp tục ID 8, 9, 10 - Map với mã Tỉnh mới ở trên)
INSERT INTO Xa (MaXa, TenXa, MaTinh) VALUES 
(8, N'Phường Minh Khai', 31),   -- Thuộc Hải Phòng
(9, N'Phường 1', 68),          -- Thuộc Đà Lạt, Lâm Đồng
(10, N'Phường Thắng Tam', 77); -- Thuộc Vũng Tàu

-- Bảng DonViTinh (Tiếp tục ID 8, 9, 10)
INSERT INTO DonViTinh (MaDVT, TenDVT) VALUES 
(8, N'Gói'), 
(9, N'Hũ'), 
(10, N'Block 6 chai');

-- Bảng TrangThai (Tiếp tục ID 8, 9, 10)
INSERT INTO TrangThai (MaTT, TenTT) VALUES 
(8, N'Hàng đặt trước'), 
(9, N'Hàng cận date'), 
(10, N'Tạm ngưng nhập');

-- Bảng TrangThaiDBH (Đơn bán - Tiếp tục ID 8, 9, 10)
INSERT INTO TrangThaiDBH (MaTTDBH, TenTTDBH) VALUES 
(8, N'Đã nhận tiền'), 
(9, N'Đang khiếu nại'), 
(10, N'Chờ kho xuất');

-- Bảng TrangThaiDMH (Đơn mua - Tiếp tục ID 8, 9, 10)
INSERT INTO TrangThaiDMH (MaTTDMH, TenTTDMH) VALUES 
(8, N'Chờ duyệt giá'), 
(9, N'Đang kiểm hàng'), 
(10, N'Trả lại NCC');

-- 2. NHÓM BẢNG PHÂN LOẠI SẢN PHẨM
-- Bảng HangSX (Tiếp tục ID 8, 9, 10 - Các hãng mới trên BHX)
INSERT INTO HangSX (MaHSX, TenHSX, MaNuoc) VALUES 
(8, N'Kirin', 'JP'), 
(9, N'Wonderfarm', 'VN'), 
(10, N'Bidrico', 'VN');

-- Bảng NhomSP (Tiếp tục ID 8, 9, 10)
INSERT INTO NhomSP (MaNhomSP, TenNhomSP) VALUES 
(8, N'Trà sữa đóng chai'), 
(9, N'Nước yến ngân nhĩ'), 
(10, N'Soda pha chế');

-- Bảng LoaiSP (Tiếp tục ID 8, 9, 10)
INSERT INTO LoaiSP (MaLSP, TenLSP, MaNhomSP) VALUES 
(8, N'Trà sữa Latte', 8), 
(9, N'Nước yến lon', 9), 
(10, N'Soda không đường', 10);

-- 3. NHÓM CON NGƯỜI
-- Bảng LoaiNV (Tiếp tục ID 8, 9, 10)
INSERT INTO LoaiNV (MaLNV, TenLNV) VALUES 
(8, N'Trưởng ca'), 
(9, N'Thực tập sinh'), 
(10, N'Nhân viên Marketing');

-- Bảng NhanVien (Tiếp tục ID 8, 9, 10)
INSERT INTO NhanVien (MaNV, TenDN, MatKhau, HoNV, TenNV, GioiTinh, DienThoai, Email, DiaChi, MaLNV, MaXa) VALUES 
(8, 'truongca1', '123456', N'Đỗ', N'Văn H', N'Nam', '0909000888', 'h@gmail.com', N'11 Điện Biên Phủ', 8, 8),
(9, 'thuctap1', '123456', N'Bùi', N'Thị I', N'Nữ', '0909000999', 'i@gmail.com', N'22 Lê Duẩn', 9, 9),
(10, 'marketing1', '123456', N'Dương', N'Văn K', N'Nam', '0909000000', 'k@gmail.com', N'33 Trần Phú', 10, 10);

-- Bảng KhachHang (Tiếp tục ID 8, 9, 10)
INSERT INTO KhachHang (MaKH, TenDN, MatKhau, HoKH, TenKH, AnhKH, DiaChi, SDT, MaXa) VALUES 
(8, 'khach8', '123', N'Châu', N'Kiệt Luân', NULL, N'888 Đường Láng', '0912345685', 8),
(9, 'khach9', '123', N'Cổ', N'Thiên Lạc', NULL, N'999 Hồ Xuân Hương', '0912345686', 9),
(10, NULL, NULL, N'Lương', N'Triều Vỹ', NULL, N'101 Thùy Vân', '0912345687', 10);

-- Bảng NhaCC (Tiếp tục ID 8, 9, 10)
INSERT INTO NhaCC (MaNCC, TenNCC, DiaChiNCC, DienThoaiNCC, EmailNCC, MaXa) VALUES 
(8, N'NPP Đồ uống Hải Phòng', N'Khu cảng Đình Vũ', '02253888888', 'haiphong@gmail.com', 8),
(9, N'Đại lý Nước ngọt Lâm Đồng', N'Chợ Đà Lạt', '02633999999', 'lamdong@gmail.com', 9),
(10, N'Công ty TNHH Vũng Tàu', N'KCN Đông Xuyên', '02543000000', 'vungtau@gmail.com', 10);

-- 4. BẢNG SẢN PHẨM (Sản phẩm mới tương ứng với Hãng và Loại mới)
-- ID 8: Trà sữa Kirin (Hãng 8, Loại 8)
-- ID 9: Nước yến Wonderfarm (Hãng 9, Loại 9)
-- ID 10: Soda Bidrico (Hãng 10, Loại 10)
INSERT INTO SanPham (MaSP, TenSP, Dongia, AnhSP, MoTaSP, MaLSP, MaDVT, MaTT, MaHSX) VALUES 
(8, N'Trà sữa Kirin Latte', 14000, NULL, N'Thơm béo đậm đà', 8, 2, 1, 8),
(9, N'Nước yến Ngân nhĩ Wonderfarm', 9000, NULL, N'Bổ dưỡng thanh mát', 9, 1, 1, 9),
(10, N'Soda Bidrico', 6000, NULL, N'Dùng để pha chế', 10, 1, 1, 10);

-- 5. NHÓM GIAO DỊCH (ĐƠN HÀNG)
-- Bảng DonMuaHang (Tiếp tục ID 8, 9, 10)
INSERT INTO DonMuaHang (MaDMH, NgayMua, MaNCC, MaNV, MaTTDMH) VALUES 
(8, '2023-10-20 08:00:00', 8, 8, 4),
(9, '2023-10-21 09:30:00', 9, 9, 1),
(10, '2023-10-22 10:15:00', 10, 10, 2);

-- Bảng DonBanHang (Tiếp tục ID 8, 9, 10)
INSERT INTO DonBanHang (MaDBH, NgayBan, DiaChiGH, MaKH, MaXa, MaTTDBH) VALUES 
(8, '2023-10-25 09:00:00', N'Giao giờ hành chính', 8, 8, 4),
(9, '2023-10-26 10:30:00', N'Gọi em gái nhận hộ', 9, 9, 3),
(10, '2023-10-27 12:00:00', NULL, 10, 10, 1);

-- 6. NHÓM CHI TIẾT GIAO DỊCH
-- Bảng CTMH (Chi tiết mua cho đơn 8, 9, 10 với sản phẩm 8, 9, 10)
INSERT INTO CTMH (MaDMH, MaSP, SLM, DGM) VALUES 
(8, 8, 50, 10000), -- Nhập trà sữa giá 10k
(9, 9, 100, 6500), -- Nhập yến giá 6.5k
(10, 10, 200, 4000); -- Nhập soda giá 4k

-- Bảng CTBH (Chi tiết bán cho đơn 8, 9, 10)
INSERT INTO CTBH (MaDBH, MaSP, SLB, DGB) VALUES 
(8, 8, 2, 14000),  -- Bán trà sữa giá 14k
(9, 9, 10, 9000),  -- Bán yến giá 9k
(10, 10, 24, 6000); -- Bán soda giá 6k
