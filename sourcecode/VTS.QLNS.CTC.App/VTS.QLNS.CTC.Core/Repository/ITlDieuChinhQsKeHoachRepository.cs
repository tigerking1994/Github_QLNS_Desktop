using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlDieuChinhQsKeHoachRepository : IRepository<TlDieuChinhQsKeHoach>
    {
        int DeleteByNam(int nam);
        IEnumerable<TlRptQuanSoKeHoachQuery> FindData(int nam, string maDonVi);
    }
}
