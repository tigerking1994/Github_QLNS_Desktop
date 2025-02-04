using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDieuChinhQsKeHoachService
    {
        IEnumerable<TlDieuChinhQsKeHoach> FindAll();
        int AddRange(List<TlDieuChinhQsKeHoach> entities);
        int UpdateRange(List<TlDieuChinhQsKeHoach> entities);
        int Delete(Guid id);
        TlDieuChinhQsKeHoach FindByKeHoach(Expression<Func<TlDieuChinhQsKeHoach, bool>> predicate);
        int DeleteByNam(int nam);
        IEnumerable<TlRptQuanSoKeHoachQuery> FindData(int nam, string maDonVi);
    }
}
