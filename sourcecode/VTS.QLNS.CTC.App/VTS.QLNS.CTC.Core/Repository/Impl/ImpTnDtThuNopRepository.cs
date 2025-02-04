using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class ImpTnDtThuNopRepository : Repository<ImpTnDtThuNop>, IImpTnDtThuNopRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public ImpTnDtThuNopRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
