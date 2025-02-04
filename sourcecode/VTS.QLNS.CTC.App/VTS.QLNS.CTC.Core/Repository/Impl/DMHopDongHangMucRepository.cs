using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class DMHopDongHangMucRepository : Repository<VdtDaHopDongDmHangMuc>, IDMHopDongHangMucRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public DMHopDongHangMucRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
