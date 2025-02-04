using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IPheDuyetQuyetToanService
    {
        IEnumerable<PheDuyetQuyetToanDetailQuery> FindAllPheDuyetQuyetToanDetailByCondition(string idDuAn, DateTime ngay);
        int AddQuyetToanChiPhi(VdtQtQuyetToanChiPhi entity);
        int AddRangeQuyetToanChiPhi(IEnumerable<VdtQtQuyetToanChiPhi> entities);
        int AddRangeQuyetToanNguonVon(IEnumerable<VdtQtQuyetToanNguonvon> entities);
        VdtQtQuyetToanChiPhi FindQuyetToanChiPhi(params object[] keyValues);
        VdtQtQuyetToanNguonvon FindQuyetToanNguonVon(params object[] keyValues);
        int UpdateChiPhi(VdtQtQuyetToanChiPhi entity);
        int UpdateNguonVon(VdtQtQuyetToanNguonvon entity);
        int DeleteChiPhi(Guid id);
        int DeleteNguonVon(Guid id);
        void DeleteQuyetToanNguonVonByQuyetToanId(Guid quyetToanId);
        List<VdtQtQuyetToanNguonvon> FindByQuyetToanId(Guid quyetToanId);

        VdtQtQuyetToan FindQuyetToanByIdQt(Guid quyetToanId);
    }
}
