using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhKhcKinhphiQuanlyService
    {
        void Add(BhKhcKinhphiQuanly entity);
        void Delete(Guid id);
        void Update(BhKhcKinhphiQuanly entity);
        IEnumerable<BhKhcKinhphiQuanlyQuery> FindIndex();
        BhKhcKinhphiQuanly FindById(Guid id);
        IEnumerable<BhKhcKinhphiQuanly> FindByCondition(Expression<Func<BhKhcKinhphiQuanly, bool>> predicate);
        void LockOrUnlock(Guid id, bool status);

        int GetSoChungTuIndexByCondition(int NamLamViec);
        List<DonVi> FindByDonViForNamLamViec(int yearOfWork);
        BhKhcKinhphiQuanly FindAggregateVoucher(int yearOfWork);
    }
}
