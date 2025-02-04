using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VDTDuAnRepository : Repository<VdtDaDuAn>, IVDTDuAnRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VDTDuAnRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
