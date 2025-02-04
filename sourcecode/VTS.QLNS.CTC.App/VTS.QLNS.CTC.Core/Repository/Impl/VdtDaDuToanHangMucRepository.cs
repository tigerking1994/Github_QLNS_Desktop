using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDaDuToanHangMucRepository : Repository<VdtDaDuToanHangMuc>, IVdtDaDuToanHangMucRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDaDuToanHangMucRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
