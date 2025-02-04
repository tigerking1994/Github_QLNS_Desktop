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
    public class TlBangLuongThangBHXHRepository : Repository<TlBangLuongThangBHXH>, ITlBangLuongThangBHXHRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlBangLuongThangBHXHRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int DeleteByParentId(Guid parentId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.TlBangLuongThangBHXHs.RemoveRange(ctx.TlBangLuongThangBHXHs.Where(x => x.Parent == parentId));
                return ctx.SaveChanges();
            }
        }

        public IEnumerable<TlBangLuongThangBHXH> FindByMonthYear(int month, int year)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlBangLuongThangBHXHs.Where(x => x.Nam == year && x.Thang == month).ToList();
            }
        }

        public IEnumerable<TlBangLuongThangBHXH> FindByParentId(Guid parentId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlBangLuongThangBHXHs.Where(x => x.Parent == parentId).ToList();
            }
        }

        public IEnumerable<TlBangLuongThangBHXHQuery> ExportBangThanhToanTroCapOmDau(string maDonVi, int year, int month, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_bhxh_luong_tro_cap_om_dau @MaDonVi, @NamLamViec, @Thang, @DonViTinh";
                
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@NamLamViec", year),
                    new SqlParameter("@Thang", month),
                    new SqlParameter("@DonViTinh", dvt)
                };
                return ctx.FromSqlRaw<TlBangLuongThangBHXHQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlBangLuongThangBHXHReportQuery> ExportBangThanhToanTroCapOmDauGiaiThich(string lstmaCanbo, int year, int month, int dvt, int typePrint, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_bhxh_giaithich_trocap @lstmaCanbo, @Thang, @Nam,@DonViTinh, @TypeOutPut, @MaDonVi";

                var parameters = new[]
                {
                    new SqlParameter("@lstmaCanbo", lstmaCanbo),
                    new SqlParameter("@Nam", year),
                    new SqlParameter("@Thang", month),
                    new SqlParameter("@TypeOutPut", typePrint),
                    new SqlParameter("@DonViTinh", dvt),
                    new SqlParameter("@MaDonVi", maDonVi)
                };
                return ctx.FromSqlRaw<TlBangLuongThangBHXHReportQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlBangLuongThangBHXHQuery> ExportBangThanhToanTroCapThaiSan(string maDonVi, int year, int month, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_bhxh_luong_tro_cap_thai_san @DsMaDonVi, @NamLamViec, @Thang, @DonViTinh";

                var parameters = new[]
                {
                    new SqlParameter("@DsMaDonVi", maDonVi),
                    new SqlParameter("@NamLamViec", year),
                    new SqlParameter("@Thang", month),
                    new SqlParameter("@DonViTinh", dvt)
                };
                return ctx.FromSqlRaw<TlBangLuongThangBHXHQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlBangLuongThangBHXHQuery> ExportBangThanhToanTroCapTNLD(string maDonVi, int year, int month, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_bhxh_luong_tro_cap_tnld @MaDonVi, @NamLamViec, @Thang, @DonViTinh";

                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@NamLamViec", year),
                    new SqlParameter("@Thang", month),
                    new SqlParameter("@DonViTinh", dvt)
                };
                return ctx.FromSqlRaw<TlBangLuongThangBHXHQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlBangLuongThangBHXHQuery> ExportBangThanhToanTroCapHuuTriPhucVienThoiViecTuTuat(string maDonVi, int year, int month, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt @MaDonVi, @NamLamViec, @Thang, @DonViTinh";

                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@NamLamViec", year),
                    new SqlParameter("@Thang", month),
                    new SqlParameter("@DonViTinh", dvt)
                };
                return ctx.FromSqlRaw<TlBangLuongThangBHXHQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlBangLuongThangBHXHQuery> ExportBangThanhToanTroCapXuatNgu(string maDonVi, int year, int month, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_bhxh_luong_tro_cap_xuat_ngu @MaDonVi, @NamLamViec, @Thang, @DonViTinh";

                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@NamLamViec", year),
                    new SqlParameter("@Thang", month),
                    new SqlParameter("@DonViTinh", dvt)
                };
                return ctx.FromSqlRaw<TlBangLuongThangBHXHQuery>(sql, parameters).ToList();
            }
        }

        public DataTable GetDataLuongThangBHXH(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_bang_luong_thang_bhxh_chitiet";
                var parameters = new[]
                {
                    new SqlParameter("@Id", id)
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public TlBangLuongThangBHXH GetLatestSalaryBHXH(string maCanBo, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlBangLuongThangBHXHs.OrderByDescending(x => x.Nam).ThenByDescending(x => x.Thang)
                    .FirstOrDefault(x => x.MaHieuCanBo == maCanBo && x.Thang < thang && x.Nam <= nam);
            }
        }

        public IEnumerable<TlBangLuongThangBHXHQuery> ExportBangLuongBHXH(int year, string months)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_export_bang_luong_thang_bhxh @YearOfWork, @Months";

                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", year),
                    new SqlParameter("@Months", months)
                };
                return ctx.FromSqlRaw<TlBangLuongThangBHXHQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlBangLuongThangBHXHQuery> ExportDataQTCBHXH(int year, string months)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_luong_bhxh_import_qtc_bhxh @YearOfWork, @Months";

                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", year),
                    new SqlParameter("@Months", months)
                };
                return ctx.FromSqlRaw<TlBangLuongThangBHXHQuery>(sql, parameters).ToList();
            }
        }

        public TlDmPhuCap GetCongChuan(string maCongChuan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmPhuCaps.FirstOrDefault(x => x.MaPhuCap == maCongChuan);
            }
        }

        public IEnumerable<TlBangLuongThangBHXHQuery> GetBangLuongTheoPhanHo(int year, int month)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_bhxh_get_bang_luong_phan_ho @Nam, @Thang";

                var parameters = new[]
                {
                    new SqlParameter("@Nam", year),
                    new SqlParameter("@Thang", month)
                };
                return ctx.FromSqlRaw<TlBangLuongThangBHXHQuery>(sql, parameters).ToList();
            }
        }
    }
}
