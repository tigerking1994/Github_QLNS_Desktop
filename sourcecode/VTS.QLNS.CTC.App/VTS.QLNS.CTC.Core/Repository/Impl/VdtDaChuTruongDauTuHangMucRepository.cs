using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDaChuTruongDauTuHangMucRepository : Repository<VdtDaChuTruongDauTuHangMuc>, IVdtDaChuTruongDauTuHangMucRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDaChuTruongDauTuHangMucRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
