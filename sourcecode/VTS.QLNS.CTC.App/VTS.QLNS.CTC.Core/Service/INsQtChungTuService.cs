using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INsQtChungTuService
    {
        List<NsQtChungTu> FindByType(string type);
        NsQtChungTu FindById(Guid id);
        List<NsQtChungTuQuery> FindByCondition(SettlementVoucherCriteria condition);
        IEnumerable<NsQtChungTu> FindByCondition(Expression<Func<NsQtChungTu, bool>> predicate);
        void LockOrUnlock(Guid id, bool isLock);
        void Add(NsQtChungTu chungTu);
        void Update(NsQtChungTu chungTu);
        void Delete(Guid id);
        NsQtChungTu FindAggregateVoucher(string voucherNoes);
        List<string> FindAgencyIdByMonth(ReportSettlementCriteria condition);
        List<string> FindLNSExist(SettlementVoucherCriteria condition, Guid voucherId, List<string> listLNSSelected);
        bool HasSTongHop(SettlementVoucherCriteria condition);
        void DeleteRange(List<NsQtChungTu> chungTus);
        int CreateVoucherIndex(SettlementVoucherCriteria condition);
        int CreateAdjustVoucherIndex(SettlementVoucherCriteria condition);
        void LockOrUnlockMultiple(List<NsQtChungTu> chungTus, bool isLock);
        IEnumerable<NsQtChungTu> FindByAggregateVoucher(List<string> voucherNoes, int yearOfWork, int yearOfBudget, int budgetSource, string voucherType);
        void UpdateAggregateStatus(string voucherIds);
        List<NsQtChungTu> GetDataExportJson(List<Guid> iIds);
        void BulkInsertNsQtChungTu(List<NsQtChungTu> lstData);
        void UpdateRange(List<NsQtChungTu> listChungTu);
    }
}
