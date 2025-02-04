using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDmNhiemVuChiRepository : IRepository<NhDmNhiemVuChi>
    {
        IEnumerable<NhDmNhiemVuChi> FindAllByIdNhKhTongThe(Guid idNhKhTongThe);
        IEnumerable<NhDmNhiemVuChiQuery> FindByKhTongTheIdAndDonViId(Guid khTongTheId, Guid donViId);
        IEnumerable<NhDmNhiemVuChiQuery> FindAllFillter(Guid donViId);
        IEnumerable<NhDmNhiemVuChiQuery> FindByDonViId(Guid donViId);
        IEnumerable<NhDmNhiemVuChiQuery> FindTreeByIds(IEnumerable<Guid> ids);
        IEnumerable<NhDmNhiemVuChiQuery> FindNhiemVuChiDuToanByDonViId(Guid idDonVi);
        IEnumerable<NhDmNhiemVuChiQuery> FindNhiemVuChiDuToanByDuToanId(Guid idDuToan);
    }
}
