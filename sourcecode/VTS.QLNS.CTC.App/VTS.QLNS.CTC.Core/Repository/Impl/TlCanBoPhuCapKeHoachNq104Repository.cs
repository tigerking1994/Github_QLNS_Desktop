using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlCanBoPhuCapKeHoachNq104Repository : Repository<TlCanBoPhuCapKeHoachNq104>, ITlCanBoPhuCapKeHoachNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlCanBoPhuCapKeHoachNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int DeleteByMaCanBo(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.Database.ExecuteSqlCommand($"DELETE FROM Tl_CanBo_PhuCap_KeHoach_NQ104 WHERE Ma_CanBo = {maCanBo}");
            }
        }

        public int DeleteManyMaCanBo(string lstMaCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_delete_can_bo_phu_cap_ke_hoach_nq104 @lstMaCanBo";
                var parameters = new[]
                {
                    new SqlParameter("@lstMaCanBo", lstMaCanBo)
                };
                return ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public int DeleteByYear(int year)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.Database.ExecuteSqlCommand("DELETE FROM Tl_CanBo_PhuCap_KeHoach_NQ104 WHERE Ma_CanBo like {0}", year + "%");
            }
        }

        public IEnumerable<TlCanBoPhuCapKeHoachNq104> FindByCondition(Expression<Func<TlCanBoPhuCapKeHoachNq104, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlCanBoPhuCapKeHoachNq104s.Where(predicate).ToList();
            }
        }

        public IEnumerable<TlCanBoPhuCapKeHoachNq104> FindByMaCanBo(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlCanBoPhuCapKeHoachNq104s.Where(x => x.MaCanBo == maCanBo).ToList();
            }
        }

        public IEnumerable<TlPhuCapNq104Query> FindCanBoPhuCap(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_get_data_phucap_kehoach_nq104 @maCanBo";
                var parameters = new[]
                {
                    new SqlParameter("@maCanBo", maCanBo)
                };
                return ctx.FromSqlRaw<TlPhuCapNq104Query>(sql, parameters).ToList();
            }
        }
    }
}
