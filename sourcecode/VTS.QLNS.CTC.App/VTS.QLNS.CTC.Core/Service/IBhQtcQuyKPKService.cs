using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhQtcQuyKPKService
    {
        IEnumerable<BhQtcQuyKPKQuery> FindIndex(int yearOfWork);
        void Add(BhQtcQuyKPK entity);
        void Delete(Guid id);
        void Update(BhQtcQuyKPK entity);
        BhQtcQuyKPK FindById(Guid id);
        IEnumerable<BhQtcQuyKPK> FindByCondition(Expression<Func<BhQtcQuyKPK, bool>> predicate);
        void LockOrUnlock(Guid id, bool status);
        int GetSoChungTuIndexByCondition(int namLamViec);
        IEnumerable<DonVi> FindByDonViForNamLamViec(int namLamViec, int iQuy, int iLoaiChungTu,Guid loaiChiQT);
        IEnumerable<BhQtcQuyKPK> FindByAggregateVoucher(List<string> voucherNos, int yearOfWork);
    }
}
