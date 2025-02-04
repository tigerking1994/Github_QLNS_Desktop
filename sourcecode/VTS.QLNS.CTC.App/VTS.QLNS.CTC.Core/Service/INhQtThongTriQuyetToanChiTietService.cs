using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhQtThongTriQuyetToanChiTietService
    {
        IEnumerable<NhQtThongTriQuyetToanChiTietQuery> GetCreateThongTriChiTiet(Guid iID_TTQT, int iNamThongTri, Guid iID_DonViID, Guid iID_KHTT_NhiemVuChiID);
        void SaveThongTriChiTiet(List<NhQtThongTriQuyetToanChiTiet> input);
        void DeleteAllThongTriChiTietByTTId(Guid Id);
        IEnumerable<NhQtThongTriQuyetToanChiTietQuery> GetThongTriChiTietByTTQTId(Guid id);
    }
}
