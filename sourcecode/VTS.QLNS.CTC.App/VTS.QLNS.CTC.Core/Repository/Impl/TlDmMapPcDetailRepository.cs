using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmMapPcDetailRepository : Repository<TlDmMapPcDetail>, ITlDmMapPcDetailRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmMapPcDetailRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
