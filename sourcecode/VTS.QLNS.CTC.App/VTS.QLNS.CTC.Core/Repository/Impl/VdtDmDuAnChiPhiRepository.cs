using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDmDuAnChiPhiRepository : Repository<VdtDmDuAnChiPhi>, IVdtDmDuAnChiPhiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDmDuAnChiPhiRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public VdtDmDuAnChiPhi FindByMaChiPhi(string ma)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDmDuAnChiPhis.Where(n => n.SMaChiPhi == ma).FirstOrDefault();
            }
        }

        public VdtDmDuAnChiPhi FindByName(string name)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDmDuAnChiPhis.Where(n => n.STenChiPhi == name).FirstOrDefault();
            }
        }
    }
}
