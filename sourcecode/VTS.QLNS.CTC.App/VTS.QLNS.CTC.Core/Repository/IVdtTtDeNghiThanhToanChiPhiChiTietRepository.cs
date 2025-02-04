using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtTtDeNghiThanhToanChiPhiChiTietRepository : IRepository<VdtTtDeNghiThanhToanChiPhiChiTiet>
    {
        void DeleteByParentId(Guid iId);
        IEnumerable<VdtTtDeNghiThanhToanChiPhiChiTietQuery> GetDeNghiThanhToanChiPhiDetail(Guid iIdDuToanId);
        IEnumerable<VdtTtDeNghiThanhToanChiPhiChiTietQuery> GetDeNghiThanhToanChiPhiDetailById(Guid iIdChungTu, Guid iIdDuToan);
    }
}
