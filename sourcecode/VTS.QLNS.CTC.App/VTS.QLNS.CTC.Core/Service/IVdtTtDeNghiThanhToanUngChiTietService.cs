using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtTtDeNghiThanhToanUngChiTietService
    {
        IEnumerable<VdtTtDeNghiThanhToanUngChiTietQuery> GetDuAnByDeNghiThanhToanUng(string iIdDonVi, DateTime dNgayLap);
        IEnumerable<VdtTtDeNghiThanhToanUngChiTietQuery> GetDuAnByIdThanhToan(Guid iIdParent, string iIdDonViQuanLyId, DateTime dNgayDeNghi);
        bool Insert(Guid iIdThanhToan, List<VdtTtDeNghiThanhToanUngChiTiet> data);
        VdtTtDeNghiThanhToanUngChiTietQuery GetLuyKeThanhToan(Guid iIdDuAn, Guid? iIdHopDong, string sMaDonViQuanLy, DateTime dNgayPheDuyet);
    }
}
