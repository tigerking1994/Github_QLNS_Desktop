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
using System.Linq.Expressions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlBangLuongThangNq104Repository : Repository<TlBangLuongThangNq104>, ITlBangLuongThangNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlBangLuongThangNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int DeleteByParentId(Guid parentId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.TlBangLuongThangsNq104.RemoveRange(ctx.TlBangLuongThangsNq104.Where(x => x.Parent == parentId));
                return ctx.SaveChanges();
            }
        }
        public IEnumerable<TlDmCanBoNq104> FindCb(decimal? thang, decimal? nam, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBoNq104s.Where(x => x.Thang == thang && x.Nam == nam && x.Parent == maDonVi && x.IsDelete == true && x.KhongLuong == false).ToList();
            }
        }

        public IEnumerable<TlDmThueThuNhapCaNhanNq104> FindThue(bool bIsThueThang)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmThueThuNhapCaNhanNq104s.Where(x => x.BIsThueThang == bIsThueThang && x.LoaiThue.StartsWith("B")).OrderBy(x => x.ThuNhapTu).ToList();
            }
        }

        public IEnumerable<TlBangLuongThangNq104> FindByParentId(Guid parentId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlBangLuongThangsNq104.Where(x => x.Parent == parentId).ToList();
            }
        }

        public DataTable ReportBangLuongThang(string maDonVi, int thang, int nam, bool isOrderChucVu, bool isGiaTriAm, bool isCheckedMaHuongLuong, bool isInCanBoMoi, decimal tyLeHuong)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_bangthanhtoan_luongthang_nq104";
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@IsOrderChucVu", isOrderChucVu),
                    new SqlParameter("@IsGiaTriAm", isGiaTriAm),
                    new SqlParameter("@IsCheckedMaHuongLuong", isCheckedMaHuongLuong),
                    new SqlParameter("@IsNew", isInCanBoMoi),
                    new SqlParameter("@TyLeHuong", tyLeHuong)
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
                string sql = "dbo.sp_tl_rpt_bangthanhtoan_luongthang_dong_nq104";
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

        public DataTable ReportBangLuongThangTheoDonVi(string maDonVi, int thang, int nam, bool isOrderChucVu, bool isGiaTriAm, bool isCheckedMaHuongLuong, bool isInCanBoMoi = false, decimal tyLeHuong = 0)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_bangthanhtoan_luongthang_2_nq104";
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@IsOrderChucVu", isOrderChucVu),
                    new SqlParameter("@IsGiaTriAm", isGiaTriAm),
                    new SqlParameter("@IsCheckedMaHuongLuong", isCheckedMaHuongLuong),
                    new SqlParameter("@TyLeHuong", tyLeHuong)
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public DataTable ReportBangLuongThangTheoDonViTruBHXH(string maDonVi, int thang, int nam, bool isOrderChucVu, bool isGiaTriAm, bool isCheckedMaHuongLuong)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2_nq104";
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
                string sql = "sp_tl_rpt_bangluong_truylinh_nq104";
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
                string sql = "dbo.sp_tl_bangluong_thang_chitiet_nq104";
                var parameters = new[]
                {
                    new SqlParameter("@Id", id)
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public IEnumerable<TlBangLuongThangNq104> FindMaCanBo(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlBangLuongThangsNq104.Where(x => x.MaCbo == maCanBo).ToList();
            }
        }

        public DataTable RptDSChiTraCaNhanNganHang(int nam, int thang, List<TlDmDonViNq104> tlDmDonVis)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                DataTable dataTable = new DataTable();

                var connection = ctx.Database.GetDbConnection();
                DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(connection);
                var cmd = connection.CreateCommand();
                cmd.CommandText = "rpt_ds_chi_tra_ca_nhan_ngan_hang_nq104";
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
                string sql = "sp_tl_rpt_bangke_trichthue_tncn_nq104";
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
                cmd.CommandText = "sp_tl_rpt_bangluongthang_phucapkhac_nq104";
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
                cmd.CommandText = "sp_tl_rpt_bangluongthang_phaitru_nq104";
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

        public IEnumerable<TlRptDienBienLuongNq104Query> GetDataBangLuong()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_lay_du_lieu_bang_luong_nq104";
                return ctx.FromSqlRaw<TlRptDienBienLuongNq104Query>(sql).ToList();
            }
        }

        public IEnumerable<TlRptTruyLinhChuyenCheDoNq104Query> ReportTruyLinhChuyenCheDo(string maDonVi, int thangTruoc, int namTruoc, int thangSau, int namSau, string maHieuCanBo, bool isOrderChucVu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_rpt_truylinh_chuyenchedo_nq104 @maDonVi, @thangTruoc, @namTruoc, @thangSau, @namSau, @maHieuCanBo, @IsOrderChucVu";
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
                return ctx.FromSqlRaw<TlRptTruyLinhChuyenCheDoNq104Query>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlBangLuongThangDongNq104Query> ReportBangLuongThangDong(string maDonVi, string ngach, string maPhuCap, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_bang_luong_thang_dong_nq104 @maDonVi, @ngach, @maPhuCap, @thang, @nam";
                var parameters = new[]
                {
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@ngach", ngach),
                    new SqlParameter("@maPhuCap", maPhuCap),
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam)
                };
                return ctx.FromSqlRaw<TlBangLuongThangDongNq104Query>(sql, parameters).ToList();
            }
        }

        public DataTable ReportBangLuongThangDoc(string maDonVi, string ngach, string maPhuCap, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "sp_tl_bang_luong_thang_doc_nq104";
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
                cmd.CommandText = "sp_tl_rpt_dienbien_luong_nq104";
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
                string sql = "sp_tl_rpt_tonghop_luong_theo_ngach_nq104";
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
                string sql = "sp_tl_rpt_tonghop_luong_theo_don_vi_nq104";
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
                string sql = "sp_tl_rpt_tonghop_luong_theo_ngach_don_vi_nq104";
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

        public IEnumerable<TlBangLuongThangNq104Query> GetDataInsert(int thang, int nam, string maDonVi, string maCachTl, int soNgay)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_bangluong_thang_dulieu_insert_nq104 @Thang, @Nam, @MaDonVi, @MaCachTl, @SoNgay";
                if (maCachTl.Equals(CachTinhLuong.CACH5))
                {
                    sql = "EXECUTE dbo.sp_tl_bangluong_truylinh_dulieu_insert_nq104 @Thang, @Nam, @MaDonVi, @MaCachTl, @SoNgay";
                }
                var parameters = new[]
                {
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@MaCachTl", maCachTl),
                    new SqlParameter("@SoNgay", soNgay)
                };
                return ctx.FromSqlRaw<TlBangLuongThangNq104Query>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlBangLuongThangNq104Query> GetDataInsertBhxh(int thang, int nam, string maDonVi, string maCachTl, int soNgay)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_qlThunop_bhxh_dulieu_insert_nq104 @Thang, @Nam, @MaDonVi, @MaCachTl, @SoNgay";
                if (maCachTl.Equals(CachTinhLuong.CACH5))
                {
                    sql = "EXECUTE dbo.sp_tl_qlThuNop_bhxh_truylinh_dulieu_insert_nq104 @Thang, @Nam, @MaDonVi, @MaCachTl, @SoNgay";
                }
                var parameters = new[]
                {
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@MaCachTl", maCachTl),
                    new SqlParameter("@SoNgay", soNgay)
                };
                return ctx.FromSqlRaw<TlBangLuongThangNq104Query>(sql, parameters).ToList();
            }
        }

        public DataTable ReportDanhSachCapPhatPhuCap(string maDonVi, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_danhsach_capphat_phucap_nq104";
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
                string sql = "dbo.sp_tl_rpt_danhsach_chitra_nganhang_nq104";
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
                string sql = "dbo.sp_tl_rpt_quyettoan_nam_thue_tncn_nq104";
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
                string sql = "dbo.sp_tl_rpt_giaithich_chitiet_phucap_hsq_cs_nq104";
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
                string sql = "dbo.sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac_nq104";
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
                string sql = "sp_tl_rpt_tien_an_nq104";
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
                string sql = "sp_tl_bang_tonghop_luong_phucap_bienphong_nq104";
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
                string sql = "dbo.sp_tl_rpt_giaithich_chitiet_phucap_vuotkhung_handinh_nq104";
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
                string sql = "dbo.sp_tl_rpt_giaithich_chitiet_phucap_khac_nq104";
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
                string sql = "dbo.sp_tl_rpt_giaithich_chitiet_phucap_truylinh_khac_nq104";
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
                string sql = "dbo.sp_tl_rpt_giaithich_phucap_theongay_nq104";
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
                string sql = "dbo.sp_tl_rpt_giaithich_songay_phucap_theongay_nq104";
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
                string sql = "dbo.sp_tl_danhsach_chitra_nganhang_thunhapkhac_nq104";
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
                string sql = "dbo.sp_tl_rpt_bangluong_truylinh_dong_phucap_nq104";
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
                string sql = "dbo.sp_tl_rpt_giaithich_chitiet_raquan_nq104";
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
                string sql = "dbo.sp_tl_rpt_giaithich_phucap_bienphong_nq104";
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
                string sql = "dbo.sp_tl_rpt_giaithich_phucap_bienphong_heso_nq104";
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

        public TlBangLuongThangNq104 GetMonthlySalary(string maCanBo, string maPhuCap, int? thang, int? nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlBangLuongThangsNq104.FirstOrDefault(x => x.MaHieuCanBo == maCanBo && x.MaPhuCap == maPhuCap && x.Nam == nam && x.Thang == thang);
            }
        }

        public TlBangLuongThangNq104 GetLatestSalary(string maCanBo, int? thang, int? nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_get_thang_luong_gan_nhat_nq104 @MaHieuCanBo, @Thang, @Nam";

                var parameters = new[]
                {
                    new SqlParameter("@MaHieuCanBo", maCanBo),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam)
                };
                return ctx.FromSqlRaw<TlBangLuongThangNq104>(sql, parameters).FirstOrDefault();
            }
        }

        public DataTable GetDataBangLuongThangTruBHXH(string maDonVi, int thang, int nam, bool isOrderChucVu, bool isGiaTriAm, bool isCheckedMaHuongLuong, bool isInCanBoMoi, decimal tyLeHuong)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_nq104";
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@IsOrderChucVu", isOrderChucVu),
                    new SqlParameter("@IsGiaTriAm", isGiaTriAm),
                    new SqlParameter("@IsCheckedMaHuongLuong", isCheckedMaHuongLuong),
                    new SqlParameter("@IsNew", isInCanBoMoi),
                    new SqlParameter("@TyLeHuong", tyLeHuong)
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public IEnumerable<TlBangLuongThangNq104> FindBangLuongThangByCondition(string maDonVi, int? thang, int? nam, string maCachTL, string maHieuCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_get_bang_luongthang_bridge_nq104 @MaDonVi, @Thang, @Nam, @MaCachTL, @MaHieuCanBo ";
                var parameters = new[]
                {
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@MaCachTl", maCachTL),
                    new SqlParameter("@MaHieuCanBo", maHieuCanBo)
                };
                return ctx.FromSqlRaw<TlBangLuongThangNq104>(sql, parameters).ToList();
            }
        }

        public List<TlBangLuongThangNq104> FindLuongThangCanBo(int? thang, int? nam, string maDonVi, Guid id, string maCach)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_find_luongthang_canbo_nq104 @thang, @nam, @maCachTl, @maDonVi, @parent ";
                var parameters = new[]
                {
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@maCachTl", maCach),
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@parent", id),
                };
                return ctx.FromSqlRaw<TlBangLuongThangNq104>(sql, parameters).ToList();
            }
        }
    }
}
