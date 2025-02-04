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
    public class VdtTtThanhToanQuaKhoBacRepository : Repository<VdtTtThanhToanQuaKhoBac>, IVdtTtThanhToanQuaKhoBacRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtTtThanhToanQuaKhoBacRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<VdtTtThanhToanQuaKhoBacQuery> GetDataIndex()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtTtThanhToanQuaKhoBacQuery>("EXECUTE dbo.sp_vdt_thanhtoankhobac_index").ToList();
            }
        }
    }
}
