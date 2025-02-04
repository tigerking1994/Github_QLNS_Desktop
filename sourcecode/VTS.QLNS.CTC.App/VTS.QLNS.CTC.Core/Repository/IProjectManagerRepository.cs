using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IProjectManagerRepository : IRepository<VdtDaDuAn>
    {
        IEnumerable<ProjectManagerQuery> FindByCondition();
        IEnumerable<VdtDmLoaiCongTrinh> GetAllDMLoaiCongTrinh();
        bool CheckExitsQdDauTuByDuAnId(Guid duAnId);
        bool CheckExitsChuTruongDauTuByDuAnId(Guid duAnId);
        IEnumerable<VdtDmPhanCapDuAn> GetAllPhanCapDuAn();
        bool CheckDuplicateMaDuAn(string maDuAn, Guid duAnId);
        IEnumerable<ProjectManagerDetailQuery> FindListProjectDetail(Guid duAnId, Guid quyetDinhId);
        IEnumerable<ReportTongHopThongTinDuAnQuery> FindByAggregateProjectInformationReport(AggregateProjectInformationReportCriteria condition);
        ProjectManagerQuery FindDuAnById(Guid duAnId);
        IEnumerable<ReportChiTieuCapPhatDuAnQuery> FindByConditionProjectAllocationReport(ReportChiTieuCapPhatDuAnCriteria condition);
        IEnumerable<ReportChiTieuCapPhatDuAnQuery> FindListParentByConditionProjectAllocationReport(ReportChiTieuCapPhatDuAnCriteria condition);
        IEnumerable<ReportDuToanNSQPNamQuery> FindByConditionDuToanNSQPNamReport(DuToanNSQPNamReportSearch condition);
        IEnumerable<ReportDuToanNSQPNamQuery> FindListParentDuToanNSQPNamReport(DuToanNSQPNamReportSearch condition);
        IEnumerable<ChiPhiHangMucQuery> GetDetailData(Guid iIdParent);
        IEnumerable<ChiPhiHangMucQuery> GetChiPhiByDuAnId(Guid iIdDuAn);
        IEnumerable<ChiPhiHangMucQuery> GetDetailByDuAnId(Guid iIdDuAn);
        void InsertDuAnHangMuc(IEnumerable<VdtDaDuAnHangMuc> datas);
        void UpdateDuAnHangMuc(IEnumerable<VdtDaDuAnHangMuc> datas);
        void DeleteDuAnHangMuc(IEnumerable<VdtDaDuAnHangMuc> datas);
        void InsertDuAnNguonVon(IEnumerable<VdtDaNguonVon> datas);
        void UpdateDuAnNguonVon(Guid iIdDuAn, IEnumerable<VdtDaNguonVon> datas);
        void DeleteDuAnNguonVon(Guid iIdDuAn, IEnumerable<VdtDaNguonVon> datas);
        void InsertDmDuAnChiPhi(IEnumerable<VdtDmDuAnChiPhi> datas);
        void UpdateDmDuAnChiPhi(IEnumerable<VdtDmDuAnChiPhi> datas);
        void DeleteDmDuAnChiPhi(IEnumerable<VdtDmDuAnChiPhi> datas);
        void InsertDuAnChiPhi(IEnumerable<VdtDaDuAnChiPhi> datas);
        void UpdateDuAnChiPhi(Guid iIdDuAn, IEnumerable<VdtDaDuAnChiPhi> datas);
        void DeleteDuAnChiPhi(Guid iIdDuAn, IEnumerable<VdtDaDuAnChiPhi> datas);
        VdtDaDuAn InsertAutoCode(VdtDaDuAn entity);
        void UpdateDataDuAn(VdtDaDuAn entity);
        IEnumerable<VDTDaNguonVonQuery> FindListNguonVonByDuan(Guid duAnId);
        IEnumerable<VdtDaHangMucQuery> FindListHangMucByDuan(Guid duAnId);
        IEnumerable<VdtDaDuAn> FindListDuAnByDonViKHLCNT(string donviId);
        IEnumerable<VdtDaDuAn> FindListDuAnByDonViChuTruongDauTu(string donviId, Guid iIDKhlcNhaThau);
        IEnumerable<VdtDaDuAn> FindListDuAnByDonViQDDauTu(string donviId);
        IEnumerable<VdtDaDuAn> FindDuAnByLoaiChungTu(string sLoaiChungTu, string sMaDonViThucHienDuAn, bool isAdd);
        bool CheckDuAnQuyetToanHoanThanh(Guid duAnId);
        IEnumerable<VdtKhlcNhaThauCanCuQuery> FindChungTuInKeHoachLuaChonNhaThauScreen(Guid iIdKhLcNhaThau, Guid iIdDuAn, string sLoaiChungTu);
        IEnumerable<VdtKhlcNhaThauCanCuQuery> FindChungTuInKeHoachLuaChonNhaThauDieuChinh(Guid iIdKhLcNhaThau, string sLoaiChungTu);
    }
}
