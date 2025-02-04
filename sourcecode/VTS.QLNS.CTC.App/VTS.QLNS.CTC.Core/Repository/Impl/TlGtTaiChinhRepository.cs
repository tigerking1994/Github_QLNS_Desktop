using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlGtTaiChinhRepository : Repository<TlGtTaiChinh>, ITlGtTaiChinhRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlGtTaiChinhRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
