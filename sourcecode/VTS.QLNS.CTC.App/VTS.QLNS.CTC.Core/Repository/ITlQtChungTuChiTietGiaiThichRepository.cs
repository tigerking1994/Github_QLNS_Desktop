using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlQtChungTuChiTietGiaiThichRepository : IRepository<TlQtChungTuChiTietGiaiThich>
    {
        TlQtChungTuChiTietGiaiThich FindByCondition(string thang, int nam, string maDonVi);
        TlQtChungTuChiTietGiaiThich FindByChungTuId(Guid chungTuId);
        IEnumerable<TlQtChungTuChiTietGiaiThich> FindListByChungTuId(Guid chungTuId);
    }
}
