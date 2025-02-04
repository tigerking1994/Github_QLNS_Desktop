using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtTtDeNghiThanhToanChiTietService
    {
        IEnumerable<VdtTtDeNghiThanhToanChiTietQuery> GetDuAnByIdThanhToan(Guid iIdThanhToanId);
        HopDongInfoQuery GetHopDongInfo(Guid iIdHopDong, DateTime dNgayPheDuyet, int iIdNguonVonId);
        bool Insert(Guid iIdThanhToan, List<VdtTtDeNghiThanhToanChiTiet> data);
    }
}
