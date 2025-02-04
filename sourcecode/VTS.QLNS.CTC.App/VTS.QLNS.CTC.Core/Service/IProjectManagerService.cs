using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using System.Linq.Expressions;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IProjectManagerService
    {
        IEnumerable<VdtDaDuAn> FindAll(Expression<Func<VdtDaDuAn, bool>> predicate);
        IEnumerable<ProjectManagerQuery> FindByCondition();
        IEnumerable<VdtDmLoaiCongTrinh> GetAllDMLoaiCongTrinh();
        bool CheckExitsQdDauTuByDuAnId(Guid duAnId);
        bool CheckExitsChuTruongDauTuByDuAnId(Guid duAnId);
        int Delete(Guid id);
        IEnumerable<VdtDmPhanCapDuAn> GetAllPhanCapDuAn();
        //IEnumerable<VdtDmLoaiCongTrinh> GetAllLoaiCongTrinh();
        VdtDaDuAn Add(VdtDaDuAn entity);
        void AddRange(IEnumerable<VdtDaDuAn> data);
        int Update(VdtDaDuAn entity);
        void InsertDuAnHangMuc(IEnumerable<VdtDaDuAnHangMuc> datas);
        bool CheckDuplicateMaDuAn(string maDuAn, Guid duAnId);
        //IEnumerable<ProjectManagerDetailQuery> FindListProjectDetail(Guid duAnId, Guid quyetDinhId);
        VdtDaDuAn FindById(Guid id);
        VdtDmNhomDuAn FindNhomDuAnById(Guid nhomDuAnId);
        IEnumerable<ReportTongHopThongTinDuAnQuery> FindByAggregateProjectInformationReport(AggregateProjectInformationReportCriteria condition);
        ProjectManagerQuery FindDuAnById(Guid duAnId);
        void InsertDmDuAnChiPhi(List<VdtDmDuAnChiPhi> datas);
        void UpdateDmDuAnChiPhi(IEnumerable<VdtDmDuAnChiPhi> datas);
        IEnumerable<ReportChiTieuCapPhatDuAnQuery> FindByConditionProjectAllocationReport(ReportChiTieuCapPhatDuAnCriteria condition);
        IEnumerable<ReportChiTieuCapPhatDuAnQuery> FindListParentByConditionProjectAllocationReport(ReportChiTieuCapPhatDuAnCriteria condition);
        IEnumerable<ReportDuToanNSQPNamQuery> FindByConditionDuToanNSQPNamReport(DuToanNSQPNamReportSearch condition);
        IEnumerable<ReportDuToanNSQPNamQuery> FindListParentDuToanNSQPNamReport(DuToanNSQPNamReportSearch condition);
        //IEnumerable<ChiPhiHangMucQuery> GetDetailData(Guid iIdParent);
        //IEnumerable<ChiPhiHangMucQuery> GetChiPhiByDuAnId(Guid iIdDuAn);
        //IEnumerable<ChiPhiHangMucQuery> GetDetailByDuAnId(Guid iIdDuAn);
        //void InsertDuAnHangMuc(IEnumerable<VdtDaDuAnHangMuc> datas);
        //void UpdateDuAnHangMuc(IEnumerable<VdtDaDuAnHangMuc> datas);
        //void DeleteDuAnHangMuc(IEnumerable<VdtDaDuAnHangMuc> datas);
        //void InsertDuAnNguonVon(IEnumerable<VdtDaNguonVon> datas);
        //void UpdateDuAnNguonVon(Guid iIdDuAn, IEnumerable<VdtDaNguonVon> datas);
        //void DeleteDuAnNguonVon(Guid iIdDuAn, IEnumerable<VdtDaNguonVon> datas);
        //void InsertDmDuAnChiPhi(IEnumerable<VdtDmDuAnChiPhi> datas);
        //void UpdateDmDuAnChiPhi(IEnumerable<VdtDmDuAnChiPhi> datas);
        //void DeleteDmDuAnChiPhi(IEnumerable<VdtDmDuAnChiPhi> datas);
        //void InsertDuAnChiPhi(IEnumerable<VdtDaDuAnChiPhi> datas);
        //void UpdateDuAnChiPhi(Guid iIdDuAn, IEnumerable<VdtDaDuAnChiPhi> datas);
        //void DeleteDuAnChiPhi(Guid iIdDuAn, IEnumerable<VdtDaDuAnChiPhi> datas);
        VdtDaDuAn InsertAutoCode(VdtDaDuAn entity);
        void UpdateDataDuAn(VdtDaDuAn entity);
        IEnumerable<VDTDaNguonVonQuery> FindListNguonVonByDuan(Guid duAnId);
        IEnumerable<VdtDaHangMucQuery> FindListHangMucByDuan(Guid duAnId);
        int AddRangeDuAnNguonVon(IEnumerable<VdtDaNguonVon> entities);
        VdtDaNguonVon FindDuAnNguonVonById(params object[] keyValues);
        int UpdateDuAnNguonVon(VdtDaNguonVon entity);
        int DeleteDuAnNguonVon(Guid id);
        IEnumerable<VdtDaDuAn> FindListDuAnByDonViKHLCNT(string donviId);
        IEnumerable<VdtDaDuAn> FindListDuAnByDonViChuTruongDauTu(string donviId, Guid iIDKhlcNhaThau);
        IEnumerable<VdtDaDuAn> FindListDuAnByDonViQDDauTu(string donviId);
        IEnumerable<VdtDaDuAn> FindDuAnByLoaiChungTu(string sLoaiChungTu, string sMaDonViThucHienDuAn, bool isAdd);
        bool CheckDuAnQuyetToanHoanThanh(Guid duAnId);
        IEnumerable<VdtKhlcNhaThauCanCuQuery> FindChungTuInKeHoachLuaChonNhaThauScreen(Guid iIdKhLcNhaThau, Guid iIdDuAn, string sLoaiChungTu);
        IEnumerable<VdtKhlcNhaThauCanCuQuery> FindChungTuInKeHoachLuaChonNhaThauDieuChinh(Guid iIdKhLcNhaThau, string sLoaiChungTu);
    }
}
