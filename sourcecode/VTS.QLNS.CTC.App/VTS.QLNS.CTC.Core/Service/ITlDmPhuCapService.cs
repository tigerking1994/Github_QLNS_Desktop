using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmPhuCapService
    {
        TlDmPhuCap FindById(string id);
        TlDmPhuCap FindByMaPhuCap(string maPhuCap);
        IEnumerable<TlDmPhuCap> FindAll();        
        IEnumerable<TlDmPhuCap> FindByCondition();
        IEnumerable<TlDmPhuCap> FindByCondition(Expression<Func<TlDmPhuCap, bool>> predicate);
        IEnumerable<TlDmPhuCap> FindByHeThong();
        IEnumerable<TlDmPhuCap> FindAll(Expression<Func<TlDmPhuCap, bool>> predicate);
        IEnumerable<TlDmPhuCap> FindHasDataBangLuong(int nam, int thang, string maCachTl);
        IEnumerable<TlDmPhuCap> GetDmPhuCapInDcTapTheCanBo();
        IEnumerable<TlPhuCapQuery> FindAllPhuCapVaCheDoBHXH();
        IEnumerable<TlDmPhuCap> FindByIdThuNopBhxh(Guid id);

    }
}
