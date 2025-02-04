using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlQtChungTuChiTietGiaiThichNq104Repository : IRepository<TlQtChungTuChiTietGiaiThichNq104>
    {
        TlQtChungTuChiTietGiaiThichNq104 FindByCondition(string thang, int nam, string maDonVi);
        TlQtChungTuChiTietGiaiThichNq104 FindByChungTuId(Guid chungTuId);
    }
}
