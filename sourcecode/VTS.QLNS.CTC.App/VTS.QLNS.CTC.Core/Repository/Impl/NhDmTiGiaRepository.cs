using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDmTiGiaRepository : Repository<NhDmTiGia>, INhDmTiGiaRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDmTiGiaRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

    }
}
