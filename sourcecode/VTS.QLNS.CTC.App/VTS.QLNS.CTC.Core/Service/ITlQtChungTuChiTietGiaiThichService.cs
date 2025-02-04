using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlQtChungTuChiTietGiaiThichService
    {
        TlQtChungTuChiTietGiaiThich FindByCondition(string thang, int nam, string maDonVi);
        TlQtChungTuChiTietGiaiThich FindByChungTuId(Guid chungTuId);
        int Add(TlQtChungTuChiTietGiaiThich entity);
        int Update(TlQtChungTuChiTietGiaiThich entity);
        int RemoveRange(IEnumerable<TlQtChungTuChiTietGiaiThich> chiTiets);
        IEnumerable<TlQtChungTuChiTietGiaiThich> FindListByChungTuId(Guid chungTuId);
    }
}
