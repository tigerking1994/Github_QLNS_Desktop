using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmHslKeHoachRepository : Repository<TlDmHslKeHoach>, ITlDmHslKeHoachRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmHslKeHoachRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
