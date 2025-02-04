using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtQuyetToanNguonVonRepository : IRepository<VdtQtQuyetToanNguonvon>
    {
        List<VdtQtQuyetToanNguonvon> FindByQuyetToanId(Guid quyetToanId);
    }
}
