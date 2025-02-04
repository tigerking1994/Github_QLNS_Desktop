using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class ImpQuyetToanRepository : Repository<ImpQuyetToan>, IImpQuyetToanRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public ImpQuyetToanRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
