using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDaDuToanNguonVonRepository : Repository<VdtDaDuToanNguonvon>, IVdtDaDuToanNguonVonRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDaDuToanNguonVonRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
