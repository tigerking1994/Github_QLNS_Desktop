using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class SktChungTuChiTietCanCuChungTuRepository : Repository<NsSktChungTuChungTuCanCu>, ISktChungTuChiTietCanCuChungTuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public SktChungTuChiTietCanCuChungTuRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
