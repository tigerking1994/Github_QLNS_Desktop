using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlQtChungTuChiTietGiaiThichNq104Service
    {
        TlQtChungTuChiTietGiaiThichNq104 FindByCondition(string thang, int nam, string maDonVi);
        TlQtChungTuChiTietGiaiThichNq104 FindByChungTuId(Guid chungTuId);
        int Add(TlQtChungTuChiTietGiaiThichNq104 entity);
        int Update(TlQtChungTuChiTietGiaiThichNq104 entity);
    }
}
