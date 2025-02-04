using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtNcNhuCauChiChiTietRepository : Repository<VdtNcNhuCauChiChiTiet>, IVdtNcNhuCauChiChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtNcNhuCauChiChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public List<VdtNcNhuCauChiChiTiet> GetDetailByParent(Guid iIdParentId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtNcNhuCauChiChiTiets.Where(n => n.IIdNhuCauChiId == iIdParentId).ToList();
            }
        }

        public void DeleteDetailByParent(Guid iIdParentID)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var lstData = ctx.VdtNcNhuCauChiChiTiets.Where(n => n.IIdNhuCauChiId == iIdParentID);
                if (lstData == null) return;
                ctx.VdtNcNhuCauChiChiTiets.RemoveRange(lstData);
                ctx.SaveChanges();
            }
        }
    }
}
