using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtQddtChiPhiRepository : Repository<VdtDaQddauTuChiPhi>, IVdtQddtChiPhiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtQddtChiPhiRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
