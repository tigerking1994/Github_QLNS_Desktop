using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtQtBcQuyetToanNienDoRepository : IRepository<VdtQtBcQuyetToanNienDo>
    {
        VdtQtBcQuyetToanNienDo FindById(Guid iId);
        List<VdtQtBcQuyetToanNienDoQuery> GetDeNghiQuyetToanNienDoIndex();
        List<VdtQtBcquyetToanNienDoChiTiet1Query> GetDeNghiQuyetToanNienDoDetail(string iIdMaDonVi, int iNamKeHoach, int iIdNguonVon);
        List<BcquyetToanNienDoVonUngChiTietQuery> GetDeNghiQuyetToanNienDoVonUngDetail(string iIdMaDonVi, int iNamKeHoach, int iIdNguonVon);
        List<BcquyetToanNienDoVonUngChiTietQuery> GetQuyetToanNienDoVonUngByParentId(Guid iIdParentId);
        List<VdtQtBcquyetToanNienDoChiTiet1Query> GetQuyetToanNienDoVonNamByParentId(Guid iIdParentId);
        List<TongHopNguonNSDauTuQuery> GetLuyKeQuyetToanNamTruoc(int iLoaiQuyetToan, string iIdMaDonViQuanLy, int iNamKeHoach, int iIdNguonVon);
        bool CheckExistDeNghiQuyetToanNienDo(string iIdMaDonVi, int iNamKeHoach, int iNguonVon);
        List<VdtQtBcQuyetToanNienDo> GetDeNghiQuyetToanNienDoByCondition(int iLoaiThanhToan, string sMaDonVi, int iNguonVon, int iNamKeHoach);
        List<VdtQtBcQuyetToanNienDo> GetBcQuyetToanInThongTriScreen(Guid? iIdThongTri, string iIdMaDonVi, int iNamThongTri, int iIdNguonVon);
    }
}
