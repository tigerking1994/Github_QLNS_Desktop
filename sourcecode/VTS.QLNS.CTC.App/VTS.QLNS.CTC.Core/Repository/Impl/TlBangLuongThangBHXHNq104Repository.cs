using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlBangLuongThangBHXHNq104Repository : Repository<TlBangLuongThangBHXHNq104>, ITlBangLuongThangBHXHNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlBangLuongThangBHXHNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int DeleteByParentId(Guid parentId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.TlBangLuongThangBHXHsNq104.RemoveRange(ctx.TlBangLuongThangBHXHsNq104.Where(x => x.Parent == parentId));
                return ctx.SaveChanges();
            }
        }

        public IEnumerable<TlBangLuongThangBHXHNq104> FindByMonthYear(int month, int year)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlBangLuongThangBHXHsNq104.Where(x => x.Nam == year && x.Thang == month).ToList();
            }
        }

        public IEnumerable<TlBangLuongThangBHXHNq104> FindByParentId(Guid parentId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlBangLuongThangBHXHsNq104.Where(x => x.Parent == parentId).ToList();
            }
        }

        public IEnumerable<TlBangLuongThangBHXHNq104Query> ExportBangThanhToanTroCapOmDau(string maDonVi, int year, int month, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_bhxh_luong_tro_cap_om_dau_nq104 @MaDonVi, @NamLamViec, @Thang, @DonViTinh";
                
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@NamLamViec", year),
                    new SqlParameter("@Thang", month),
                    new SqlParameter("@DonViTinh", dvt)
                };
                return ctx.FromSqlRaw<TlBangLuongThangBHXHNq104Query>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlBangLuongThangBHXHNq104ReportQuery> ExportBangThanhToanTroCapOmDauGiaiThich(string lstmaCanbo, int year, int month, int dvt, int typePrint)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_bhxh_giaithich_trocap_nq104 @lstmaCanbo, @Thang, @Nam,@DonViTinh, @TypeOutPut";

                var parameters = new[]
                {
                    new SqlParameter("@lstmaCanbo", lstmaCanbo),
                    new SqlParameter("@Nam", year),
                    new SqlParameter("@Thang", month),
                    new SqlParameter("@TypeOutPut", typePrint),
                    new SqlParameter("@DonViTinh", dvt)
                };
                return ctx.FromSqlRaw<TlBangLuongThangBHXHNq104ReportQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlBangLuongThangBHXHNq104Query> ExportBangThanhToanTroCapThaiSan(string maDonVi, int year, int month, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_bhxh_luong_tro_cap_thai_san_nq104 @DsMaDonVi, @NamLamViec, @Thang, @DonViTinh";

                var parameters = new[]
                {
                    new SqlParameter("@DsMaDonVi", maDonVi),
                    new SqlParameter("@NamLamViec", year),
                    new SqlParameter("@Thang", month),
                    new SqlParameter("@DonViTinh", dvt)
                };
                return ctx.FromSqlRaw<TlBangLuongThangBHXHNq104Query>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlBangLuongThangBHXHNq104Query> ExportBangThanhToanTroCapTNLD(string maDonVi, int year, int month, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_bhxh_luong_tro_cap_tnld_nq104 @MaDonVi, @NamLamViec, @Thang, @DonViTinh";

                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@NamLamViec", year),
                    new SqlParameter("@Thang", month),
                    new SqlParameter("@DonViTinh", dvt)
                };
                return ctx.FromSqlRaw<TlBangLuongThangBHXHNq104Query>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlBangLuongThangBHXHNq104Query> ExportBangThanhToanTroCapHuuTriPhucVienThoiViecTuTuat(string maDonVi, int year, int month, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt_nq104 @MaDonVi, @NamLamViec, @Thang, @DonViTinh";

                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@NamLamViec", year),
                    new SqlParameter("@Thang", month),
                    new SqlParameter("@DonViTinh", dvt)
                };
                return ctx.FromSqlRaw<TlBangLuongThangBHXHNq104Query>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlBangLuongThangBHXHNq104Query> ExportBangThanhToanTroCapXuatNgu(string maDonVi, int year, int month, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_bhxh_luong_tro_cap_xuat_ngu_nq104 @MaDonVi, @NamLamViec, @Thang, @DonViTinh";

                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@NamLamViec", year),
                    new SqlParameter("@Thang", month),
                    new SqlParameter("@DonViTinh", dvt)
                };
                return ctx.FromSqlRaw<TlBangLuongThangBHXHNq104Query>(sql, parameters).ToList();
            }
        }

        public DataTable GetDataLuongThangBHXH(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_bang_luong_thang_bhxh_chitiet_nq104";
                var parameters = new[]
                {
                    new SqlParameter("@Id", id)
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public TlBangLuongThangBHXHNq104 GetLatestSalaryBHXH(string maCanBo, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlBangLuongThangBHXHsNq104.OrderByDescending(x => x.Nam).ThenByDescending(x => x.Thang)
                    .FirstOrDefault(x => x.MaHieuCanBo == maCanBo && x.Thang < thang && x.Nam <= nam);
            }
        }

        public IEnumerable<TlBangLuongThangBHXHNq104Query> ExportBangLuongBHXH(int year, string months)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_export_bang_luong_thang_bhxh_nq104 @YearOfWork, @Months";

                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", year),
                    new SqlParameter("@Months", months)
                };
                return ctx.FromSqlRaw<TlBangLuongThangBHXHNq104Query>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlBangLuongThangBHXHNq104Query> ExportDataQTCBHXH(int year, string months)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_luong_bhxh_import_qtc_bhxh_nq104 @YearOfWork, @Months";

                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", year),
                    new SqlParameter("@Months", months)
                };
                return ctx.FromSqlRaw<TlBangLuongThangBHXHNq104Query>(sql, parameters).ToList();
            }
        }

        public TlDmPhuCapNq104 GetCongChuan(string maCongChuan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmPhuCapsNq104.FirstOrDefault(x => x.MaPhuCap == maCongChuan);
            }
        }
    }
}
