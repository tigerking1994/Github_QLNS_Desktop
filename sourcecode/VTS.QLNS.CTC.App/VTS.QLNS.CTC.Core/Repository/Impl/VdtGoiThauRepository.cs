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
    public class VdtGoiThauRepository : Repository<VdtDaGoiThau>, IVdtGoiThauRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtGoiThauRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<GoiThauQuery> FindGoiThauByDuAnId(string idDuAn, Guid? iIdHopDong)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_get_goi_thau @DuAnId, @iIdHopDong";
                var parameters = new[]
                {
                    new SqlParameter("@DuAnId", idDuAn),
                    new SqlParameter("@iIdHopDong", iIdHopDong)
                };
                return ctx.FromSqlRaw<GoiThauQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<GoiThauQuery> FindGoiThauByHopDong(Guid? iIdHopDong)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_get_goi_thau_by_hopdong  @iIdHopDong";
                var parameters = new[]
                {
                    new SqlParameter("@iIdHopDong", iIdHopDong)
                };
                return ctx.FromSqlRaw<GoiThauQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<VdtDaGoiThau> FindGoiThauDieuChinh(Guid khLuaChonNhaThauId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaGoiThaus.Where(x => x.IIdKhlcnhaThau == khLuaChonNhaThauId);
            }
        }
    }
}
