using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlBangLuongThangTruyThuRepository : Repository<TlBangLuongThangTruyThu>, ITlBangLuongThangTruyThuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlBangLuongThangTruyThuRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int DeleteByParentId(Guid parentId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.TlBangLuongThangTruyThus.RemoveRange(ctx.TlBangLuongThangTruyThus.Where(x => x.Parent == parentId));
                return ctx.SaveChanges();
            }
        }
        public IEnumerable<TlDmCanBo> FindCb(decimal? thang, decimal? nam, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBos.Include(x => x.TlDmCapBac).Where(x => x.Thang == thang && x.Nam == nam && x.Parent == maDonVi && x.IsDelete == true && x.KhongLuong == false).ToList();
            }
        }

        public IEnumerable<TlDmThueThuNhapCaNhan> FindThue(bool bIsThueThang)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmThueThuNhapCaNhans.Where(x => x.BIsThueThang == bIsThueThang && x.LoaiThue.StartsWith("B")).OrderBy(x => x.ThuNhapTu).ToList();
            }
        }

        public IEnumerable<TlBangLuongThangTruyThu> FindByParentId(Guid parentId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlBangLuongThangTruyThus.Where(x => x.Parent == parentId).ToList();
            }
        }

        public DataTable ReportBangLuongThang(string maDonVi, int thang, int nam, bool isOrderChucVu, bool isGiaTriAm, bool isCheckedMaHuongLuong, bool isInCanBoMoi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_bangthanhtoan_luongthang";
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@IsOrderChucVu", isOrderChucVu),
                    new SqlParameter("@IsGiaTriAm", isGiaTriAm),
                    new SqlParameter("@IsCheckedMaHuongLuong", isCheckedMaHuongLuong),
                    new SqlParameter("@IsNew", isInCanBoMoi)
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public DataTable ReportBangLuongThangDong(string maDonVi, int thang, int nam, bool isOrderChucVu, List<string> lstColumnInclude)
        {
            List<string> lstHeader = new List<string>();
            int i = 1;
            foreach (var item in lstColumnInclude)
            {
                lstHeader.Add(string.Format("{0} as Column_{1}", item, i));
                ++i;
            }
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_bangthanhtoan_luongthang_dong";
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@IsOrderChucVu", isOrderChucVu),
                    new SqlParameter("@lstColumnInclude", string.Join(",", lstColumnInclude.Where(n=>n != "NULL"))),
                    new SqlParameter("@lstHeaderInclude", string.Join(",", lstHeader))
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public DataTable ReportBangLuongThangTheoDonVi(string maDonVi, int thang, int nam, bool isOrderChucVu, bool isGiaTriAm, bool isCheckedMaHuongLuong)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_bangthanhtoan_luongthang_2";
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@IsOrderChucVu", isOrderChucVu),
                    new SqlParameter("@IsGiaTriAm", isGiaTriAm),
                    new SqlParameter("@IsCheckedMaHuongLuong", isCheckedMaHuongLuong)
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public DataTable ReportBangLuongThangTheoDonViTruBHXH(string maDonVi, int thang, int nam, bool isOrderChucVu, bool isGiaTriAm, bool isCheckedMaHuongLuong)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2";
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@IsOrderChucVu", isOrderChucVu),
                    new SqlParameter("@IsGiaTriAm", isGiaTriAm),
                    new SqlParameter("@IsCheckedMaHuongLuong", isCheckedMaHuongLuong)
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public DataTable ReportBangLuongTruyLinh(string maDonVi, int thang, int nam, bool isTruyLinh, bool isOrderChucVu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "sp_tl_rpt_bangluong_truylinh";
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@IsTruyLinh", isTruyLinh),
                    new SqlParameter("@IsOrderChucVu", isOrderChucVu)
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public DataTable GetDataLuongThang(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_bangluong_thang_chitiet";
                var parameters = new[]
                {
                    new SqlParameter("@Id", id)
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public IEnumerable<TlBangLuongThangTruyThu> FindMaCanBo(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlBangLuongThangTruyThus.Where(x => x.MaCbo == maCanBo).ToList();
            }
        }

        public DataTable RptDSChiTraCaNhanNganHang(int nam, int thang, List<TlDmDonVi> tlDmDonVis)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                DataTable dataTable = new DataTable();

                var connection = ctx.Database.GetDbConnection();
                DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(connection);
                var cmd = connection.CreateCommand();
                cmd.CommandText = "rpt_ds_chi_tra_ca_nhan_ngan_hang";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nam", nam));
                cmd.Parameters.Add(new SqlParameter("@thang", thang));
                cmd.Parameters.Add(new SqlParameter("@maDonVi", string.Join(",", tlDmDonVis.Select(t => t.MaDonVi))));

                using (DbDataAdapter adapter = dbProviderFactory.CreateDataAdapter())
                {
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dataTable);
                }

                return dataTable;
            }
        }

        public DataTable ReportBangKeTrichThueTNCN(int thang, int nam, string maCachTl, string maDonVi, bool isExportAll, bool isOrderChucVu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "sp_tl_rpt_bangke_trichthue_tncn";
                var parameters = new[]
                {
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@MaCachTL", maCachTl),
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@IsExportAll", isExportAll),
                    new SqlParameter("@IsOrderChucVu", isOrderChucVu)
                };

                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public DataTable ExportGiaiThichPhuCapKhac(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                DataTable dataTable = new DataTable();

                var connection = ctx.Database.GetDbConnection();
                DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(connection);
                var cmd = connection.CreateCommand();
                cmd.CommandText = "sp_tl_rpt_bangluongthang_phucapkhac";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MaCanBo", maCanBo));

                using (DbDataAdapter adapter = dbProviderFactory.CreateDataAdapter())
                {
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dataTable);
                }

                return dataTable;
            }
        }

        public DataTable ExportGiaiThichPhaiTru(string maCanBo, string maPhuCap)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                DataTable dataTable = new DataTable();

                var connection = ctx.Database.GetDbConnection();
                DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(connection);
                var cmd = connection.CreateCommand();
                cmd.CommandText = "sp_tl_rpt_bangluongthang_phaitru";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MaCanBo", maCanBo));
                cmd.Parameters.Add(new SqlParameter("@maPhuCap", maPhuCap));

                using (DbDataAdapter adapter = dbProviderFactory.CreateDataAdapter())
                {
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dataTable);
                }

                return dataTable;
            }
        }

        public IEnumerable<TlRptDienBienLuongQuery> GetDataBangLuong()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_lay_du_lieu_bang_luong";
                return ctx.FromSqlRaw<TlRptDienBienLuongQuery>(sql).ToList();
            }
        }

        public IEnumerable<TlRptTruyLinhChuyenCheDoQuery> ReportTruyLinhChuyenCheDo(string maDonVi, int thangTruoc, int namTruoc, int thangSau, int namSau, string maHieuCanBo, bool isOrderChucVu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_rpt_truylinh_chuyenchedo @maDonVi, @thangTruoc, @namTruoc, @thangSau, @namSau, @maHieuCanBo, @IsOrderChucVu";
                var parameters = new[]
                {
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@thangTruoc", thangTruoc),
                    new SqlParameter("@namTruoc", namTruoc),
                    new SqlParameter("@thangSau", thangSau),
                    new SqlParameter("@namSau", namSau),
                    new SqlParameter("@maHieuCanBo", maHieuCanBo),
                    new SqlParameter("@IsOrderChucVu", isOrderChucVu)
                };
                return ctx.FromSqlRaw<TlRptTruyLinhChuyenCheDoQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlBangLuongThangDongQuery> ReportBangLuongThangDong(string maDonVi, string ngach, string maPhuCap, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_bang_luong_thang_dong @maDonVi, @ngach, @maPhuCap, @thang, @nam";
                var parameters = new[]
                {
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@ngach", ngach),
                    new SqlParameter("@maPhuCap", maPhuCap),
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam)
                };
                return ctx.FromSqlRaw<TlBangLuongThangDongQuery>(sql, parameters).ToList();
            }
        }

        public DataTable ReportBangLuongThangDoc(string maDonVi, string ngach, string maPhuCap, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "sp_tl_bang_luong_thang_doc";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@ngach", ngach),
                    new SqlParameter("@maPhuCap", maPhuCap),
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam)
                };
                return ctx.FromSqlCommand(sql, CommandType.StoredProcedure, 600, parameters);
            }
        }

        public DataTable ReportDienBienLuong(string maHieuCanBo, string tuNgay, string denNgay)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                DataTable dataTable = new DataTable();

                var connection = ctx.Database.GetDbConnection();
                DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(connection);
                var cmd = connection.CreateCommand();
                cmd.CommandText = "sp_tl_rpt_dienbien_luong";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@maHieuCanBo", maHieuCanBo));
                cmd.Parameters.Add(new SqlParameter("@TuNgay", tuNgay));
                cmd.Parameters.Add(new SqlParameter("@DenNgay", denNgay));

                using (DbDataAdapter adapter = dbProviderFactory.CreateDataAdapter())
                {
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dataTable);
                }

                return dataTable;
            }
        }

        public DataTable FindLuongNgachCanBo(int thang, int nam, string maDonVi, string maCachTL, int donViTinh, bool isSummary)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "sp_tl_rpt_tonghop_luong_theo_ngach";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@donViTinh", donViTinh),
                    new SqlParameter("@isSummary", isSummary),
                    new SqlParameter("@maCachTl", maCachTL)
                };

                return ctx.FromSqlCommand(sql, CommandType.StoredProcedure, 600, parameters);
            }
        }

        public DataTable FindLuongDonViCanBo(int thang, int nam, string maDonVi, string maCachTL, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "sp_tl_rpt_tonghop_luong_theo_don_vi";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@donViTinh", donViTinh),
                    new SqlParameter("@maCachTl", maCachTL)
                };

                return ctx.FromSqlCommand(sql, CommandType.StoredProcedure, 600, parameters);
            }
        }

        public DataTable FindLuongNgachDonViCanBo(int thang, int nam, string maDonVi, string maCachTL, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "sp_tl_rpt_tonghop_luong_theo_ngach_don_vi";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@donViTinh", donViTinh),
                    new SqlParameter("@maCachTl", maCachTL)
                };

                return ctx.FromSqlCommand(sql, CommandType.StoredProcedure, 600, parameters);
            }
        }

        public IEnumerable<TlBangLuongThangQuery> GetDataInsert(int thang, int nam, string maDonVi, string maCachTl, int soNgay)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_bangluong_thang_dulieu_insert @Thang, @Nam, @MaDonVi, @MaCachTl, @SoNgay";
                if (maCachTl.Equals(CachTinhLuong.CACH5))
                {
                    sql = "EXECUTE dbo.sp_tl_bangluong_truylinh_dulieu_insert @Thang, @Nam, @MaDonVi, @MaCachTl, @SoNgay";
                }
                var parameters = new[]
                {
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@MaCachTl", maCachTl),
                    new SqlParameter("@SoNgay", soNgay)
                };
                return ctx.FromSqlRaw<TlBangLuongThangQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlBangLuongThangQuery> GetDataInsertBhxh(int thang, int nam, string maDonVi, string maCachTl, int soNgay)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_qlThunop_bhxh_dulieu_insert @Thang, @Nam, @MaDonVi, @MaCachTl, @SoNgay";
                if (maCachTl.Equals(CachTinhLuong.CACH5))
                {
                    sql = "EXECUTE dbo.sp_tl_qlThuNop_bhxh_truylinh_dulieu_insert @Thang, @Nam, @MaDonVi, @MaCachTl, @SoNgay";
                }
                var parameters = new[]
                {
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@MaCachTl", maCachTl),
                    new SqlParameter("@SoNgay", soNgay)
                };
                return ctx.FromSqlRaw<TlBangLuongThangQuery>(sql, parameters).ToList();
            }
        }

        public DataTable ReportDanhSachCapPhatPhuCap(string maDonVi, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_danhsach_capphat_phucap";
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam)
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public DataTable GetDataReportDanhSachChiTraNganHang(string maDonVi, int nam, int thang)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_danhsach_chitra_nganhang";
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam)
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public DataTable GetDataReportThueTncnNam(string maDonVi, int nam, bool isOrderChucVu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_quyettoan_nam_thue_tncn";
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@IsOrderChucVu", isOrderChucVu),
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public DataTable GetDataReportGiaiThichChiTietHsqCs(string maDonVi, int nam, int thang, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_giaithich_chitiet_phucap_hsq_cs";
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@donViTinh", donViTinh)
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public DataTable ReportGiaiThichLuongChiTiet(string maDonVi, int thang, int nam, string maCachTl, string maPhuCap, string maPhuCapCount, int donViTinh, bool isSummary)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac";
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@MaCachTl", maCachTl),
                    new SqlParameter("@MaPhuCap", maPhuCap),
                    new SqlParameter("@MaPhuCapCount", maPhuCapCount),
                    new SqlParameter("@DonViTinh", donViTinh),
                    new SqlParameter("@IsSummary", isSummary),
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public DataTable ReportTienAn(int thang, int nam, string maDonVi, int daysInMonth)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "sp_tl_rpt_tien_an";
                var parameters = new[]
                {
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@daysInMonth", daysInMonth)
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public DataTable GetDataBangLuongPhuCapTongHopBienPhong(string maDonVi, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "sp_tl_bang_tonghop_luong_phucap_bienphong";
                var parameters = new[]
                {
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@maDonVi", maDonVi)
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public DataTable ReportGiaiThichChiTietPhuCapTNVKTHD(string maDonVi, int nam, int thang, string maCachTl, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_giaithich_chitiet_phucap_vuotkhung_handinh";
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@MaCachTl", maCachTl),
                    new SqlParameter("@DonViTinh", donViTinh),
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public DataTable ReportGiaiThichChiTietPhuCapKhac(string maDonVi, string maPhuCap, int nam, int thang, string maCachTl, int donViTinh, bool isOrderChucVu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_giaithich_chitiet_phucap_khac";
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@MaCachTl", maCachTl),
                    new SqlParameter("@MaPhuCap", maPhuCap),
                    new SqlParameter("@DonViTinh", donViTinh),
                    new SqlParameter("@IsOrderChucVu", isOrderChucVu),
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public DataTable ReportGiaiThichChiTietPhuCapTruyLinhKhac(string maDonVi, string maPhuCap, int nam, int thang, string maCachTl, int donViTinh, bool isOrderChucVu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_giaithich_chitiet_phucap_truylinh_khac";
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@MaCachTl", maCachTl),
                    new SqlParameter("@MaPhuCap", maPhuCap),
                    new SqlParameter("@DonViTinh", donViTinh),
                    new SqlParameter("@IsOrderChucVu", isOrderChucVu),
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public DataTable ReportGiaiThichPhuCapTheoNgay(string maDonVi, string maPhuCap, int nam, int thang, string maCachTl, int donViTinh, bool isOrderChucVu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_giaithich_phucap_theongay";
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@MaCachTl", maCachTl),
                    new SqlParameter("@MaPhuCap", maPhuCap),
                    new SqlParameter("@DonViTinh", donViTinh),
                    new SqlParameter("@IsOrderChucVu", isOrderChucVu),
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public DataTable ReportGiaiThichSoNgayPhuCapTheoNgay(string maDonVi, string maPhuCap, int nam, int thang, string maCachTl, int donViTinh, bool isOrderChucVu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_giaithich_songay_phucap_theongay";
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@MaCachTl", maCachTl),
                    new SqlParameter("@MaPhuCap", maPhuCap),
                    new SqlParameter("@DonViTinh", donViTinh),
                    new SqlParameter("@IsOrderChucVu", isOrderChucVu),
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public DataTable ReportChiTraNganHangThuNhapKhac(string maDonVi, int thang, int nam, bool isOrderChucVu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_danhsach_chitra_nganhang_thunhapkhac";
                var parameter = new[]
                {
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@IsOrderChucVu", isOrderChucVu)
                };
                return ctx.FromSqlCommand(sql, parameter);
            }
        }

        public DataTable ReportBangLuongTruyLinhDongPhuCap(string maDonVi, string maPhuCap, int nam, int thang, string maCachTl, string condition, int donViTinh, bool isOrderChucVu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_bangluong_truylinh_dong_phucap";
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@MaCachTl", maCachTl),
                    new SqlParameter("@MaPhuCap", maPhuCap),
                    new SqlParameter("@Condition", condition),
                    new SqlParameter("@DonViTinh", donViTinh),
                    new SqlParameter("@IsOrderChucVu", isOrderChucVu)
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public DataTable ReportGiaiThichRaQuanXuatNgu(string maDonVi, int nam, int thang, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_giaithich_chitiet_raquan";
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@donViTinh", donViTinh),
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public DataTable ReportGiaiThichBienPhong(string maDonVi, int nam, int thang, int donViTinh, string maPhuCap)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_giaithich_phucap_bienphong";
                var parameters = new[]
                {
                    new SqlParameter("@maPhuCap", maPhuCap),
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam)
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public DataTable ReportGiaiThichBienPhongTheoHeSo(string maDonVi, int nam, int thang, int donViTinh, string maPhuCap, string maPhuCapTien)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_giaithich_phucap_bienphong_heso";
                var parameters = new[]
                {
                    new SqlParameter("@maPhuCap", maPhuCap),
                    new SqlParameter("@maPhuCapTien", maPhuCapTien),
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam)
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public TlBangLuongThangTruyThu GetMonthlySalary(string maCanBo, string maPhuCap, int? thang, int? nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlBangLuongThangTruyThus.FirstOrDefault(x => x.MaHieuCanBo == maCanBo && x.MaPhuCap == maPhuCap && x.Nam == nam && x.Thang == thang);
            }
        }

        public TlBangLuongThangTruyThu GetLatestSalary(string maCanBo, int? thang, int? nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_get_thang_luong_gan_nhat @MaHieuCanBo, @Thang, @Nam";

                var parameters = new[]
                {
                    new SqlParameter("@MaHieuCanBo", maCanBo),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam)
                };
                return ctx.FromSqlRaw<TlBangLuongThangTruyThu>(sql, parameters).FirstOrDefault();
            }
        }

        public DataTable GetDataBangLuongThangTruBHXH(string maDonVi, int thang, int nam, bool isOrderChucVu, bool isGiaTriAm, bool isCheckedMaHuongLuong, bool isInCanBoMoi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh";
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@IsOrderChucVu", isOrderChucVu),
                    new SqlParameter("@IsGiaTriAm", isGiaTriAm),
                    new SqlParameter("@IsCheckedMaHuongLuong", isCheckedMaHuongLuong),
                    new SqlParameter("@IsNew", isInCanBoMoi)
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public DataTable GetDataLuongThangTruyThu(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_bang_luong_thang_truy_thu_chitiet";
                var parameters = new[]
                {
                    new SqlParameter("@Id", id)
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }
    }
}
