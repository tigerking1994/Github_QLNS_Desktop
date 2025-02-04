using System.Data.SqlClient;
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
    public class VdtKtKhoiTaoRepository : Repository<VdtKtKhoiTao>, IVdtKtKhoiTaoRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtKtKhoiTaoRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<KhoiTaoQuery> FindByCondition(int namLamViec)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
				string sql = "EXECUTE dbo.sp_vdt_khoitao_index @YearOfWork";
				var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", namLamViec)
                };
                return ctx.FromSqlRaw<KhoiTaoQuery>(sql, parameters).ToList();
            }
        }
    }
}
