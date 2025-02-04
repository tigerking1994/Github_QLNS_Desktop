using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDaGoiThauChiPhiRepository : Repository<VdtDaGoiThauChiPhi>, IVdtDaGoiThauChiPhiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDaGoiThauChiPhiRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void DeleteByParentId(Guid iIdGoiThau)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var lstData = ctx.VdtDaGoiThauChiPhis.Where(n => n.IIdGoiThauId == iIdGoiThau).ToList();
                if (lstData == null) return;
                RemoveRange(lstData);
            }
        }
    }
}
