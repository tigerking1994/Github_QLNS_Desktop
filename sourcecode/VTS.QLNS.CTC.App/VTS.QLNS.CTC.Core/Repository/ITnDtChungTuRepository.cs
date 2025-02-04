using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITnDtChungTuRepository : IRepository<TnDtChungTu>
    {
        int LockOrUnLock(Guid id, bool lockStatus);
        int FindNextSoChungTuIndex(Expression<Func<TnDtChungTu, bool>> predicate);
        TnDtChungTu FindAggregateVoucher(string voucherNoes);
        IEnumerable<TnDtChungTu> FindByType(int iLoai);
        IEnumerable<string> GetLnsHasData(List<Guid> chungTuIds);
        bool CheckDeletePhanBo(Guid id);
        TnDtChungTu FindByIdDotNhan(string sid);
        List<string> GetAgencyCodeByVoucherDetail(Expression<Func<TnDtChungTu, bool>> predicate);
    }
}
