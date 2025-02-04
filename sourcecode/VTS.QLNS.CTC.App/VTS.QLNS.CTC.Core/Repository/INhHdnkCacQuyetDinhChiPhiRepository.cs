using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhHdnkCacQuyetDinhChiPhiRepository : IRepository<NhHdnkCacQuyetDinhChiPhi>
    {
        IEnumerable<NhHdnkCacQuyetDinhChiPhi> FindByIdQuyetDinh(Guid? idQuyetDinh);
        IEnumerable<NhHdnkCacQuyetDinhChiPhiQuery> FindByIdKhttNhiemVuChi(Guid IdKhttNhiemVuChi);
        IEnumerable<NhHdnkCacQuyetDinhChiPhiDmChiPhiQuery> FindByIdQuyetDinhGoiThau(Guid? idQuyetDinh);
    }
}
