using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDieuChinhQsKeHoachNq104Repository : Repository<TlDieuChinhQsKeHoachNq104>, ITlDieuChinhQsKeHoachNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public TlDieuChinhQsKeHoachNq104Repository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int DeleteByNam(int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.Database.ExecuteSqlCommand($"DELETE FROM TL_DieuChinh_QS_KeHoach_NQ104 WHERE Nam = {nam}");
            }
        }

        public IEnumerable<TlRptQuanSoKeHoachNq104Query> FindData(int nam, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_tl_rpt_dieuchinh_quanso_kehoach_nq104 @Nam, @maDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@maDonVi", maDonVi)
                };
                return ctx.FromSqlRaw<TlRptQuanSoKeHoachNq104Query>(sql, parameters).ToList();
            }
        }
    }
}
