using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDaChuTruongDauTuNguonVonRepository : Repository<VdtDaChuTruongDauTuNguonVon>, IVdtDaChuTruongDauTuNguonVonRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDaChuTruongDauTuNguonVonRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
