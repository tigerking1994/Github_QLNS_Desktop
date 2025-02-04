using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITnDtdnChungTuRepository : IRepository<TnDtdnChungTu>
    {
        int LockOrUnLock(Guid id, bool lockStatus);
        int FindNextSoChungTuIndex(Expression<Func<TnDtdnChungTu, bool>> predicate);
        TnDtdnChungTu FindAggregateVoucher(string voucherNoes);
        void CreateDataReportTotalSummary(string id, int namLamViec, int namNganSach, int nguonNganSach, string idDonVi, string listChungTuTongHop, string nguoiTao);
        int DeleteItem(Guid id);
        List<string> GetAgencyCodeByVoucherDetail(Expression<Func<TnDtdnChungTu, bool>> predicate);
        IEnumerable<string> GetLnsHasData(List<Guid> chungTuIds);
        IEnumerable<BaoCaoNhanVaQuyetToanKinhPhi> GetBaoCaoNhanVaQuyetToanKinhPhis(string sMaDonVi, int NamLamViec, int DonViTinh);

    }
}
