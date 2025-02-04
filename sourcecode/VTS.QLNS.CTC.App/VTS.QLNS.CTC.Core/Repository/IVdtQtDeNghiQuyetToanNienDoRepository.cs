using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtQtDeNghiQuyetToanNienDoRepository : IRepository<VdtQtDeNghiQuyetToanNienDo>
    {
        List<VdtQtDenghiQuyetToanNienDoQuery> GetDeNghiQuyetToanNienDoIndex();
        bool CheckExistDeNghiQuyetToanNienDo(Guid iIdDonVi, int iNamKeHoach, int iNguonVon, Guid iIdLoaiNguonvon);
    }
}
