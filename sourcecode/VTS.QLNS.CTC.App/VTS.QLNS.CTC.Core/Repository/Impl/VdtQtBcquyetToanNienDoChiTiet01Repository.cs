using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtQtBcquyetToanNienDoChiTiet01Repository : Repository<VdtQtBcQuyetToanNienDoChiTiet01>, IVdtQtBcquyetToanNienDoChiTiet01Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtQtBcquyetToanNienDoChiTiet01Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public List<VdtQtBcQuyetToanNienDoChiTiet01> GetDenghiQuyetToanNienDoChiTiet01ByParent(Guid iIdParentId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtQtBcquyetToanNienDoChiTiet01s.Where(n => n.IIdBcquyetToanNienDo == iIdParentId).ToList();
            }
        }
    }
}
