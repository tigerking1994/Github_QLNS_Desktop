using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDaNhomDuAnRepository : Repository<VdtDmNhomDuAn>, IVdtDaNhomDuAnRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDaNhomDuAnRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
