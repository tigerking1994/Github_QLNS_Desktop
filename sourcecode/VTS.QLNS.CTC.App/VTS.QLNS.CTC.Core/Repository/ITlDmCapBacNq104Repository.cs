using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public interface ITlDmCapBacNq104Repository : IRepository<TlDmCapBacNq104>
    {
        TlDmCapBacNq104 FindByMaCapBac(string maCapBac);
        IEnumerable<TlDmCapBacNq104> FindByNote();
        IEnumerable<RptGiayGTTaiChinhLoaiNhomQuery> FindByTenLoaiAndTenNhom(int nam, int thang, string maCanBo, string maDonVi);
        IEnumerable<TlDmCapBacNq104> FindParent();
        void UpdateCanBoPhuCapWhenChangeCapBac(int iThang, int iNam, List<Guid> lstIdCapBac, bool bIsDelete, string sMaPhuCapChange);
    }
}
