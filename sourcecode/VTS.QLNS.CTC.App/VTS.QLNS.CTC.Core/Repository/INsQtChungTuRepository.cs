using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INsQtChungTuRepository : IRepository<NsQtChungTu>
    {
        IEnumerable<NsQtChungTu> FindByType(string type);
        IEnumerable<NsQtChungTuQuery> FindByCondition(SettlementVoucherCriteria condition);
        NsQtChungTu FindAggregateVoucher(string voucherNoes);
        List<string> FindAgencyIdByMonth(ReportSettlementCriteria condition);
        void DeleteRange(List<NsQtChungTu> chungTus);
        int FindLastIndex(SettlementVoucherCriteria condition);
        int FindLastAdjustIndex(SettlementVoucherCriteria condition);
        void LockOrUnlockMultiple(List<NsQtChungTu> chungTus, bool isLock);
        IEnumerable<NsQtChungTu> FindByAggregateVoucher(List<string> voucherNoes, int yearOfWork, int yearOfBudget, int budgetSource, string voucherType);
        void UpdateAggregateStatus(string voucherIds);
    }
}
