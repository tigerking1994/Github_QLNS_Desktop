using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITnQtChungTuService
    {
        TnQtChungTu Add(TnQtChungTu entity);
        int Delete(Guid id);
        IEnumerable<TnQtChungTu> FindByCondition(Expression<Func<TnQtChungTu, bool>> predicate);
        TnQtChungTu FindById(Guid id);
        int FindNextSoChungTuIndex(Expression<Func<TnQtChungTu, bool>> predicate);
        int LockOrUnLock(Guid id, bool lockStatus);
        int Update(TnQtChungTu item);
        TnQtChungTu FindAggregateVoucher(string voucherNoes);
        IEnumerable<TnQtChungTu> FindByIdDonVi(string idDonVi, int iThangQuyLoai);
        IEnumerable<TnQtChungTu> FindAll();
    }
}
