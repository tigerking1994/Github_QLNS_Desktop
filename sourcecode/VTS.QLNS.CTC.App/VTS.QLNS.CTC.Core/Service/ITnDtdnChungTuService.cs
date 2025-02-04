using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITnDtdnChungTuService
    {
        int LockOrUnLock(Guid id, bool lockStatus);
        IEnumerable<TnDtdnChungTu> FindByCondition(Expression<Func<TnDtdnChungTu, bool>> predicate);
        int FindNextSoChungTuIndex(Expression<Func<TnDtdnChungTu, bool>> predicate);
        TnDtdnChungTu Add(TnDtdnChungTu entity);
        int Update(TnDtdnChungTu item);
        int UpdateRange(IEnumerable<TnDtdnChungTu> items);
        int Delete(Guid id);
        TnDtdnChungTu FindById(Guid id);
        TnDtdnChungTu FindAggregateVoucher(string voucherNoes);
        void CreateDataReportTotalSummary(string id, int namLamViec, int namNganSach, int nguonNganSach, string idDonVi, string listChungTuTongHop, string nguoiTao);
        List<string> GetAgencyCodeByVoucherDetail(Expression<Func<TnDtdnChungTu, bool>> predicate);
        IEnumerable<string> GetLnsHasData(List<Guid> chungTuIds);
        IEnumerable<BaoCaoNhanVaQuyetToanKinhPhi> GetBaoCaoNhanVaQuyetToanKinhPhis(string sMaDonVi, int NamLamViec, int DonViTinh);



    }
}
