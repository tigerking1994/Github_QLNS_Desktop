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
    public class NhDaKhlcNhaThauRepository : Repository<NhDaKhlcnhaThau>, INhDaKhlcNhaThauRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaKhlcNhaThauRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhDaKhlcNhaThauQuery> GetAllKhlcntIndex(int iThuocMenu)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE sp_nh_khlcnhathau_index @iThuocMenu";
                var parameters = new[] {
                    new SqlParameter("@iThuocMenu", iThuocMenu)
                };
                return ctx.FromSqlRaw<NhDaKhlcNhaThauQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<NhDaKhlcNhaThauQuery> GetAllKhlcntMuaSam()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE sp_nh_khlcnhathau_index_2";
                return ctx.FromSqlRaw<NhDaKhlcNhaThauQuery>(executeSql).ToList();
            }
        }

        public void DeleteById(Guid iId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "sp_nh_khlcnhathau_delete_by_id @iId";
                var parameters = new[] {
                    new SqlParameter("@iId", iId)
                };
                ctx.Database.ExecuteSqlCommand(executeSql, parameters);
            }
        }
    }
}
