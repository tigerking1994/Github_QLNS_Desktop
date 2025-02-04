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
    public class TlDieucChinhQsKeHoachRepository : Repository<TlDieuChinhQsKeHoach>, ITlDieuChinhQsKeHoachRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public TlDieucChinhQsKeHoachRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int DeleteByNam(int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.Database.ExecuteSqlCommand($"DELETE FROM TL_DieuChinh_QS_KeHoach WHERE Nam = {nam}");
            }
        }

        public IEnumerable<TlRptQuanSoKeHoachQuery> FindData(int nam, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_tl_rpt_dieuchinh_quanso_kehoach @Nam, @maDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@maDonVi", maDonVi)
                };
                return ctx.FromSqlRaw<TlRptQuanSoKeHoachQuery>(sql, parameters).ToList();
            }
        }
    }
}
