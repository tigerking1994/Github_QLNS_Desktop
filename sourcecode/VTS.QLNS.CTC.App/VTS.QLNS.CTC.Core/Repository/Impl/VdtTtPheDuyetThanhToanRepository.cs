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
    public class VdtTtPheDuyetThanhToanRepository : Repository<VdtTtPheDuyetThanhToan>, IVdtTtPheDuyetThanhToanRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtTtPheDuyetThanhToanRepository(ApplicationDbContextFactory context)
            : base(context)
        {
            _contextFactory = context;
        }

        public List<VdtTtPheDuyetThanhToanQuery> GetAllPheDuyetThanhToan()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtTtPheDuyetThanhToanQuery>("sp_tt_get_pheduyetthanhtoan").ToList();
            }
        }
    }
}
