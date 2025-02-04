using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class LoaiHopDongRepository : Repository<VdtDmLoaiHopDong>, ILoaiHopDongRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public LoaiHopDongRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
