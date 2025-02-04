using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtThongTriRepository : IRepository<VdtThongTri>
    {
        IEnumerable<VdtThongTriQuery> GetVdtThongTriIndex(Guid iIdLoaiThongTri, int openFromPheDuyetThanhToan);
        IEnumerable<VdtDmLoaiThongTri> GetAllDmLoaiThongTri();
        IEnumerable<VdtDmKieuThongTri> GetAllKieuThongTri();
    }
}
