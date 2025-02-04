using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhKhcKcbService
    {
        void Add(BhKhcKcb entity);
        void Delete(Guid id);
        void Update(BhKhcKcb entity);
        IEnumerable<BhKhcKcbQuery> FindIndex();
        BhKhcKcb FindById(Guid id);
        IEnumerable<BhKhcKcb> FindByCondition(Expression<Func<BhKhcKcb, bool>> predicate);
        void LockOrUnlock(Guid id, bool status);

        int GetSoChungTuIndexByCondition(int yearOfWork);
        List<DonVi> FindByDonViForNamLamViec(int yearOfWork);
        BhKhcKcb FindAggregateVoucher(int yearOfWork);
    }
}
