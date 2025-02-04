using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlQtChungTuNq104Service
    {
        IEnumerable<TlQtChungTuNq104> FindAll();
        IEnumerable<TlQtChungTuNq104> FindChungTuExist(int yearOfWork, int thang, string maDonVi);
        IEnumerable<TlQtChungTuNq104> FindAll(Expression<Func<TlQtChungTuNq104, bool>> predicate);
        TlQtChungTuNq104 FindById(Guid id);
        int Add(TlQtChungTuNq104 entity);
        void Add(IEnumerable<TlQtChungTuNq104> entities, IEnumerable<TlQtChungTuChiTietNq104> detailEntities);
        int Delete(Guid id);
        void LockOrUnlock(Guid id, bool isLock);
        int Update(TlQtChungTuNq104 entity);
        int GetSoChungTuIndexByCondition(int namLamViec);
    }
}
