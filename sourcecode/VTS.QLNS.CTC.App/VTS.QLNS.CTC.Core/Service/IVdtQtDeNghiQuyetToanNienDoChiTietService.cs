using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtQtDeNghiQuyetToanNienDoChiTietService
    {
        List<VdtQtDenghiQuyetToanNienDoChiTietQuery> GetAllDuAnByQuyetToanNienDo(string iIdDonvi, int iNguonVon, Guid iIdLoaiNguonVon, DateTime dNgayLap, int iNamKeHoach);
        List<VdtQtDenghiQuyetToanNienDoChiTietQuery> GetQuyetToanNienDoChiTietByParentid(Guid iIdQuyetToanNienDoId);
        bool Insert(Guid iIdQuyetToanNienDo, List<VdtQtDeNghiQuyetToanNienDoChiTiet> lstDataInsert);
    }
}
