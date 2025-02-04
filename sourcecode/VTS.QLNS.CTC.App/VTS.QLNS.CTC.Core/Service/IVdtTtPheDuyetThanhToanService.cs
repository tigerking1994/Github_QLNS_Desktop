using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtTtPheDuyetThanhToanService
    {
        List<VdtTtPheDuyetThanhToanQuery> GetAllPheDuyetThanhToan();
        void DeletePheDuyetThanhToan(Guid iIdData, string sUserLogin);
        void Insert(VdtTtPheDuyetThanhToan item, string userLogin);
        void Update(VdtTtPheDuyetThanhToan item, string userLogin);
        void InsertDetailData(Guid parentId, List<VdtTtPheDuyetThanhToanChiTiet> lstData);
        List<PheDuyetThanhToanChiTietQuery> GetAllVdtTTPheDuyetThanhToanChiTiet(Guid iIdParentId);
        List<VdtTtPheDuyetThanhToanChiTietQuery> GetAllPheDuyetThanhToanChiTiet(Guid iIdParentId);
    }
}
