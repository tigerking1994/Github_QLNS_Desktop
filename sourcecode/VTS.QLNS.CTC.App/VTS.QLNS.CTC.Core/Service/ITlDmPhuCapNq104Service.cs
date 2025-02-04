using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmPhuCapNq104Service
    {
        TlDmPhuCapNq104 FindById(string id);
        TlDmPhuCapNq104 FindByMaPhuCap(string maPhuCap);
        IEnumerable<TlDmPhuCapNq104> FindAll();
        IEnumerable<TlDmPhuCapNq104> FindByCondition();
        IEnumerable<TlDmPhuCapNq104> FindByCondition(Expression<Func<TlDmPhuCapNq104, bool>> predicate);
        IEnumerable<TlDmPhuCapNq104> FindByHeThong();
        IEnumerable<TlDmPhuCapNq104> FindAll(Expression<Func<TlDmPhuCapNq104, bool>> predicate);
        IEnumerable<TlDmPhuCapNq104> FindHasDataBangLuong(int nam, int thang, string maCachTl);
        IEnumerable<TlDmPhuCapNq104> GetDmPhuCapInDcTapTheCanBo();
        IEnumerable<TlPhuCapNq104Query> FindAllPhuCapVaCheDoBHXH();
        IEnumerable<TlDmPhuCapNq104> FindByIdThuNopBhxh(Guid id);

    }
}
