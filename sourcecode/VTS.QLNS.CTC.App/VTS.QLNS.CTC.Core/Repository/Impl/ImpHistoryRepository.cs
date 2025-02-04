using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class ImpHistoryRepository : Repository<ImpHistory>, IImpHistoryRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public ImpHistoryRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
