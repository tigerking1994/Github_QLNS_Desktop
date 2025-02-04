using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhKhcCheDoBhXhService
    {
        void Add(BhKhcCheDoBhXh entity);
        void Update(BhKhcCheDoBhXh entity);
        void Delete(Guid id);
        void LockOrUnlock(Guid id, bool status);
        IEnumerable<BhKhcCheDoBhXhQuery> FindIndex();
        int GetSoChungTuIndexByCondition(int iNamLamViec);
        BhKhcCheDoBhXh FindById(Guid id);
        bool IsExistChungTuTongHop(int loaiTongHop, int namLamViec);
        IEnumerable<BhKhcCheDoBhXh> FindByCondition(Expression<Func<BhKhcCheDoBhXh, bool>> predicate);
        List<DonVi> FindByDonViForNamLamViec(int namLamViec);
        BhKhcCheDoBhXh FindAggregateVoucher(int yearOfWork);
    }
}
