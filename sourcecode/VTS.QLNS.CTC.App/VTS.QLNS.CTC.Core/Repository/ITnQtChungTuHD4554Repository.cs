using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITnQtChungTuHD4554Repository : IRepository<TnQtChungTuHD4554>
    {
        TnQtChungTuHD4554 FindAggregateVoucher(string voucherNoes);
        IEnumerable<TnQtChungTuHD4554> FindByIdDonVi(string idDonVi, int iThangQuyLoai);
        int FindNextSoChungTuIndex(Expression<Func<TnQtChungTuHD4554, bool>> predicate);
        IEnumerable<TnQtChungTuHD4554Query> GetChungTuHD4554(int iNamLamViec);
        int LockOrUnLock(Guid id, bool lockStatus);
    }
}
