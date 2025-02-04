using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class ImpTnQtThuNopRepository : Repository<ImpTnQtThuNop>, IImpTnQtThuNopRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public ImpTnQtThuNopRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
