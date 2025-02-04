using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmNangLuongRepository : Repository<TlDmNangLuong>, ITlDmNangLuongRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmNangLuongRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
