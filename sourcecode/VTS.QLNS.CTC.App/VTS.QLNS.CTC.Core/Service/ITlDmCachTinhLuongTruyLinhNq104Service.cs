using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmCachTinhLuongTruyLinhNq104Service
    {
        IEnumerable<TlDmCachTinhLuongTruyLinhNq104> FindAll(Expression<Func<TlDmCachTinhLuongTruyLinhNq104, bool>> predicate);
        TlDmCachTinhLuongTruyLinhNq104 Find(Guid id);
        TlDmCachTinhLuongTruyLinhNq104 FindByMaCot(string maCot);
        int Update(TlDmCachTinhLuongTruyLinhNq104 entity);
        int Add(TlDmCachTinhLuongTruyLinhNq104 entity);
        int Delete(Guid id);
    }
}
