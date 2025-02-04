using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INsQtChungTuChiTietGiaiThichService
    {
        NsQtChungTuChiTietGiaiThich FindById(Guid id);
        NsQtChungTuChiTietGiaiThich FindByCondition(SettlementVoucherDetailExplainCriteria condition);
        NsQtChungTuChiTietGiaiThich FindByCondition(Expression<Func<NsQtChungTuChiTietGiaiThich, bool>> predicate);
        IEnumerable<NsQtChungTuChiTietGiaiThich> FindListCondition(SettlementVoucherDetailExplainCriteria condition);
        void Add(NsQtChungTuChiTietGiaiThich chungTuChiTietGiaiThich);
        void Update(NsQtChungTuChiTietGiaiThich chungTuChiTietGiaiThich);
        int Delete(Guid id);
        IEnumerable<NsQtChungTuChiTietGiaiThich> GetDataChungTuGiaiTichBangSo(string sLoai, string iID_MaDonVi, int iThang, int iQuy, int iNamLamViec, int iNamNganSach, int iID_NguonNganSach, int isTongHop, string explainId);
        IEnumerable<NsQtChungTuChiTietGiaiThich> GetDataChungTuGiaiTichBangLoi(string sLoai, string iID_MaDonVi, int iThang, int iQuy, int iNamLamViec, int iNamNganSach, int iID_NguonNganSach, int isTongHop, string explainId);
    }
}
