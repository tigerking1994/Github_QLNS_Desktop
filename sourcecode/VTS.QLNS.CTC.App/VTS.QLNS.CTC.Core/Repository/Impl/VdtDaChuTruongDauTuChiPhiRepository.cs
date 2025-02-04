using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDaChuTruongDauTuChiPhiRepository : Repository<VdtDaChuTruongDauTuChiPhi>, IVdtDaChuTruongDauTuChiPhiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDaChuTruongDauTuChiPhiRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
