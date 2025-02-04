using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDaGoiThauChiPhiRepository : Repository<NhDaGoiThauChiPhi>, INhDaGoiThauChiPhiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaGoiThauChiPhiRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhDaDetailChiPhiQuery> GetGoiThauChiPhiByKhlcntId(Guid iIdKhlcnt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@iIdKhlcnt", iIdKhlcnt)
                };
                string executeSql = "EXECUTE dbo.sp_nh_goithau_chiphi_by_khlcnt @iIdKhlcnt";
                return ctx.FromSqlRaw<NhDaDetailChiPhiQuery>(executeSql, parameters).ToList();
            }
        }
        public IEnumerable<NhDaDetailChiPhiQuery> GetGoiThauChiPhiByGoiThauId(Guid iIdKhlcnt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@iIdKhlcnt", iIdKhlcnt)
                };
                string executeSql = "EXECUTE dbo.sp_nh_goithau_chiphi_by_goithau @iIdKhlcnt";
                return ctx.FromSqlRaw<NhDaDetailChiPhiQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<NhDaGoiThauChiPhi> FindListChiPhiByKhlcnt(Guid iIdKhlcnt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@iIdKhlcnt", iIdKhlcnt)
                };
                string executeSql = "EXECUTE dbo.sp_nh_goithau_chiphi_by_khlcnt_listall @iIdKhlcnt";
                return ctx.FromSqlRaw<NhDaGoiThauChiPhi>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<NhDaGoiThauChiPhi> FindListChiPhiByGT(Guid iIdKhlcnt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@iIdKhlcnt", iIdKhlcnt)
                };
                string executeSql = "EXECUTE dbo.sp_nh_goithau_chiphi_by_GoiThau_listall @iIdKhlcnt";
                return ctx.FromSqlRaw<NhDaGoiThauChiPhi>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<NhDaGoiThauChiPhiQuery> FindByGoiThauId(Guid idGoiThau)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_chiphi_bygoithauid @idGoiThau";
                var parameters = new[] {
                    new SqlParameter("@idGoiThau", idGoiThau),
                };
                return ctx.FromSqlRaw<NhDaGoiThauChiPhiQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<NhDaCacQuyetDinhChiPhiGoiThauQuery> FindByCacQuyetDinhChiPhiByGoiThauId(Guid idGoiThau)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_cacquyetdinhchiphi_byidgoithau @idGoiThau";
                var parameters = new[] {
                    new SqlParameter("@idGoiThau", idGoiThau),
                };
                return ctx.FromSqlRaw<NhDaCacQuyetDinhChiPhiGoiThauQuery>(executeSql, parameters).ToList();
            }
        }
    }
}
