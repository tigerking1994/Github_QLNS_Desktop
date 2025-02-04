using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class SktChungTuChiTietCanCuRepository : Repository<NsSktChungTuChiTietCanCu>, ISktChungTuChiTietCanCuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public SktChungTuChiTietCanCuRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
