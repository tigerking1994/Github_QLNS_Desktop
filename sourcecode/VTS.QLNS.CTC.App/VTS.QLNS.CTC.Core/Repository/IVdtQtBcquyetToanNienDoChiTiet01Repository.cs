using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtQtBcquyetToanNienDoChiTiet01Repository : IRepository<VdtQtBcQuyetToanNienDoChiTiet01>
    {
        List<VdtQtBcQuyetToanNienDoChiTiet01> GetDenghiQuyetToanNienDoChiTiet01ByParent(Guid iIdParentId);
    }
}
