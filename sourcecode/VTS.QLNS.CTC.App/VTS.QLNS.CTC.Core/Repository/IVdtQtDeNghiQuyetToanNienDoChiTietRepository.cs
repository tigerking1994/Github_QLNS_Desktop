using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtQtDeNghiQuyetToanNienDoChiTietRepository : IRepository<VdtQtDeNghiQuyetToanNienDoChiTiet>
    {
        List<VdtQtDenghiQuyetToanNienDoChiTietQuery> GetAllDuAnByQuyetToanNienDo(string iIdDonvi, int iNguonVon, Guid iIdLoaiNguonVon, DateTime dNgayLap, int iNamKeHoach);
        List<VdtQtDenghiQuyetToanNienDoChiTietQuery> GetQuyetToanNienDoChiTietByParentid(Guid iIdQuyetToanNienDoId);
        bool DeleteByQuyetToanNienDoId(Guid iIdQuyetToanNienDo);
    }
}
