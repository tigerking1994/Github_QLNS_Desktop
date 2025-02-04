using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhQtQuyetToanNienDoChiTietService
    {
        void AddOrUpdate(IEnumerable<NhQtQuyetToanNienDoChiTiet> entities);
        void DeleteQTNDChiTiet(NhQtQuyetToanNienDo entity);
        IEnumerable<NhQtQuyetToanNienDoChiTiet> FindByQuyetToanNienDoId(Guid quyetToanNienDoId);
        NhQtQuyetToanNienDoChiTiet FetchData(Guid quyetToanNienDoId, Guid hopDongId);
        //List<NhQtQuyetToanNienDoChiTiet> getListDetailChiTiet(Guid quyetToanNienDoId, Guid? donViId, int? nam);
        List<NhQTQuyetToanNienDoChiTietQuery> getListQTNDDetailChiTiet(Guid quyetToanNienDoId, Guid? donViId, int? nam, int? donViTinhUSD, int? donViTinhVND);

        bool checkListAddOrUpdateQTNDChiTiet(Guid quyetToanNienDoId, Guid? donViId, int? nam, int? donViTinhUSD, int? donViTinhVND);

    }
}
