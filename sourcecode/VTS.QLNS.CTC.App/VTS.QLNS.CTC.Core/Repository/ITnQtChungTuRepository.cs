using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITnQtChungTuRepository : IRepository<TnQtChungTu>
    {
        int FindNextSoChungTuIndex(Expression<Func<TnQtChungTu, bool>> predicate);
        int LockOrUnLock(Guid id, bool lockStatus);
        TnQtChungTu FindAggregateVoucher(string voucherNoes);
        IEnumerable<TnQtChungTu> FindByIdDonVi(string idDonVi, int iThangQuyLoai);
    }
}
