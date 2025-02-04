using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtTtDeNghiThanhToanChiTietRepository : IRepository<VdtTtDeNghiThanhToanChiTiet>
    {
        IEnumerable<VdtTtDeNghiThanhToanChiTietQuery> GetDuAnByIdThanhToan(Guid iIdThanhToanId);
        HopDongInfoQuery GetHopDongInfo(Guid iIdHopDong, DateTime dNgayPheDuyet, int iIdNguonVonId);
        bool Insert(List<VdtTtDeNghiThanhToanChiTiet> data);
        bool DeleteByThanhToanId(Guid iIdThanhToan);
    }
}
