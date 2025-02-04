using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Query.Shared;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhQtThongTriQuyetToanService
    {
        IEnumerable<NhQtThongTriQuyetToanQuery> GetAll();
        IEnumerable<LookupQuery<Guid, string>> GetLookupNhiemVuChi();
        IEnumerable<LookupQuery<Guid, string>> GetLookupNhiemVuChiByDonVi(Guid iID_DonViID);
        Guid SaveAndGetIdThongTriQuyetToan(NhQtThongTriQuyetToan input);
        void DeleteThongTriById(Guid Id);
        NhQtThongTriQuyetToanQuery GetThongTriById(Guid id);
        NhQtThongTriQuyetToan GetThongTriUpdateById(Guid id);
        void UpdateThongTriQuyetToan(NhQtThongTriQuyetToan input);
    }
}
