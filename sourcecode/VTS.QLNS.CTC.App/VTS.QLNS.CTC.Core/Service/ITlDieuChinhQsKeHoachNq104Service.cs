using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDieuChinhQsKeHoachNq104Service
    {
        IEnumerable<TlDieuChinhQsKeHoachNq104> FindAll();
        int AddRange(List<TlDieuChinhQsKeHoachNq104> entities);
        int UpdateRange(List<TlDieuChinhQsKeHoachNq104> entities);
        int Delete(Guid id);
        TlDieuChinhQsKeHoachNq104 FindByKeHoach(Expression<Func<TlDieuChinhQsKeHoachNq104, bool>> predicate);
        int DeleteByNam(int nam);
        IEnumerable<TlRptQuanSoKeHoachNq104Query> FindData(int nam, string maDonVi);
    }
}
