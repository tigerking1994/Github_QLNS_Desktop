using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmCachTinhLuongChuanNq104Service
    {
        IEnumerable<TlDmCachTinhLuongChuanNq104> FindAll(Expression<Func<TlDmCachTinhLuongChuanNq104, bool>> predicate);
        TlDmCachTinhLuongChuanNq104 Find(Guid id);
        TlDmCachTinhLuongChuanNq104 FindByMaCot(string maCot);
        int Update(TlDmCachTinhLuongChuanNq104 entity);
        int Add(TlDmCachTinhLuongChuanNq104 entity);
        int Delete(Guid id);
    }
}
