using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITnDtChungTuService
    {
        int LockOrUnLock(Guid id, bool lockStatus);
        IEnumerable<TnDtChungTu> FindByCondition(Expression<Func<TnDtChungTu, bool>> predicate);
        int FindNextSoChungTuIndex(Expression<Func<TnDtChungTu, bool>> predicate);
        TnDtChungTu Add(TnDtChungTu entity);
        int Update(TnDtChungTu item);
        int Delete(Guid id);
        TnDtChungTu FindById(Guid id);
        TnDtChungTu FindAggregateVoucher(string voucherNoes);
        IEnumerable<TnDtChungTu> FindByType(int iLoai);
        IEnumerable<string> GetLnsHasData(List<Guid> chungTuIds);
        bool CheckDeletePhanBo(Guid id);
        TnDtChungTu FindByIdDotNhan(string sid);
        List<string> GetAgencyCodeByVoucherDetail(Expression<Func<TnDtChungTu, bool>> predicate);
    }
}
