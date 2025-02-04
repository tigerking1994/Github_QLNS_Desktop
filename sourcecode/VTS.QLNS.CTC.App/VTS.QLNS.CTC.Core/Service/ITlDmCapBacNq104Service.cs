using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmCapBacNq104Service
    {
        int AddOrUpdateRange(IEnumerable<TlDmCapBacNq104> entities);
        IEnumerable<TlDmCapBacNq104> FindAll();
        TlDmCapBacNq104 FindByMaCapBac(string maCapBac);
        TlDmCapBacNq104 FindById(Guid id);
        IEnumerable<TlDmCapBacNq104> FindByNote();
        IEnumerable<TlDmCapBacNq104> FindParent();
        IEnumerable<TlDmCapBacNq104> FindAll(Expression<Func<TlDmCapBacNq104, bool>> predicate);
        void UpdateCanBoPhuCapWhenChangeCapBac(int iThang, int iNam, List<Guid> lstIdCapBac, bool bIsDelete, string sMaPhuCapChange);
        IEnumerable<RptGiayGTTaiChinhLoaiNhomQuery> FindByTenLoaiAndTenNhom(int v1, int v2, string maCanBo,string maDonVi);
    }
}
