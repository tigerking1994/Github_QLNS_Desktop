using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlCanBoPhuCapNq104Repository : IRepository<TlCanBoPhuCapNq104>
    {
        int DeleteByMaCanBo(string maCanBo);
        IEnumerable<TlCanBoPhuCapNq104> FindByMaCbo(string maCanbo);
        IEnumerable<TlPhuCapNq104Query> FindCanBoPhuCap(string maCanBo);
        IEnumerable<TlCanBoPhuCapNq104> FindByLstMaCanBo(List<string> maCanBo);
        int DeleteCanBo(string maCanBo);
        TlCanBoPhuCapNq104 FindByMaCanBoAndMaPhuCap(string maCanBo, string maPhuCap);
        IEnumerable<TLCanBoPhuCapNq104Query> Copy(string maCanBo, int fromYear, int fromMonth, int toYear, int toMonth, bool isCopyValue);
        IEnumerable<TlCanBoPhuCapNq104> FindDataCanBoPhuCap(string maCanbo);
    }
}
