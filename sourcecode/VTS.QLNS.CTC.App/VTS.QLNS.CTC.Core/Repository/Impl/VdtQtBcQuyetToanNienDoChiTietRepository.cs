using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtQtBcQuyetToanNienDoChiTietRepository : Repository<VdtQtBcQuyetToanNienDoChiTiet01>, IVdtQtBcQuyetToanNienDoChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtQtBcQuyetToanNienDoChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void DeleteDeNghiQuyetToanByParentId(Guid iIDParentID)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var lstDeNghiChiTiet = ctx.VdtQtBcquyetToanNienDoChiTiet01s.Where(n => n.IIdBcquyetToanNienDo == iIDParentID);
                if (lstDeNghiChiTiet == null) return;
                ctx.VdtQtBcquyetToanNienDoChiTiet01s.RemoveRange(lstDeNghiChiTiet);
                ctx.SaveChanges();
            }
        }
    }
}
