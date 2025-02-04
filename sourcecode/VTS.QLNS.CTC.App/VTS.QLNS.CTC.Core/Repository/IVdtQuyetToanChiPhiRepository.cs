using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;
namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtQuyetToanChiPhiRepository : IRepository<VdtQtQuyetToanChiPhi>
    {
        IEnumerable<PheDuyetQuyetToanDetailQuery> FindAllPheDuyetQuyetToanDetailByCondition(string idDuAn, DateTime ngay);

        VdtQtQuyetToan FindQuyetToanByIdQt(Guid quyetToanId);
    }
}
