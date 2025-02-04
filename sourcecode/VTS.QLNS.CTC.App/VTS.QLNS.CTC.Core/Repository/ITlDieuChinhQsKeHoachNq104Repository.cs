using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlDieuChinhQsKeHoachNq104Repository : IRepository<TlDieuChinhQsKeHoachNq104>
    {
        int DeleteByNam(int nam);
        IEnumerable<TlRptQuanSoKeHoachNq104Query> FindData(int nam, string maDonVi);
    }
}
