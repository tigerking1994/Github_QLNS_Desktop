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
    public interface IBhKhcKService
    {
        void Add(BhKhcK entity);
        void Delete(Guid id);
        void Update(BhKhcK entity);
        IEnumerable<BhKhcKQuery> FindIndex();
        BhKhcK FindById(Guid id);
        IEnumerable<BhKhcK> FindByCondition(Expression<Func<BhKhcK, bool>> predicate);
        void LockOrUnlock(Guid id, bool status);

        int GetSoChungTuIndexByCondition(int yearOfWork);
        List<DonVi> FindByDonViForNamLamViec(int yearOfWork, Guid IdLoaiChi);
        List<BhKhcK> FindAggregateVoucher(int yearOfWork);
    }
}
