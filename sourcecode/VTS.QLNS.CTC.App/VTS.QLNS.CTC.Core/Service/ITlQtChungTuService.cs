using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlQtChungTuService
    {
        IEnumerable<TlQtChungTu> FindAll();
        IEnumerable<TlQtChungTu> FindChungTuExist(int yearOfWork, int thang, string maDonVi);
        IEnumerable<TlQtChungTu> FindAll(Expression<Func<TlQtChungTu, bool>> predicate);
        TlQtChungTu FindById(Guid id);
        int Add(TlQtChungTu entity);
        void Add(IEnumerable<TlQtChungTu> entities, IEnumerable<TlQtChungTuChiTiet> detailEntities);
        int Delete(Guid id);
        void LockOrUnlock(Guid id, bool isLock);
        int Update(TlQtChungTu entity);
        int GetSoChungTuIndexByCondition(int namLamViec);
    }
}
