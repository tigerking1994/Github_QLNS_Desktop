using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDaDuToanChiPhiRepository : Repository<VdtDaDuToanChiPhi>, IVdtDaDuToanChiPhiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDaDuToanChiPhiRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
