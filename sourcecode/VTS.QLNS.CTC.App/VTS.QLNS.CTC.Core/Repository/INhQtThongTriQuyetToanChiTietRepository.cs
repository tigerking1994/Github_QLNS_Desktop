using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhQtThongTriQuyetToanChiTietRepository : IRepository<NhQtThongTriQuyetToanChiTiet>
    {
        IEnumerable<NhQtThongTriQuyetToanChiTietQuery> GetCreateThongTriChiTiet(int iNamThongTri, Guid iID_DonViID, Guid iID_KHTT_NhiemVuChiID);
        IEnumerable<NhQtThongTriQuyetToanChiTietQuery> GetThongTriChiTietByTTQTId(Guid id);
    }
}
