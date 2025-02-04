using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlQsChungTuChiTietNq104Repository : IRepository<TlQsChungTuChiTietNq104>
    {
        int DeleteParent(Guid ChungTuId);
        IEnumerable<TlQsChungTuChiTietNq104> FindQuyetToanQuanSo(string idDonVi, string thang, int nam, string thangTruoc, int namTruoc);
    }
}
