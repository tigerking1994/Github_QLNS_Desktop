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
    public class TlCanBoPhuCapRepository : Repository<TlCanBoPhuCap>, ITlCanBoPhuCapRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlCanBoPhuCapRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int DeleteByMaCanBo(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.Database.ExecuteSqlCommand($"DELETE FROM Tl_CanBo_PhuCap WHERE MA_CBO = {maCanBo}");
            }
        }

        public IEnumerable<TlCanBoPhuCap> FindByMaCbo(string maCanbo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlCanBoPhuCaps.Where(x => x.MaCbo == maCanbo).ToList();
            }
        }

        public IEnumerable<TlCanBoPhuCap> FindByLstMaCanBo(List<string> maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlCanBoPhuCaps.Where(x => maCanBo.Contains(x.MaCbo)).ToList();
            }
        }

        public IEnumerable<TlPhuCapQuery> FindCanBoPhuCap(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_get_data_phucap @maCanBo";
                var parameters = new[]
                {
                    new SqlParameter("@maCanBo", maCanBo)
                };
                return ctx.FromSqlRaw<TlPhuCapQuery>(sql, parameters).ToList();
            }
        }

        public int DeleteCanBo(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_tl_delete_can_bo @maCanBo";
                var parameters = new[]
                {
                    new SqlParameter("@maCanBo", maCanBo)
                };
                return ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public TlCanBoPhuCap FindByMaCanBoAndMaPhuCap(string maCanBo, string maPhuCap)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlCanBoPhuCaps.FirstOrDefault(x => x.MaCbo.Equals(maCanBo) && x.MaPhuCap.Equals(maPhuCap));
            }
        }

        public IEnumerable<TLCanBoPhuCapQuery> Copy(string maCanBo, int fromYear, int fromMonth, int toYear, int toMonth, bool isCopyValue)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_tl_canbo_phucap_saochep @MaCanBo, @FromYear, @FromMonth, @ToYear, @ToMonth, @IsCopyValue";
                var parameters = new[]
                {
                    new SqlParameter("@MaCanBo", maCanBo),
                    new SqlParameter("@FromYear", fromYear),
                    new SqlParameter("@FromMonth", fromMonth),
                    new SqlParameter("@ToYear", toYear),
                    new SqlParameter("@ToMonth", toMonth),
                    new SqlParameter("@IsCopyValue", isCopyValue)
                };
                return ctx.FromSqlRaw<TLCanBoPhuCapQuery>(sql, parameters);
            }
        }

        public IEnumerable<TLCanBoPhuCapQuery> FindCanBoPhuCapByMaCanBo(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_get_can_bo_phu_cap_bang_luong @maCanBo";
                var parameters = new[]
                {
                    new SqlParameter("@MaCanBo", maCanBo)
                };
                return ctx.FromSqlRaw<TLCanBoPhuCapQuery>(sql, parameters).ToList();
            }
        }
    }
}
