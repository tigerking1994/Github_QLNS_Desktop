using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlMapColumnConfigRepository : Repository<TlMapColumnConfig>, ITlMapColumnConfigRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlMapColumnConfigRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
