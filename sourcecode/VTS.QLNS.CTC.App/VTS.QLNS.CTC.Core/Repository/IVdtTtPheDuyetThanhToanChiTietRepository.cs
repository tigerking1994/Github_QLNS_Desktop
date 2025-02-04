using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtTtPheDuyetThanhToanChiTietRepository : IRepository<VdtTtPheDuyetThanhToanChiTiet>
    {
        void DeletePheDuyetThanhToanChiTietByParentId(Guid iIdParent);
        List<PheDuyetThanhToanChiTietQuery> GetAllVdtTTPheDuyetThanhToanChiTiet(Guid iIdParentId);
        List<VdtTtPheDuyetThanhToanChiTietQuery> GetAllPheDuyetThanhToanChiTiet(Guid iIdParentId);
        List<VdtTtPheDuyetThanhToanChiTiet> FindByDeNghiThanhToanId(Guid deNghiThanhToanId);
    }
}
