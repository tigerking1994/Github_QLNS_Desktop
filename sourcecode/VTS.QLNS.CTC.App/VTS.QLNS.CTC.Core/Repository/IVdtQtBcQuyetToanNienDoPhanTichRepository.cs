using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtQtBcQuyetToanNienDoPhanTichRepository : IRepository<VdtQtBcQuyetToanNienDoPhanTich>
    {
        List<VdtQtBcQuyetToanNienDoPhanTich> GetBcQuyetToanNienDoPhanTich(Guid iIdParentId);
        void DeleteByParent(Guid iId);
        IEnumerable<VdtQtBcQuyetToanNienDoPhanTichQuery> GetBaoCaoQuyetToanNienDoPhanTich(string iIdMaDonVi, int iNamKeHoach, int iIdNguonVon);
        IEnumerable<VdtQtBcQuyetToanNienDoPhanTichQuery> GetBaoCaoQuyetToanNienDoPhanTichById(Guid iIdBcQuyetToanNienDo);
    }
}
