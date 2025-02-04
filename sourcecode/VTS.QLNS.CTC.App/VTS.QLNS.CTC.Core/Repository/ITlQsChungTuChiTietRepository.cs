using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlQsChungTuChiTietRepository : IRepository<TlQsChungTuChiTiet>
    {
        int DeleteParent(Guid ChungTuId);
        IEnumerable<TlQsChungTuChiTiet> FindQuyetToanQuanSo(string idDonVi, string thang, int nam, string thangTruoc, int namTruoc);
    }
}
