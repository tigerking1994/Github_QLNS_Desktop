using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhQtQuyetToanNienDoChiTietRepository : IRepository<NhQtQuyetToanNienDoChiTiet>
    {
        IEnumerable<NhQtQuyetToanNienDoChiTiet> FetchData(Guid quyetToanNienDoId, Guid hopDongId);
        //IEnumerable<NhQtQuyetToanNienDoChiTiet> GetDetailQuyetToanNienDoDetail(Guid quyetToanNienDoId, int? donViTinh);
        //IEnumerable<NhQtQuyetToanNienDoChiTiet> GetDetailQuyetToanNienDoCreate(Guid? donViId, int? nam, int? donViTinh);
        IEnumerable<NhQTQuyetToanNienDoChiTietQuery> GetDetailQuyetToanNienDoDetail(Guid quyetToanNienDoId, int? donViTinhUSD, int? donViTinhVND);
        IEnumerable<NhQTQuyetToanNienDoChiTietQuery> GetDetailQuyetToanNienDoCreate(Guid? donViId, int? nam, int? donViTinhUSD, int? donViTinhVND);
    }
}
