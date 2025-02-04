using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtQuyetToanNguonVonRepository : Repository<VdtQtQuyetToanNguonvon>, IVdtQuyetToanNguonVonRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtQuyetToanNguonVonRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public List<VdtQtQuyetToanNguonvon> FindByQuyetToanId(Guid quyetToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtQtQuyetToanNguonvons.Where(n => n.IIdQuyetToanId == quyetToanId).ToList();
            }
        }
    }
}
