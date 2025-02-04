using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDaDmDuToanHangMucRepository : Repository<VdtDaDuToanDmHangMuc>, IVdtDaDmDuToanHangMucRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDaDmDuToanHangMucRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public VdtDaDuToanDmHangMuc FindByMa(string ma)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaDuToanDmHangMucs.Where(x => x.SMaHangMuc == ma).FirstOrDefault();
            }
        }
    }
}
