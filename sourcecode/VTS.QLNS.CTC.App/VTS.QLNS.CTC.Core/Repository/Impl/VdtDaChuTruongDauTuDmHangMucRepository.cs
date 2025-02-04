using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDaChuTruongDauTuDmHangMucRepository : Repository<VdtDaChuTruongDauTuDmHangMuc>, IVdtDaChuTruongDauTuDmHangMucRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDaChuTruongDauTuDmHangMucRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
