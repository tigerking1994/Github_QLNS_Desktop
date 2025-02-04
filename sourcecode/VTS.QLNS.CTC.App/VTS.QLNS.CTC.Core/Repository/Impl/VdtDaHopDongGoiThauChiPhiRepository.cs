using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDaHopDongGoiThauChiPhiRepository : Repository<VdtDaHopDongGoiThauChiPhi>, IVdtDaHopDongGoiThauChiPhiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDaHopDongGoiThauChiPhiRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
