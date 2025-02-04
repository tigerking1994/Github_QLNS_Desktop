using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmThueThuNhapCaNhanRepository : Repository<TlDmThueThuNhapCaNhan>, ITlDmThueThuNhapCaNhanRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmThueThuNhapCaNhanRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
