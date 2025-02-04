using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class ImpSoKiemTraRepository : Repository<ImpSoKiemTra>, IImpSoKiemTraRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public ImpSoKiemTraRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
