using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtQddtNguonVonRepository : Repository<VdtDaQddauTuNguonVon>, IVdtQddtNguonVonRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtQddtNguonVonRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
