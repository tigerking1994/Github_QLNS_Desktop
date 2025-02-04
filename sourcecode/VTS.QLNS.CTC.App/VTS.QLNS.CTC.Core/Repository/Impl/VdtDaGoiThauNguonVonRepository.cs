using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDaGoiThauNguonVonRepository : Repository<VdtDaGoiThauNguonVon>, IVdtDaGoiThauNguonVonRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDaGoiThauNguonVonRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void DeleteByParentId(Guid iIdGoiThau)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var lstData = ctx.VdtDaGoiThauNguonVons.Where(n => n.IIdGoiThauId == iIdGoiThau).ToList();
                if (lstData == null) return;
                ctx.RemoveRange(lstData);
                ctx.SaveChanges();
            }
        }
    }
}
