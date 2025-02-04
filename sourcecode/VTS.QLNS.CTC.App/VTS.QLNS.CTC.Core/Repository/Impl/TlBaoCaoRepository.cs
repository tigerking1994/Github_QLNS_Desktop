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
    public class TlBaoCaoRepository : Repository<TlBaoCao>, ITlBaoCaoRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlBaoCaoRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<ExportLuongCapBacGiaiThichChiTietLuongQuery> FindLuongChiTietCapBac(int thang, int nam, string maDonVi, string maCachTL)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.rpt_luong_capbac_giaithichbangluong @Thang, @Nam, @CaTl, @maDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@CaTl", maCachTL),

                };
                return ctx.FromSqlRaw<ExportLuongCapBacGiaiThichChiTietLuongQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ExportLuongChiTietNgachCanBoQuery> FindLuongChiTietNgach(int thang, int nam, string maDonVi, string maCachTL)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.prt_luong_chi_tiet_ngachcanbo @Thang, @Nam, @CaTl, @maDonVi";
                var parameters = new[]
                                {
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@CaTl", maCachTL)
                };
                var a = ctx.FromSqlRaw<ExportLuongChiTietNgachCanBoQuery>(sql, parameters).ToList();
                return a;
            }
        }

        public IEnumerable<ExportLuongCapBacGiaiThichChiTietLuongQuery> FindLuongChiTietNgachCapBac(int thang, int nam, string maDonVi, string maCachTL)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.rpt_luong_giaithichluong_ngachcapbac @Thang, @Nam, @CaTl, @maDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@CaTl", maCachTL)
                };
                return ctx.FromSqlRaw<ExportLuongCapBacGiaiThichChiTietLuongQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ExportLuongNgachCanBoQuery> FindLuongNgachCanBo(int thang, int nam, string maDonVi, string maCachTL)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.rpt_luong_ngachcanbo @Thang, @Nam, @CaTl, @maDonVi";
                var parameters = new[]
                                {
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@CaTl", maCachTL)
                };
                return ctx.FromSqlRaw<ExportLuongNgachCanBoQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlCanBoPhuCapInKiemQuery> FindCanBoPhuCapInKiem(string thangNam, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_cb_phucap_inkiem @thangNam, @maDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@thangNam", thangNam),
                    new SqlParameter("@maDonVi", maDonVi)
                };
                return ctx.FromSqlRaw<TlCanBoPhuCapInKiemQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<BaoCaoPhanTichTienAnQuery> ReportBaoCaoTienAn(string MaCanBo, string MaPhuCap)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_baocao_phantich_tienan @MaCanBo, @MaPhuCap";
                var parameters = new[]
                {
                    new SqlParameter("@MaCanBo", MaCanBo),
                    new SqlParameter("@MaPhuCap", MaPhuCap)
                };
                return ctx.FromSqlRaw<BaoCaoPhanTichTienAnQuery>(sql, parameters).ToList();
            }
        }
    }
}
