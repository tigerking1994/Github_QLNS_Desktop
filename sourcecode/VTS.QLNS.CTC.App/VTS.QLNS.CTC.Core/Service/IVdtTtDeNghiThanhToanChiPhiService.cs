using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtTtDeNghiThanhToanChiPhiService
    {
        VdtTtDeNghiThanhToanChiPhi Find(Guid id);
        void Insert(VdtTtDeNghiThanhToanChiPhi obj);
        void Update(VdtTtDeNghiThanhToanChiPhi obj);
        void Delete(Guid iId);
        IEnumerable<VdtTtDeNghiThanhToanChiPhiIndexQuery> GetDeNghiThanhToanChiPhiIndex();
        void AddDetail(Guid iId, List<VdtTtDeNghiThanhToanChiPhiChiTiet> datas);
        IEnumerable<VdtTtDeNghiThanhToanChiPhiChiTietQuery> GetDeNghiThanhToanChiPhiDetail(Guid iIdDuToanId);
        IEnumerable<VdtTtDeNghiThanhToanChiPhiChiTietQuery> GetDeNghiThanhToanChiPhiDetailById(Guid iIdChungTu, Guid iIdDuToan);
    }
}
