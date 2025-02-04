using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INsQtChungTuChiTietGiaiThichRepository : IRepository<NsQtChungTuChiTietGiaiThich>
    {
        NsQtChungTuChiTietGiaiThich FindByCondition(SettlementVoucherDetailExplainCriteria condition);
        NsQtChungTuChiTietGiaiThich FindByCondition(Expression<Func<NsQtChungTuChiTietGiaiThich, bool>> predicate);
        IEnumerable<NsQtChungTuChiTietGiaiThich> GetDataChungTuGiaiTichBangSo(string sLoai, string iID_MaDonVi, int iThang, int iQuy, int iNamLamViec, int iNamNganSach, int iID_NguonNganSach, int isTongHop, string explainId);
        IEnumerable<NsQtChungTuChiTietGiaiThich> FindListCondition(SettlementVoucherDetailExplainCriteria condition);
        IEnumerable<NsQtChungTuChiTietGiaiThich> GetDataChungTuGiaiTichBangLoi(string sLoai, string iID_MaDonVi, int iThang, int iQuy, int iNamLamViec, int iNamNganSach, int iID_NguonNganSach, int isTongHop, string explainId);

    }
}
