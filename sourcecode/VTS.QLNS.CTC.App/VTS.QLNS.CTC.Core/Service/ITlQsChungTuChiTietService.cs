using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlQsChungTuChiTietService
    {
        IEnumerable<TlQsChungTuChiTiet> FindAll();
        IEnumerable<TlQsChungTuChiTiet> FindAll(Expression<Func<TlQsChungTuChiTiet, bool>> predicate);
        TlQsChungTuChiTiet FirstOrDefault(Expression<Func<TlQsChungTuChiTiet, bool>> predicate);
        int Add(IEnumerable<TlQsChungTuChiTiet> entites);
        int DeleteByChungTuId(Guid ParentId);
        int UpDate(TlQsChungTuChiTiet entity);
        int DeleteId(Guid id);
        int UpDateRange(IEnumerable<TlQsChungTuChiTiet> entites);
    }
}
