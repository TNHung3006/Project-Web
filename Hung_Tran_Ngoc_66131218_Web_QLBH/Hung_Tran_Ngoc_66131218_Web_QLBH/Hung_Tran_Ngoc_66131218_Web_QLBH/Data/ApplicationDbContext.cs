using Humanizer.Localisation;
using Hung_Tran_Ngoc_66131218_Web_QLBH.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Hung_Tran_Ngoc_66131218_Web_QLBH.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts) : base(opts) { }

        //1.LoaiSP
        public DbSet<LoaiSP> lsp { get; set; }
        //2. SanPham
        public DbSet<SanPham> sp { get; set; }
        //3. Nhóm SanPham
        public DbSet<NhomSP> nsp { get; set; }
        //4. Nước
        public DbSet<Nuoc> n { get; set; }
        //5. Tỉnh
        public DbSet<Tinh> t { get; set; }
        //6. Xã
        public DbSet<Xa> x { get; set; }
        //7. Nhà Cung Cấp
        public DbSet<NhaCC> ncc { get; set; }
        //8. Khách Hàng
        public DbSet<KhachHang> kh { get; set; }
        //9. Đơn Mua Hàng
        public DbSet<DonMuaHang> dmh { get; set; }
        //10. Đơn Bán Hàng
        public DbSet<DonBanHang> dbh { get; set; }
        //11. Chi Tiết Bán Hàng
        public DbSet<CTBH> ctbh { get; set; }
        //12. Chi Tiết Mua Hàng
        public DbSet<CTMH> ctmh { get; set; }
        //13. Nhân Viên
        public DbSet<NhanVien> nv { get; set; }
        //14. Hãng sản xuất
        public DbSet<HangSX> hsx {  get; set; }
        //15. Trạng Thái
        public DbSet<TrangThai> tt { get; set; }
        //16. Đơn vị tính
        public DbSet<DonViTinh> dvt { get; set; }
        //17. Loại nhân viên
        public DbSet<LoaiNV> lnv { get; set; }
        //18. Trạng thái đơn mua hàng
        public DbSet<TrangThaiDMH> ttdmh { get; set; }
        //19. Trạng thái đơn mua hàng
        public DbSet<TrangThaiDBH> ttdbh { get; set; }

        //1. LoaiSP
        //1.1. Hàm LoaiSP_GetAll(): trả về danh sách các đối tượng thuộc lớp LoaiSP với dữ liệu lấy từ CSDL thông qua thủ tục lưu trữ LoaiSP_GetAll
        public List<LoaiSP> LoaiSP_GetAll()
        {
            //Tên mỗi cột trong thủ tục lưu trữ SQL LoaiSP_GetAll
            //bắt buộc phải trùng tên các thuộc tính trong lớp LoaiSp.cs tương ửng ở thư mục Models
            return lsp.FromSqlRaw("EXEC LoaiSP_GetAll").ToList();
        }
        //1.2. Hàm LoaiSP_GetById(): trả về 1 đối tượng thuộc lớp LoaiSP tương ứng MaLSP được cung cấp
        public LoaiSP? LoaiSP_GetById(int maLSP)
        {
            var p = new[]
            {
                new SqlParameter("@MaLSP", maLSP)
            };
            return lsp.FromSqlRaw("EXEC LoaiSP_GetById @MaLSP", p).ToList().FirstOrDefault();
        }

        //1.3. Hàm LoaiSP_Insert(): thêm 1 loại sản phẩm từ 1 đối tượng lsp thuộc lớp LoaiSP vào bảng LoaiSP trong CSDL
        public void LoaiSP_Insert(LoaiSP lsp)
        {
            var p = new[]
            {
                new SqlParameter("@TenLSP", lsp.TenLSP),
                new SqlParameter("@MaNhomSP", lsp.MaNhomSP)
            };
            Database.ExecuteSqlRaw("EXEC LoaiSP_Insert @TenLSP, @MaNhomSP", p);

        }

        //1.4. Hàm LoaiSP_Update(): sửa thông tin 1 loại sản phẩm từ 1 đối tượng lsp thuộc lớp LoaiSP vào bảng LoaiSP trong CSDL
        public void LoaiSP_Update(LoaiSP lsp)
        {
            var p = new[]
            {
                new SqlParameter("@MaLSP", lsp.MaLSP),
                new SqlParameter("@TenLSP", lsp.TenLSP),
                new SqlParameter("@MaNhomSP", lsp.MaNhomSP)
            };
            Database.ExecuteSqlRaw("EXEC LoaiSP_Update @MaLSP, @TenLSP, @MaNhomSP", p);
        }

        //1.5. Hàm LoaiSP_Delete(): xóa 1 loại sản phẩm với maLSP được chỉ ra
        public void LoaiSP_Delete(int maLSP)
        {
            var p = new SqlParameter("@MaLSP", maLSP);
            Database.ExecuteSqlRaw("EXEC LoaiSP_Delete @MaLSP", p);
        }

        //2. SanPham
        //2.1. Hàm SanPham_GetAll():
        public List<SanPham> SanPham_GetAll(string? search = null)
        {
            if (string.IsNullOrEmpty(search))
                return sp.FromSqlRaw("EXEC SanPham_GetAll @Search = NULL").ToList();

            var p = new SqlParameter("@Search", search);
            return sp.FromSqlRaw("EXEC SanPham_GetAll @Search", p).ToList();
        }
        //2.2. SanPham_GetById()
        public SanPham? SanPham_GetById(string maSP)
        {
            var p = new SqlParameter("@MaSP", maSP);
            return sp.FromSqlRaw("EXEC SanPham_GetById @MaSP", p).AsEnumerable().FirstOrDefault();
        }

        // 2.3.
        public string SanPham_Insert(SanPham sp)
        {
            using var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "SanPham_Insert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@TenSP", sp.TenSP));
            cmd.Parameters.Add(new SqlParameter("@DonGia", sp.DonGia));
            cmd.Parameters.Add(new SqlParameter("@SoLuong", sp.SoLuong));
            cmd.Parameters.Add(new SqlParameter("@AnhSP", sp.AnhSP?? (object)DBNull.Value));    
            cmd.Parameters.Add(new SqlParameter("@MaLSP", sp.MaLSP));
            cmd.Parameters.Add(new SqlParameter("@MaGH", sp.MaGH));
            Database.OpenConnection();
            string maSP = string.Empty;
            using var reader = cmd.ExecuteReader();
            if (reader.Read()) maSP = reader["MaSP"].ToString();
            Database.CloseConnection();
            return maSP;
        }
        //2.4.
        public void SanPham_Update(SanPham sp)
        {
            var p = new[]
            {
            new SqlParameter("@MaSP", sp.MaSP),
            new SqlParameter("@TenSP", sp.TenSP),
            new SqlParameter("@DonGia", sp.DonGia),
            new SqlParameter("@SoLuong", sp.SoLuong),
            new SqlParameter("@AnhSP", sp.AnhSP?? (object)DBNull.Value),
            new SqlParameter("@MaLSP", sp.MaLSP),
            new SqlParameter("@MaGH", sp.MaGH),
            };
            Database.ExecuteSqlRaw("EXEC SanPham_Update @MaSP, @TenSP, @DonGia, @Soluong, @AnhSP, @MaLSP, @MaGH", p);
        }
        //2.5.
        public void SanPham_Delete(string maSP)
        {
            var p = new SqlParameter("@MaSP", maSP);
            Database.ExecuteSqlRaw("EXEC SanPham_Delete @MaSP", p);
        }


        //3. NhomSP
        //3.1.
        public List<NhomSP> NhomSP_GetAll()
        {

            return nsp.FromSqlRaw("EXEC NhomSP_GetAll").ToList();
        }
        //3.2.
        public NhomSP? NhomSP_GetById(int nhomSP)
        {
            var p = new[]
            {
                new SqlParameter("@MaNhomSP", nhomSP)
            };
            return nsp.FromSqlRaw("EXEC NhomSP_GetById @MaNhomSP", p).ToList().FirstOrDefault();
        }

        //3.3.
        public void NhomSP_Insert(NhomSP nsp)
        {
            var p = new[]
            {
                new SqlParameter("@TenNhomSP", nsp.TenNhomSP)
            };
            Database.ExecuteSqlRaw("EXEC NhomSP_Insert @TenNhomSP", p);

        }

        //3.4.
        public void NhomSP_Update(NhomSP nsp)
        {
            var p = new[]
            {
                new SqlParameter("@MaNhomSP", nsp.MaNhomSP),
                new SqlParameter("@TenNhomSP", nsp.TenNhomSP)
            };
            Database.ExecuteSqlRaw("EXEC NhomSP_Update @MaNhomSP, @TenNhomSP", p);
        }

        //3.5.
        public void NhomSP_Delete(int manhomSP)
        {
            var p = new SqlParameter("@MaNhomSP", manhomSP);
            Database.ExecuteSqlRaw("EXEC NhomSP_Delete @MaNhomSP", p);
        }


        //4. Nước
        //4.1.
        public List<Nuoc> Nuoc_GetAll()
        {

            return n.FromSqlRaw("EXEC Nuoc_GetAll").ToList();
        }
        //4.2.
        public Nuoc? Nuoc_GetById(string nuoc)
        {
            var p = new[]
            {
                new SqlParameter("@MaNuoc", nuoc)
            };
            return n.FromSqlRaw("EXEC Nuoc_GetById @MaNuoc", p).ToList().FirstOrDefault();
        }
        //4.3.
        public void Nuoc_Insert(Nuoc nuoc)
        {
            var p = new[]
            {
                new SqlParameter("@MaNuoc", nuoc.MaNuoc),
                new SqlParameter("@TenNuoc", nuoc.TenNuoc)
            };
            Database.ExecuteSqlRaw("EXEC Nuoc_Insert @MaNuoc, @TenNuoc", p);
        }
        //4.4.
        public void Nuoc_Update(string id, Nuoc nuoc)
        {
            var p = new[]
            {
                new SqlParameter("@MaNuocCu", id),
                new SqlParameter("@MaNuocMoi", nuoc.MaNuoc),
                new SqlParameter("@TenNuoc", nuoc.TenNuoc)
            };
            Database.ExecuteSqlRaw("EXEC Nuoc_Update @MaNuocCu, @MaNuocMoi, @TenNuoc", p);
        }
        //4.5.
        public void Nuoc_Delete(string nuoc)
        {
            var p = new SqlParameter("@MaNuoc", nuoc);
            Database.ExecuteSqlRaw("EXEC Nuoc_Delete @MaNuoc", p);
        }


        //5. Tỉnh
        //5.1.
        public List<Tinh> Tinh_GetAll()
        {

            return t.FromSqlRaw("EXEC Tinh_GetAll").ToList();
        }
        //5.2.
        public Tinh? Tinh_GetById(int tinh)
        {
            var p = new[]
            {
                new SqlParameter("@MaTinh", tinh)
            };
            return t.FromSqlRaw("EXEC Tinh_GetById @MaTinh", p).ToList().FirstOrDefault();
        }

        //5.3.
        public void Tinh_Insert(Tinh tinh)
        {
            var p = new[]
            {
                new SqlParameter("@MaTinh", tinh.MaTinh),
                new SqlParameter("@TenTinh", tinh.TenTinh)
            };
            Database.ExecuteSqlRaw("EXEC Tinh_Insert @MaTinh, @TenTinh", p);

        }

        //5.4.
        public void Tinh_Update(int id, Tinh tinh)
        {
            var p = new[]
            {
                new SqlParameter("@MaTinhCu", id),
                new SqlParameter("@MaTinhMoi", tinh.MaTinh),
                new SqlParameter("@TenTinh", tinh.TenTinh)
            };
            Database.ExecuteSqlRaw("EXEC Tinh_Update @MaTinhCu, @MaTinhMoi, @TenTinh", p);
        }

        //5.5.
        public void Tinh_Delete(int tinh)
        {
            var p = new SqlParameter("@MaTinh", tinh);
            Database.ExecuteSqlRaw("EXEC Tinh_Delete @MaTinh", p);
        }


        //6. Xã
        //6.1.
        public List<Xa> Xa_GetAll()
        {

            return x.FromSqlRaw("EXEC Xa_GetAll").ToList();
        }
        //6.2.
        public Xa? Xa_GetById(int xa)
        {
            var p = new[]
            {
                new SqlParameter("@MaXa", xa)
            };
            return x.FromSqlRaw("EXEC Xa_GetById @MaXa", p).ToList().FirstOrDefault();
        }

        //6.3.
        public void Xa_Insert(Xa xa)
        {
            var p = new[]
            {
                new SqlParameter("@TenXa", xa.TenXa),
                new SqlParameter("@MaTinh", xa.MaTinh)
            };
            Database.ExecuteSqlRaw("EXEC Xa_Insert @TenXa, @MaTinh", p);

        }

        //6.4.
        public void Xa_Update(Xa xa)
        {
            var p = new[]
            {
                new SqlParameter("@Maxa", xa.MaXa),
                new SqlParameter("@TenXa", xa.TenXa),
                new SqlParameter("@MaTinh", xa.MaTinh)
            };
            Database.ExecuteSqlRaw("EXEC Xa_Update @MaXa, @TenXa, @MaTinh", p);
        }

        //6.5.
        public void Xa_Delete(int xa)
        {
            var p = new SqlParameter("@MaXa", xa);
            Database.ExecuteSqlRaw("EXEC Xa_Delete @MaXa", p);
        }


        //7. Nhà cung cấp
        //7.1.
        public List<NhaCC> NhaCC_GetAll()
        {

            return ncc.FromSqlRaw("EXEC NhaCC_GetAll").ToList();
        }
        //7.2.
        public NhaCC? NhaCC_GetById(int nhaCC)
        {
            var p = new[]
            {
                new SqlParameter("@MaNCC", nhaCC)
            };
            return ncc.FromSqlRaw("EXEC NhaCC_GetById @MaNCC", p).ToList().FirstOrDefault();
        }

        //7.3.
        public void NhaCC_Insert(NhaCC nhaCC)
        {
            var p = new[]
            {
                new SqlParameter("@TenNCC", nhaCC.TenNCC),
                new SqlParameter("@DiaChiNCC", nhaCC.DiaChiNCC),
                new SqlParameter("@DienThoaiNCC", nhaCC.DienThoaiNCC),
                new SqlParameter("@EmailNCC", nhaCC.EmailNCC)
            };
            Database.ExecuteSqlRaw("EXEC NhaCC_Insert @TenNCC, @DiaChiNCC, @DienThoaiNCC, @EmailNCC", p);

        }

        //7.4.
        public void NhaCC_Update(NhaCC nhaCC)
        {
            var p = new[]
            {
                new SqlParameter("@MaNCC", nhaCC.MaNCC),
                new SqlParameter("@TenNCC", nhaCC.TenNCC),
                new SqlParameter("@DiaChiNCC", nhaCC.DiaChiNCC),
                new SqlParameter("@DienThoaiNCC", nhaCC.DienThoaiNCC),
                new SqlParameter("@EmailNCC", nhaCC.EmailNCC)
            };
            Database.ExecuteSqlRaw("EXEC NhaCC_Update @MaNCC, @TenNCC, @DiaChiNCC, @DienThoaiNCC, @EmailNCC", p);
        }

        //7.5.
        public void NhaCC_Delete(int nhaCC)
        {
            var p = new SqlParameter("@MaNCC", nhaCC);
            Database.ExecuteSqlRaw("EXEC NhaCC_Delete @MaNCC", p);
        }


        //8. Khách hàng
        //8.1.
        public List<KhachHang> KhachHang_GetAll(string? search = null)
        {
            if (string.IsNullOrEmpty(search))
                return kh.FromSqlRaw("EXEC KhachHang_GetAll @Search = NULL").ToList();

            var p = new SqlParameter("@Search", search);
            return kh.FromSqlRaw("EXEC KhachHang_GetAll @Search", p).ToList();
        }

        //8.2.
        public KhachHang? KhachHang_GetById(int khachhang)
        {
            var p = new[]
            {
                new SqlParameter("@MaKH", khachhang)
            };
            return kh.FromSqlRaw("EXEC KhachHang_GetById @MaKH", p).ToList().FirstOrDefault();
        }

        public string KhachHang_Insert(KhachHang khachHang)
        {

            using var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "KhachHang_Insert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@TenKH", khachHang.TenKH));
            cmd.Parameters.Add(new SqlParameter("@AnhKH", khachHang.AnhKH ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@DiaChi", khachHang.DiaChi));
            cmd.Parameters.Add(new SqlParameter("@SDT", khachHang.SDT));
            cmd.Parameters.Add(new SqlParameter("@MaXa", khachHang.MaXa));
            Database.OpenConnection();
            string maKH = string.Empty;
            using var reader = cmd.ExecuteReader();
            if (reader.Read()) maKH = reader["MaKH"].ToString();
            Database.CloseConnection();
            return maKH;

        }

        //8.4.
        public void KhachHang_Update(KhachHang khachHang)
        {
            var p = new[]
            {
                new SqlParameter("@MaKH", khachHang.MaKH),
                new SqlParameter("@TenKH", khachHang.TenKH),
                new SqlParameter("@AnhKH", khachHang.AnhKH?? (object)DBNull.Value),
                new SqlParameter("@DiaChi", khachHang.DiaChi),
                new SqlParameter("@SDT", khachHang.SDT),
                new SqlParameter("@MaXa", khachHang.MaXa)
            };
            Database.ExecuteSqlRaw("EXEC KhachHang_Update @MaKH, @TenKH, @AnhKH, @DiaChi, @SDT, @MaXa", p);
        }

        //8.5.
        public void KhachHang_Delete(int khachhang)
        {
            var p = new SqlParameter("@MaKH", khachhang);
            Database.ExecuteSqlRaw("EXEC KhachHang_Delete @MaKH", p);
        }


        //9. Đơn Mua Hàng
        //9.1.
        public List<DonMuaHang> DonMuaHang_GetAll()
        {

            return dmh.FromSqlRaw("EXEC DonMuaHang_GetAll").ToList();
        }
        //9.2.
        public DonMuaHang? DonMuaHang_GetById(int donmuahang)
        {
            var p = new[]
            {
                new SqlParameter("@MaDMH", donmuahang)
            };
            return dmh.FromSqlRaw("EXEC DonMuaHang_GetById @MaDMH", p).ToList().FirstOrDefault();
        }

        //9.3.
        public void DonMuaHang_Insert(DonMuaHang dmh)
        {
            var p = new[]
            {
                new SqlParameter("@NgayMua", dmh.NgayMua),
                new SqlParameter("@MaNCC", dmh.MaNCC)
            };
            Database.ExecuteSqlRaw("EXEC DonMuaHang_Insert @NgayMua, @MaNCC", p);

        }

        //9.4.
        public void DonMuaHang_Update(DonMuaHang dmh)
        {
            var p = new[]
            {
                new SqlParameter("@MaDMH", dmh.MaDMH),
                new SqlParameter("@NgayMua", dmh.NgayMua),
                new SqlParameter("@MaNCC", dmh.MaNCC)
            };
            Database.ExecuteSqlRaw("EXEC DonMuaHang_Update @MaDMH, @NgayMua, @MaNCC", p);
        }

        //9.5.
        public void DonMuaHang_Delete(int dmh)
        {
            var p = new SqlParameter("@MaDMH", dmh);
            Database.ExecuteSqlRaw("EXEC DonMuaHang_Delete @MaDMH", p);
        }


        //10. Đơn Bán Hàng
        //10.1.
        public List<DonBanHang> DonBanHang_GetAll()
        {

            return dbh.FromSqlRaw("EXEC DonBanHang_GetAll").ToList();
        }
        //10.2.
        public DonBanHang? DonBanHang_GetById(int donbanhang)
        {
            var p = new[]
            {
                new SqlParameter("@MaDBH", donbanhang)
            };
            return dbh.FromSqlRaw("EXEC DonBanHang_GetById @MaDBH", p).ToList().FirstOrDefault();
        }

        //10.3.
        public void DonBanHang_Insert(DonBanHang dbh)
        {
            var p = new[]
            {
                new SqlParameter("@NgayBan", dbh.NgayBan),
                new SqlParameter("@MaKH", dbh.MaKH)
            };
            Database.ExecuteSqlRaw("EXEC DonBanHang_Insert @NgayBan, @MaKH", p);

        }

        //10.4.
        public void DonBanHang_Update(DonBanHang dbh)
        {
            var p = new[]
            {
                new SqlParameter("@MaDBH", dbh.MaDBH),
                new SqlParameter("@NgayBan", dbh.NgayBan),
                new SqlParameter("@MaKH", dbh.MaKH)
            };
            Database.ExecuteSqlRaw("EXEC DonBanHang_Update @MaDBH, @NgayBan, @MaKH", p);
        }

        //10.5.
        public void DonBanHang_Delete(int dbh)
        {
            var p = new SqlParameter("@MaDBH", dbh);
            Database.ExecuteSqlRaw("EXEC DonBanHang_Delete @MaDBH", p);
        }

        //11. CTBH
        //11.1.
        public List<CTBH> CTBH_GetAll()
        {

            return ctbh.FromSqlRaw("EXEC CTBH_GetAll").ToList();
        }
        //11.2.
        public CTBH? CTBH_GetById(int maDBH, int maSP)
        {
            var p = new[]
            {
                new SqlParameter("@MaDBH", maDBH),
                new SqlParameter("@MaSP", maSP)
            };
            return ctbh.FromSqlRaw("EXEC CTBH_GetById @MaDBH, @MaSP", p).ToList().FirstOrDefault();
        }

        //11.3.
        public void CTBH_Insert(CTBH ctBH)
        {
            var p = new[]
            {
                new SqlParameter("@MaDBH", ctBH.MaDBH),
                new SqlParameter("@MaSP", ctBH.MaSP),
                new SqlParameter("@SoLuong", ctBH.SLB),
                new SqlParameter("@DonGia", ctBH.DGB)
            };
            Database.ExecuteSqlRaw("EXEC CTBH_Insert @MaDBH, @MaSP, @SoLuong, @DonGia", p);

        }

        //11.4.
        public void CTBH_Update(CTBH ctBH)
        {
            var p = new[]
            {
                new SqlParameter("@MaDBH", ctBH.MaDBH),
                new SqlParameter("@MaSP", ctBH.MaSP),
                new SqlParameter("@SoLuong", ctBH.SLB),
                new SqlParameter("@DonGia", ctBH.DGB)
            };
            Database.ExecuteSqlRaw("EXEC CTBH_Update @MaDBH, @MaSP, @SoLuong, @DonGia", p);
        }

        //11.5.
        public void CTBH_Delete(int maDBH, int maSP)
        {
            var p = new[]
            {
                new SqlParameter("@MaDBH", maDBH),
                new SqlParameter("@MaSP", maSP) 
            };
            Database.ExecuteSqlRaw("EXEC CTBH_Delete @MaDBH, @MaSP", p);
        }


        //12. CTMH
        //12.1.
        public List<CTMH> CTMH_GetAll()
        {

            return ctmh.FromSqlRaw("EXEC CTMH_GetAll").ToList();
        }
        //12.2.
        public CTMH? CTMH_GetById(int maDMH, int maSP)
        {
            var p = new[]
            {
                new SqlParameter("@MaDMH", maDMH),
                new SqlParameter("@MaSP", maSP)
            };
            return ctmh.FromSqlRaw("EXEC CTMH_GetById @MaDMH, @MaSP", p).ToList().FirstOrDefault();
        }

        //12.3.
        public void CTMH_Insert(CTMH ctMH)
        {
            var p = new[]
            {
                new SqlParameter("@MaDMH", ctMH.MaDMH),
                new SqlParameter("@MaSP", ctMH.MaSP),
                new SqlParameter("@SoLuong", ctMH.SLM),
                new SqlParameter("@DonGia", ctMH.DGM)
            };
            Database.ExecuteSqlRaw("EXEC CTMH_Insert @MaDMH, @MaSP, @SoLuong, @DonGia", p);

        }

        //12.4.
        public void CTMH_Update(CTMH ctMH)
        {
            var p = new[]
            {
                new SqlParameter("@MaDMH", ctMH.MaDMH),
                new SqlParameter("@MaSP", ctMH.MaSP),
                new SqlParameter("@SoLuong", ctMH.SLM),
                new SqlParameter("@DonGia", ctMH.DGM)
            };
            Database.ExecuteSqlRaw("EXEC CTMH_Update @MaDMH, @MaSP, @SoLuong, @DonGia", p);
        }

        //12.5.
        public void CTMH_Delete(int maDMH, int maSP)
        {
            var p = new[]
            {
                new SqlParameter("@MaDMH", maDMH),
                new SqlParameter("@MaSP", maSP)
            };
            Database.ExecuteSqlRaw("EXEC CTBH_Delete @MaDMH, @MaSP", p);
        }


        //13. Nhân Viên
        //13.1.
        public List<NhanVien> NhanVien_GetAll()
        {
            return nv.FromSqlRaw("EXEC NhanVien_GetAll").ToList();
        }

        //13.2.
        public NhanVien? NhanVien_GetById(int nhanvien)
        {
            var p = new[]
            {
                new SqlParameter("@MaNV", nhanvien)
            };
            return nv.FromSqlRaw("EXEC NhanVien_GetById @MaNV", p).ToList().FirstOrDefault();
        }

        public void NhanVien_Insert(NhanVien nhanvien)
        {

            var p = new[]
            {
                new SqlParameter("@TenDN", nhanvien.TenDN),
                new SqlParameter("@MatKhau", nhanvien.MatKhau),
                new SqlParameter("@TenNV", nhanvien.TenNV),
                new SqlParameter("@DienThoai", nhanvien.DienThoai),
                new SqlParameter("@Email", nhanvien.Email),
                new SqlParameter("@DiaChi", nhanvien.DiaChi)
            };
            Database.ExecuteSqlRaw("EXEC NhanVien_Insert @TenDN, @MatKhau, @TenNV, @DienThoai, @Email, @DiaChi", p);

        }

        //13.4.
        public void NhanVien_Update(NhanVien nhanvien)
        {
            var p = new[]
            {
                new SqlParameter("@MaNV", nhanvien.MaNV),
                new SqlParameter("@TenDN", nhanvien.TenDN),
                new SqlParameter("@MatKhau", nhanvien.MatKhau),
                new SqlParameter("@TenNV", nhanvien.TenNV),
                new SqlParameter("@DienThoai", nhanvien.DienThoai),
                new SqlParameter("@Email", nhanvien.Email),
                new SqlParameter("@DiaChi", nhanvien.DiaChi)
            };
            Database.ExecuteSqlRaw("EXEC NhanVien_Update @MaNV, @TenDN, @MatKhau, @TenNV, @DienThoai, @Email, @DiaChi", p);

        }

        //13.5.
        public void NhanVien_Delete(int nhanvien)
        {
            var p = new SqlParameter("@MaNV", nhanvien);
            Database.ExecuteSqlRaw("EXEC NhanVien_Delete @MaNV", p);
        }

        //14. Hãng sản xuất
        //14.1.
        public List<HangSX> HangSX_GetAll()
        {

            return hsx.FromSqlRaw("EXEC HangSX_GetAll").ToList();
        }
        //14.2.
        public HangSX? HangSX_GetById(int hangSX)
        {
            var p = new[]
            {
                new SqlParameter("@MaHSX", hangSX)
            };
            return hsx.FromSqlRaw("EXEC HangSX_GetById @MaHSX", p).ToList().FirstOrDefault();
        }

        //14.3.
        public void HangSX_Insert(HangSX hangSX)
        {
            var p = new[]
            {
                new SqlParameter("@TenHSX", hangSX.TenHSX),
                new SqlParameter("@MaNuoc", hangSX.MaNuoc)
            };
            Database.ExecuteSqlRaw("EXEC HangSX_Insert @TenHSX, @MaNuoc", p);

        }

        //14.4.
        public void HangSX_Update(HangSX hangSX)
        {
            var p = new[]
            {
                new SqlParameter("@MaHSX", hangSX.MaHSX),
                new SqlParameter("@TenHSX", hangSX.TenHSX),
                new SqlParameter("@MaNuoc", hangSX.MaNuoc)
            };
            Database.ExecuteSqlRaw("EXEC HangSX_Update @MaHSX, @TenHSX, @MaNuoc", p);
        }

        //14.5. 
        public void HangSX_Delete(int mahsx)
        {
            var p = new SqlParameter("@MaHSX", mahsx);
            Database.ExecuteSqlRaw("EXEC HangSX_Delete @MaHSX", p);
        }

        //15. Trạng Thái
        //15.1.
        public List<TrangThai> TrangThai_GetAll()
        {

            return tt.FromSqlRaw("EXEC TrangThai_GetAll").ToList();
        }
        //15.2.
        public TrangThai? TrangThai_GetById(int trangthai)
        {
            var p = new[]
            {
                new SqlParameter("@MaTT", trangthai)
            };
            return tt.FromSqlRaw("EXEC TrangThai_GetById @MaTT", p).ToList().FirstOrDefault();
        }

        //15.3.
        public void TrangThai_Insert(TrangThai tt)
        {
            var p = new[]
            {
                new SqlParameter("@TenTT", tt.TenTT)
            };
            Database.ExecuteSqlRaw("EXEC TrangThai_Insert @TenTT", p);

        }

        //15.4.
        public void TrangThai_Update(TrangThai tt)
        {
            var p = new[]
            {
                new SqlParameter("@MaTT", tt.MaTT),
                new SqlParameter("@TenTT", tt.TenTT)
            };
            Database.ExecuteSqlRaw("EXEC TrangThai_Update @MaTT, @TenTT", p);
        }

        //15.5.
        public void TrangThai_Delete(int matt)
        {
            var p = new SqlParameter("@MaTT", matt);
            Database.ExecuteSqlRaw("EXEC TrangThai_Delete @MaTT", p);
        }


        //16. Đơn Vị Tính
        //16.1.
        public List<DonViTinh> DonViTinh_GetAll()
        {

            return dvt.FromSqlRaw("EXEC DonViTinh_GetAll").ToList();
        }
        //16.2.
        public DonViTinh? DonViTinh_GetById(int dvtinh)
        {
            var p = new[]
            {
                new SqlParameter("@MaDVT", dvtinh)
            };
            return dvt.FromSqlRaw("EXEC DonViTinh_GetById @MaDVT", p).ToList().FirstOrDefault();
        }

        //16.3.
        public void DonViTinh_Insert(DonViTinh dvt)
        {
            var p = new[]
            {
                new SqlParameter("@TenDVT", dvt.TenDVT)
            };
            Database.ExecuteSqlRaw("EXEC DonViTinh_Insert @TenDVT", p);

        }

        //16.4.
        public void DonViTinh_Update(DonViTinh dvt)
        {
            var p = new[]
            {
                new SqlParameter("@MaDVT", dvt.MaDVT),
                new SqlParameter("@TenDVT", dvt.TenDVT)
            };
            Database.ExecuteSqlRaw("EXEC DonViTinh_Update @MaDVT, @TenDVT", p);
        }

        //16.5.
        public void DonViTinh_Delete(int madvt)
        {
            var p = new SqlParameter("@MaDVT", madvt);
            Database.ExecuteSqlRaw("EXEC DonViTinh_Delete @MaDVT", p);
        }

        //17. Loại nhân viên
        //17.1.
        public List<LoaiNV> LoaiNV_GetAll()
        {

            return lnv.FromSqlRaw("EXEC LoaiNV_GetAll").ToList();
        }
        //17.2.
        public LoaiNV? LoaiNV_GetById(int loainv)
        {
            var p = new[]
            {
                new SqlParameter("@MaLNV", loainv)
            };
            return lnv.FromSqlRaw("EXEC LoaiNV_GetById @MaLNV", p).ToList().FirstOrDefault();
        }

        //17.3.
        public void LoaiNV_Insert(LoaiNV loainv)
        {
            var p = new[]
            {
                new SqlParameter("@TenLNV", loainv.TenLNV)
            };
            Database.ExecuteSqlRaw("EXEC LoaiNV_Insert @TenLNV", p);

        }

        //17.4.
        public void LoaiNV_Update(LoaiNV loainv)
        {
            var p = new[]
            {
                new SqlParameter("@MaLNV", loainv.MaLNV),
                new SqlParameter("@TenLNV", loainv.TenLNV)
            };
            Database.ExecuteSqlRaw("EXEC LoaiNV_Update @MaLNV, @TenLNV", p);
        }

        //17.5.
        public void LoaiNV_Delete(int malnv)
        {
            var p = new SqlParameter("@MaLNV", malnv);
            Database.ExecuteSqlRaw("EXEC LoaiNV_Delete @MaLNV", p);
        }

        //18. Trạng Thái đơn mua hàng
        //18.1.
        public List<TrangThaiDMH> TrangThaiDMH_GetAll()
        {

            return ttdmh.FromSqlRaw("EXEC TrangThaiDMH_GetAll").ToList();
        }
        //18.2.
        public TrangThaiDMH? TrangThaiDMH_GetById(int trangthai)
        {
            var p = new[]
            {
                new SqlParameter("@MaTTDMH", trangthai)
            };
            return ttdmh.FromSqlRaw("EXEC TrangThaiDMH_GetById @MaTTDMH", p).ToList().FirstOrDefault();
        }

        //18.3.
        public void TrangThaiDMH_Insert(TrangThaiDMH ttDMH)
        {
            var p = new[]
            {
                new SqlParameter("@TenTTDMH", ttDMH.TenTTDMH)
            };
            Database.ExecuteSqlRaw("EXEC TrangThaiDMH_Insert @TenTTDMH", p);

        }

        //18.4.
        public void TrangThaiDMH_Update(TrangThaiDMH ttDMH)
        {
            var p = new[]
            {
                new SqlParameter("@MaTTDMH", ttDMH.MaTTDMH),
                new SqlParameter("@TenTTDMH", ttDMH.TenTTDMH)
            };
            Database.ExecuteSqlRaw("EXEC TrangThaiDMH_Update @MaTTDMH, @TenTTDMH", p);
        }

        //18.5.
        public void TrangThaiDMH_Delete(int mattDMH)
        {
            var p = new SqlParameter("@MaTTDMH", mattDMH);
            Database.ExecuteSqlRaw("EXEC TrangThaiDMH_Delete @MaTTDMH", p);
        }

        //19. Trạng Thái đơn bán hàng
        //19.1.
        public List<TrangThaiDBH> TrangThaiDBH_GetAll()
        {

            return ttdbh.FromSqlRaw("EXEC TrangThaiDBH_GetAll").ToList();
        }
        //19.2.
        public TrangThaiDBH? TrangThaiDBH_GetById(int trangthai)
        {
            var p = new[]
            {
                new SqlParameter("@MaTTDBH", trangthai)
            };
            return ttdbh.FromSqlRaw("EXEC TrangThaiDBH_GetById @MaTTDBH", p).ToList().FirstOrDefault();
        }

        //19.3.
        public void TrangThaiDBH_Insert(TrangThaiDBH ttDBH)
        {
            var p = new[]
            {
                new SqlParameter("@TenTTDBH", ttDBH.TenTTDBH)
            };
            Database.ExecuteSqlRaw("EXEC TrangThaiDBH_Insert @TenTTDBH", p);

        }

        //19.4.
        public void TrangThaiDBH_Update(TrangThaiDBH ttDBH)
        {
            var p = new[]
            {
                new SqlParameter("@MaTTDBH", ttDBH.MaTTDBH),
                new SqlParameter("@TenTTDBH", ttDBH.TenTTDBH)
            };
            Database.ExecuteSqlRaw("EXEC TrangThaiDBH_Update @MaTTDBH, @TenTTDBH", p);
        }

        //19.5.
        public void TrangThaiDBH_Delete(int mattDBH)
        {
            var p = new SqlParameter("@MaTTDBH", mattDBH);
            Database.ExecuteSqlRaw("EXEC TrangThaiDBH_Delete @MaTTDBH", p);
        }

    }
}
