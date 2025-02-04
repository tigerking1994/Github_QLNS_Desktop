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
    public class VdtTtDeNghiThanhToanUngRepository : Repository<VdtTtDeNghiThanhToanUng>, IVdtTtDeNghiThanhToanUngRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtTtDeNghiThanhToanUngRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<VdtTtDeNghiThanhToanUngQuery> GetDeNghiThanhToanUngIndex()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtTtDeNghiThanhToanUngQuery>("EXECUTE dbo.sp_vdt_denghithanhtoanung_index").ToList();
            }
        }
    }
}
