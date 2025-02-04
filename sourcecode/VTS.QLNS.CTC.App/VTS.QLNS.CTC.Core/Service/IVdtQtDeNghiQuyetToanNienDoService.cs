using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtQtDeNghiQuyetToanNienDoService
    {
        List<VdtQtDenghiQuyetToanNienDoQuery> GetDeNghiQuyetToanNienDoIndex();
        bool Insert(VdtQtDeNghiQuyetToanNienDo data, string sUserLogin);
        bool Update(VdtQtDeNghiQuyetToanNienDo data, string sUserLogin);
        bool DeleteDeNghiQuyetToan(VdtQtDeNghiQuyetToanNienDo data, string sUserLogin);
        bool CheckExistDeNghiQuyetToanNienDo(Guid iIdDonVi, int iNamKeHoach, int iNguonVon, Guid iIdLoaiNguonvon);
    }
}
