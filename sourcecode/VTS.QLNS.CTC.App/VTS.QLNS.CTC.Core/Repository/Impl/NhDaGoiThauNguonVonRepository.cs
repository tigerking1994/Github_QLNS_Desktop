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
    public class NhDaGoiThauNguonVonRepository : Repository<NhDaGoiThauNguonVon>, INhDagoiThauNguonVonRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaGoiThauNguonVonRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhDaGoiThauThongTinNguonVonQuery> FindByIdGoiThau(Guid idGoiThau)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_thongtinnguonvon_byidgoithau @idGoiThau";
                var parameters = new[] {
                    new SqlParameter("@idGoiThau", idGoiThau),
                };
                return ctx.FromSqlRaw<NhDaGoiThauThongTinNguonVonQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<NhDaCacQuyetDinhNguonVonGoiThauQuery> FindCacQuyetDinhNguonVonByIdGoiThau(Guid idGoiThau)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@idGoiThau", idGoiThau)
                };
                string executeSql = "EXECUTE dbo.sp_nh_cacquyetdinhnguonvon_byidgoithau @idGoiThau";
                return ctx.FromSqlRaw<NhDaCacQuyetDinhNguonVonGoiThauQuery>(executeSql, parameters).ToList();
            }
        }
        public IEnumerable<NhDaGoiThauNguonVon> GetListNguonVonByIdGoiThau(Guid idGoiThau)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhDaGoiThauNguonVons.Where(n => n.IIdGoiThauId == idGoiThau).ToList();
            }
        }

        public IEnumerable<NhDaGoiThauNguonVon> FindByListNguonVonKhlcntId(Guid iIdKhlcnt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@iIdKhlcnt", iIdKhlcnt)
                };
                string executeSql = "EXECUTE dbo.sp_nh_goithau_nguonvon_by_khlcnt_listall @iIdKhlcnt";
                return ctx.FromSqlRaw<NhDaGoiThauNguonVon>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<NhDaGoiThauNguonVon> FindByListNguonVonGoiThauId(Guid iIdKhlcnt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@iIdKhlcnt", iIdKhlcnt)
                };
                string executeSql = "EXECUTE dbo.sp_nh_goithau_nguonvon_by_goithau_listall @iIdKhlcnt";
                return ctx.FromSqlRaw<NhDaGoiThauNguonVon>(executeSql, parameters).ToList();
            }
        }



        public IEnumerable<NhDaDetailNguonVonQuery> GetGoiThauNguonVonByKhlcntId(Guid iIdKhlcnt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@iIdKhlcnt", iIdKhlcnt)
                };
                string executeSql = "EXECUTE dbo.sp_nh_goithau_nguonvon_by_khlcnt @iIdKhlcnt";
                return ctx.FromSqlRaw<NhDaDetailNguonVonQuery>(executeSql, parameters).ToList();
            }
        }
        public IEnumerable<NhDaDetailNguonVonQuery> GetGoiThauNguonVonByGoiThauId(Guid iIdKhlcnt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@iIdKhlcnt", iIdKhlcnt)
                };
                string executeSql = "EXECUTE dbo.sp_nh_goithau_nguonvon_by_goithau @iIdKhlcnt";
                return ctx.FromSqlRaw<NhDaDetailNguonVonQuery>(executeSql, parameters).ToList();
            }
        }
    }
}
