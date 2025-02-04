using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlCanBoPhuCapNq104Repository : Repository<TlCanBoPhuCapNq104>, ITlCanBoPhuCapNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlCanBoPhuCapNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int DeleteByMaCanBo(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.Database.ExecuteSqlCommand($"DELETE FROM Tl_CanBo_PhuCap_nq104 WHERE MA_CBO = {maCanBo}");
            }
        }

        public IEnumerable<TlCanBoPhuCapNq104> FindByMaCbo(string maCanbo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlCanBoPhuCapsNq104.Where(x => x.MaCbo == maCanbo).ToList();
            }
        }

        public IEnumerable<TlCanBoPhuCapNq104> FindByLstMaCanBo(List<string> maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlCanBoPhuCapsNq104.Where(x => maCanBo.Contains(x.MaCbo)).ToList();
            }
        }

        public IEnumerable<TlPhuCapNq104Query> FindCanBoPhuCap(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_get_data_phucap_nq104 @maCanBo";
                var parameters = new[]
                {
                    new SqlParameter("@maCanBo", maCanBo)
                };
                return ctx.FromSqlRaw<TlPhuCapNq104Query>(sql, parameters).ToList();
            }
        }

        public int DeleteCanBo(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_tl_delete_can_bo_nq104 @maCanBo";
                var parameters = new[]
                {
                    new SqlParameter("@maCanBo", maCanBo)
                };
                return ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public TlCanBoPhuCapNq104 FindByMaCanBoAndMaPhuCap(string maCanBo, string maPhuCap)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlCanBoPhuCapsNq104.FirstOrDefault(x => x.MaCbo.Equals(maCanBo) && x.MaPhuCap.Equals(maPhuCap));
            }
        }

        public IEnumerable<TLCanBoPhuCapNq104Query> Copy(string maCanBo, int fromYear, int fromMonth, int toYear, int toMonth, bool isCopyValue)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_tl_canbo_phucap_saochep_nq104 @MaCanBo, @FromYear, @FromMonth, @ToYear, @ToMonth, @IsCopyValue";
                var parameters = new[]
                {
                    new SqlParameter("@MaCanBo", maCanBo),
                    new SqlParameter("@FromYear", fromYear),
                    new SqlParameter("@FromMonth", fromMonth),
                    new SqlParameter("@ToYear", toYear),
                    new SqlParameter("@ToMonth", toMonth),
                    new SqlParameter("@IsCopyValue", isCopyValue)
                };
                return ctx.FromSqlRaw<TLCanBoPhuCapNq104Query>(sql, parameters);
            }
        }

        public IEnumerable<TlCanBoPhuCapNq104> FindDataCanBoPhuCap(string maCanbo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_tl_find_canbo_phucap_nq104 @lstMaCBo";
                var parameters = new[]
                {
                    new SqlParameter("@lstMaCBo", maCanbo),

                };
                return ctx.FromSqlRaw<TlCanBoPhuCapNq104>(sql, parameters).ToList();
            }
        }
    }
}
