using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDaQDDauTuDMHangMucRepository : Repository<VdtDaQddauTuDmHangMuc>, IVdtDaQDDauTuDMHangMucRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDaQDDauTuDMHangMucRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
