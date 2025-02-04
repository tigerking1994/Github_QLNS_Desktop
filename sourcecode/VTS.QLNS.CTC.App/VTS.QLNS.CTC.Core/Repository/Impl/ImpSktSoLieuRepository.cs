using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class ImpSktSoLieuRepository : Repository<ImpSktSoLieuChiTiet>, IImpSktSoLieuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public ImpSktSoLieuRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
