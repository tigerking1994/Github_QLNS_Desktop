using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDuToanDMHangMucRepository : Repository<VdtDaDuToanDmHangMuc>, IVdtDuToanDMHangMucRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDuToanDMHangMucRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
