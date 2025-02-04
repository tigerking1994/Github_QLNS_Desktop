using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDmNhiemVuChiService
    {
        IEnumerable<NhDmNhiemVuChi> FindAll();
        IEnumerable<NhDmNhiemVuChiQuery> FindAllFillter(Guid idDonVi);
        IEnumerable<NhDmNhiemVuChi> FindByCondition(Expression<Func<NhDmNhiemVuChi, bool>> predicate);
        IEnumerable<NhDmNhiemVuChiQuery> FindByKhTongTheIdAndDonViId(Guid idNhKhTongThe, Guid idDonVi);
        IEnumerable<NhDmNhiemVuChiQuery> FindByDonViId(Guid idDonVi);
        IEnumerable<NhDmNhiemVuChiQuery> FindNhiemVuChiDuToanByDonViId(Guid idDonVi);
        IEnumerable<NhDmNhiemVuChiQuery> FindNhiemVuChiDuToanByDuToanId(Guid idDuToan);

        
        int Delete(Guid id);
    }
}