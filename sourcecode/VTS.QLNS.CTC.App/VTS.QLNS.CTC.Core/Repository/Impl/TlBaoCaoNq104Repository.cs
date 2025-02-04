using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlBaoCaoNq104Repository : Repository<TlBaoCao>, ITlBaoCaoNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlBaoCaoNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<ExportLuongCapBacGiaiThichChiTietLuongNq104Query> FindLuongChiTietCapBac(int thang, int nam, string maDonVi, string maCachTL)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.rpt_luong_capbac_giaithichbangluong_nq104 @Thang, @Nam, @CaTl, @maDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@CaTl", maCachTL),

                };
                return ctx.FromSqlRaw<ExportLuongCapBacGiaiThichChiTietLuongNq104Query>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ExportLuongChiTietNgachCanBoNq104Query> FindLuongChiTietNgach(int thang, int nam, string maDonVi, string maCachTL)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.prt_luong_chi_tiet_ngachcanbo_nq104 @Thang, @Nam, @CaTl, @maDonVi";
                var parameters = new[]
                                {
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@CaTl", maCachTL)
                };
                var a = ctx.FromSqlRaw<ExportLuongChiTietNgachCanBoNq104Query>(sql, parameters).ToList();
                return a;
            }
        }

        public IEnumerable<ExportLuongCapBacGiaiThichChiTietLuongNq104Query> FindLuongChiTietNgachCapBac(int thang, int nam, string maDonVi, string maCachTL)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.rpt_luong_giaithichluong_ngachcapbac_nq104 @Thang, @Nam, @CaTl, @maDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@CaTl", maCachTL)
                };
                return ctx.FromSqlRaw<ExportLuongCapBacGiaiThichChiTietLuongNq104Query>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ExportLuongNgachCanBoNq104Query> FindLuongNgachCanBo(int thang, int nam, string maDonVi, string maCachTL)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.rpt_luong_ngachcanbo_nq104 @Thang, @Nam, @CaTl, @maDonVi";
                var parameters = new[]
                                {
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@CaTl", maCachTL)
                };
                return ctx.FromSqlRaw<ExportLuongNgachCanBoNq104Query>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlCanBoPhuCapInKiemNq104Query> FindCanBoPhuCapInKiem(string thangNam, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_cb_phucap_inkiem_nq104 @thangNam, @maDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@thangNam", thangNam),
                    new SqlParameter("@maDonVi", maDonVi)
                };
                return ctx.FromSqlRaw<TlCanBoPhuCapInKiemNq104Query>(sql, parameters).ToList();
            }
        }

        public IEnumerable<BaoCaoPhanTichTienAnNq104Query> ReportBaoCaoTienAn(string MaCanBo, string MaPhuCap)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_baocao_phantich_tienan_nq104 @MaCanBo, @MaPhuCap";
                var parameters = new[]
                {
                    new SqlParameter("@MaCanBo", MaCanBo),
                    new SqlParameter("@MaPhuCap", MaPhuCap)
                };
                return ctx.FromSqlRaw<BaoCaoPhanTichTienAnNq104Query>(sql, parameters).ToList();
            }
        }
    }
}
