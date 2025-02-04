using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using System.Linq.Expressions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlCanBoPhuCapKeHoachRepository : Repository<TlCanBoPhuCapKeHoach>, ITlCanBoPhuCapKeHoachRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlCanBoPhuCapKeHoachRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int DeleteByMaCanBo(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.Database.ExecuteSqlCommand($"DELETE FROM Tl_CanBo_PhuCap_KeHoach WHERE Ma_CanBo = {maCanBo}");
            }
        }

        public int DeleteManyMaCanBo(string lstMaCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_delete_can_bo_phu_cap_ke_hoach @lstMaCanBo";
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
                return ctx.Database.ExecuteSqlCommand("DELETE FROM Tl_CanBo_PhuCap_KeHoach WHERE Ma_CanBo like {0}", year + "%");
            }
        }

        public IEnumerable<TlCanBoPhuCapKeHoach> FindByCondition(Expression<Func<TlCanBoPhuCapKeHoach, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlCanBoPhuCapKeHoaches.Where(predicate).ToList();
            }
        }

        public IEnumerable<TlCanBoPhuCapKeHoach> FindByMaCanBo(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlCanBoPhuCapKeHoaches.Where(x => x.MaCanBo == maCanBo).ToList();
            }
        }

        public IEnumerable<TlCanBoPhuCapKeHoachQuery> FindCanBoPhuCapKeHoachByMaCanBo(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_get_can_bo_phu_cap_ke_hoach @maCanBo";
                var parameters = new[]
                {
                    new SqlParameter("@MaCanBo", maCanBo)
                };
                return ctx.FromSqlRaw<TlCanBoPhuCapKeHoachQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlPhuCapQuery> FindCanBoPhuCap(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_get_data_phucap_kehoach @maCanBo";
                var parameters = new[]
                {
                    new SqlParameter("@maCanBo", maCanBo)
                };
                return ctx.FromSqlRaw<TlPhuCapQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlPhuCapNq104Query> FindCanBoPhuCapNq104(string maCanBo)
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

        public int DeleteByUnitYear(int year, string unit)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_tl_delete_can_bo_phu_cap_ke_hoach_theo_nam_donvi @Nam, @DonVi";
                var parameters = new[]
                {
                    new SqlParameter("@Nam", year.ToString()),
                    new SqlParameter("@DonVi", unit)
                };
                return ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }
    }
}
