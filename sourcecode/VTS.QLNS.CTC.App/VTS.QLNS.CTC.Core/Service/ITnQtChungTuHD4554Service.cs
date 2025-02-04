using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITnQtChungTuHD4554Service
    {
        TnQtChungTuHD4554 Add(TnQtChungTuHD4554 entity);
        int Delete(Guid id);
        IEnumerable<TnQtChungTuHD4554> FindByCondition(Expression<Func<TnQtChungTuHD4554, bool>> predicate);
        TnQtChungTuHD4554 FindById(Guid id);
        int FindNextSoChungTuIndex(Expression<Func<TnQtChungTuHD4554, bool>> predicate);
        int LockOrUnLock(Guid id, bool lockStatus);
        int Update(TnQtChungTuHD4554 item);
        TnQtChungTuHD4554 FindAggregateVoucher(string voucherNoes);
        IEnumerable<TnQtChungTuHD4554> FindByIdDonVi(string idDonVi, int iThangQuyLoai);
        IEnumerable<TnQtChungTuHD4554> FindAll();
        IEnumerable<TnQtChungTuHD4554Query> GetChungTuHD4554(int iNamLamViec);
        List<string> FindLNSExist(SettlementVoucherCriteria condition, Guid voucherId, List<string> listLNSSelected);
    }
}
