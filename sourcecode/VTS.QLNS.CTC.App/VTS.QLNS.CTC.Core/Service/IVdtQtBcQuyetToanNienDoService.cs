using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtQtBcQuyetToanNienDoService
    {
        VdtQtBcQuyetToanNienDo Find(Guid iId);
        List<VdtQtBcQuyetToanNienDoQuery> GetDeNghiQuyetToanNienDoIndex();
        List<VdtQtBcQuyetToanNienDo> GetDeNghiQuyetToanNienDoByCondition(int iLoaiThanhToan, string sMaDonVi, int iNguonVon, int iNamKeHoach);
        List<VdtQtBcquyetToanNienDoChiTiet1Query> GetDeNghiQuyetToanNienDoDetail(string iIdMaDonVi, int iNamKeHoach, int iIdNguonVon);
        List<BcquyetToanNienDoVonUngChiTietQuery> GetDeNghiQuyetToanNienDoVonUngDetail(string iIdMaDonVi, int iNamKeHoach, int iIdNguonVon);
        List<BcquyetToanNienDoVonUngChiTietQuery> GetQuyetToanNienDoVonUngByParentId(Guid iIdParentId);
        List<VdtQtBcquyetToanNienDoChiTiet1Query> GetQuyetToanNienDoVonNamByParentId(Guid iIdParentId);
        List<TongHopNguonNSDauTuQuery> GetLuyKeQuyetToanNamTruoc(int iLoaiQuyetToan, string iIdMaDonViQuanLy, int iNamKeHoach, int iIdNguonVon);
        void Insert(VdtQtBcQuyetToanNienDo data, string sUserLogin);
        void Update(VdtQtBcQuyetToanNienDo data, string sUserLogin);
        void DeleteDeNghiQuyetToan(Guid iIDParentID);
        bool CheckExistDeNghiQuyetToanNienDo(string iIdMaDonVi, int iNamKeHoach, int iNguonVon);
        void InsertVdtQtBcquyetToanNienDoChiTiet01(Guid iIDParentID, List<VdtQtBcQuyetToanNienDoChiTiet01> datas);
        IEnumerable<VdtQtBcQuyetToanNienDoPhanTichQuery> GetBaoCaoQuyetToanNienDoPhanTich(string iIdMaDonVi, int iNamKeHoach, int iIdNguonVon);
        IEnumerable<VdtQtBcQuyetToanNienDoPhanTichQuery> GetBaoCaoQuyetToanNienDoPhanTichById(Guid iIdBcQuyetToanNienDo);
        void AddRangePhanTich(Guid iIdParentId, List<VdtQtBcQuyetToanNienDoPhanTich> datas);
        List<VdtQtBcQuyetToanNienDoChiTiet01> GetDenghiQuyetToanNienDoChiTiet01ByParent(Guid iIdParentId);
        List<VdtQtBcQuyetToanNienDoPhanTich> GetBcQuyetToanNienDoPhanTich(Guid iIdParentId);
        List<VdtQtBcQuyetToanNienDo> GetBcQuyetToanInThongTriScreen(Guid? iIdThongTri, string iIdMaDonVi, int iNamThongTri, int iIdNguonVon);
    }
}
