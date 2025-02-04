using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlCanBoPhuCapRepository : IRepository<TlCanBoPhuCap>
    {
        int DeleteByMaCanBo(string maCanBo);
        IEnumerable<TlCanBoPhuCap> FindByMaCbo(string maCanbo);
        IEnumerable<TlPhuCapQuery> FindCanBoPhuCap(string maCanBo);
        IEnumerable<TlCanBoPhuCap> FindByLstMaCanBo(List<string> maCanBo);
        int DeleteCanBo(string maCanBo);
        TlCanBoPhuCap FindByMaCanBoAndMaPhuCap(string maCanBo, string maPhuCap);
        IEnumerable<TLCanBoPhuCapQuery> Copy(string maCanBo, int fromYear, int fromMonth, int toYear, int toMonth, bool isCopyValue);
        IEnumerable<TLCanBoPhuCapQuery> FindCanBoPhuCapByMaCanBo(string maCanBo);
    }
}
