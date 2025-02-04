using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NsDtChungTuCanCuRepository : Repository<NsDtChungTuCanCu>, INsDtChungTuCanCuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NsDtChungTuCanCuRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
