using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using static Dapper.SqlMapper;
using System.Linq.Expressions;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class ProjectManagerService : IProjectManagerService
    {
        private readonly IProjectManagerRepository _projectManagerRepository;
        private readonly IVdtDaNhomDuAnRepository _nhomDuAnRepository;
        private readonly IVdtDaNguonVonRepository _vdtDaNguonVonRepository;

        public ProjectManagerService(
            IProjectManagerRepository projectManagerRepository,
            IVdtDaNhomDuAnRepository nhomDuAnRepository,
            IVdtDaNguonVonRepository vdtDaNguonVonRepository)
        {
            _projectManagerRepository = projectManagerRepository;
            _nhomDuAnRepository = nhomDuAnRepository;
            _vdtDaNguonVonRepository = vdtDaNguonVonRepository;
        }

        #region NamNV
        public IEnumerable<VdtDaDuAn> FindAll(Expression<Func<VdtDaDuAn, bool>> predicate)
        {
            return _projectManagerRepository.FindAll(predicate);
        }

        public IEnumerable<ProjectManagerQuery> FindByCondition()
        {
            return _projectManagerRepository.FindByCondition();
        }

        public IEnumerable<VdtDmLoaiCongTrinh> GetAllDMLoaiCongTrinh()
        {
            return _projectManagerRepository.GetAllDMLoaiCongTrinh();
        }

        public bool CheckExitsChuTruongDauTuByDuAnId(Guid duAnId)
        {
            return _projectManagerRepository.CheckExitsChuTruongDauTuByDuAnId(duAnId);
        }

        public bool CheckExitsQdDauTuByDuAnId(Guid duAnId)
        {
            return _projectManagerRepository.CheckExitsQdDauTuByDuAnId(duAnId);
        }

        public int Delete(Guid id)
        {
            VdtDaDuAn entity = _projectManagerRepository.Find(id);
            return _projectManagerRepository.Delete(entity);
        }

        public IEnumerable<VdtDmPhanCapDuAn> GetAllPhanCapDuAn()
        {
            return _projectManagerRepository.GetAllPhanCapDuAn();
        }

        //public IEnumerable<VdtDmLoaiCongTrinh> GetAllLoaiCongTrinh()
        //{
        //    return _projectManagerRepository.GetAllDMLoaiCongTrinh();
        //}

        public VdtDaDuAn Add(VdtDaDuAn entity)
        {

            _projectManagerRepository.Add(entity);
            return entity;
        }

        public void AddRange(IEnumerable<VdtDaDuAn> data)
        {
            _projectManagerRepository.AddRange(data);
        }

        public VdtDaDuAn InsertAutoCode(VdtDaDuAn entity)
        {
            _projectManagerRepository.InsertAutoCode(entity);
            return entity;
        }

        public void UpdateDataDuAn(VdtDaDuAn entity)
        {
            _projectManagerRepository.UpdateDataDuAn(entity);
        }

        public int Update(VdtDaDuAn entity)
        {
            return _projectManagerRepository.Update(entity);
        }

        public bool CheckDuplicateMaDuAn(string maDuAn, Guid duAnId)
        {
            return _projectManagerRepository.CheckDuplicateMaDuAn(maDuAn, duAnId);
        }

        //public IEnumerable<ProjectManagerDetailQuery> FindListProjectDetail(Guid duAnId, Guid quyetDinhId)
        //{
        //    return _projectManagerRepository.FindListProjectDetail(duAnId, quyetDinhId);
        //}

        public VdtDaDuAn FindById(Guid id)
        {
            return _projectManagerRepository.Find(id);
        }

        public IEnumerable<ReportTongHopThongTinDuAnQuery> FindByAggregateProjectInformationReport(AggregateProjectInformationReportCriteria condition)
        {
            return _projectManagerRepository.FindByAggregateProjectInformationReport(condition);
        }

        public ProjectManagerQuery FindDuAnById(Guid duAnId)
        {
            return _projectManagerRepository.FindDuAnById(duAnId);
        }

        public void InsertDmDuAnChiPhi(List<VdtDmDuAnChiPhi> datas)
        {
            _projectManagerRepository.InsertDmDuAnChiPhi(datas);
        }

        public void UpdateDmDuAnChiPhi(IEnumerable<VdtDmDuAnChiPhi> datas)
        {
            _projectManagerRepository.UpdateDmDuAnChiPhi(datas);
        }

        public IEnumerable<ReportChiTieuCapPhatDuAnQuery> FindByConditionProjectAllocationReport(ReportChiTieuCapPhatDuAnCriteria condition)
        {
            return _projectManagerRepository.FindByConditionProjectAllocationReport(condition);
        }

        public IEnumerable<ReportChiTieuCapPhatDuAnQuery> FindListParentByConditionProjectAllocationReport(ReportChiTieuCapPhatDuAnCriteria condition)
        {
            return _projectManagerRepository.FindListParentByConditionProjectAllocationReport(condition);
        }

        public IEnumerable<ReportDuToanNSQPNamQuery> FindByConditionDuToanNSQPNamReport(DuToanNSQPNamReportSearch condition)
        {
            return _projectManagerRepository.FindByConditionDuToanNSQPNamReport(condition);
        }

        public IEnumerable<ReportDuToanNSQPNamQuery> FindListParentDuToanNSQPNamReport(DuToanNSQPNamReportSearch condition)
        {
            return _projectManagerRepository.FindListParentDuToanNSQPNamReport(condition);
        }

        public VdtDmNhomDuAn FindNhomDuAnById(Guid nhomDuAnId)
        {
            return _nhomDuAnRepository.Find(nhomDuAnId);
        }
        #endregion

        //public IEnumerable<ChiPhiHangMucQuery> GetDetailData(Guid iIdParent)
        //{
        //    return _projectManagerRepository.GetDetailData(iIdParent);
        //}

        //public IEnumerable<ChiPhiHangMucQuery> GetChiPhiByDuAnId(Guid iIdDuAn)
        //{
        //    return _projectManagerRepository.GetChiPhiByDuAnId(iIdDuAn);
        //}

        public void InsertDuAnHangMuc(IEnumerable<VdtDaDuAnHangMuc> datas)
        {
            _projectManagerRepository.InsertDuAnHangMuc(datas);
        }

        //public void UpdateDuAnHangMuc(IEnumerable<VdtDaDuAnHangMuc> datas)
        //{
        //    _projectManagerRepository.UpdateDuAnHangMuc(datas);
        //}

        //public void DeleteDuAnHangMuc(IEnumerable<VdtDaDuAnHangMuc> datas)
        //{
        //    _projectManagerRepository.DeleteDuAnHangMuc(datas);
        //}

        //public void InsertDuAnNguonVon(IEnumerable<VdtDaNguonVon> datas)
        //{
        //    _projectManagerRepository.InsertDuAnNguonVon(datas);
        //}

        //public void UpdateDuAnNguonVon(Guid iIdDuAn, IEnumerable<VdtDaNguonVon> datas)
        //{
        //    _projectManagerRepository.UpdateDuAnNguonVon(iIdDuAn, datas);
        //}

        //public void DeleteDuAnNguonVon(Guid iIdDuAn, IEnumerable<VdtDaNguonVon> datas)
        //{
        //    _projectManagerRepository.DeleteDuAnNguonVon(iIdDuAn, datas);
        //}

        //public void InsertDmDuAnChiPhi(IEnumerable<VdtDmDuAnChiPhi> datas)
        //{
        //    _projectManagerRepository.InsertDmDuAnChiPhi(datas);
        //}

        //public void UpdateDmDuAnChiPhi(IEnumerable<VdtDmDuAnChiPhi> datas)
        //{
        //    _projectManagerRepository.UpdateDmDuAnChiPhi(datas);
        //}

        //public void DeleteDmDuAnChiPhi(IEnumerable<VdtDmDuAnChiPhi> datas)
        //{
        //    _projectManagerRepository.DeleteDmDuAnChiPhi(datas);
        //}

        //public void InsertDuAnChiPhi(IEnumerable<VdtDaDuAnChiPhi> datas)
        //{
        //    _projectManagerRepository.InsertDuAnChiPhi(datas);
        //}

        //public void UpdateDuAnChiPhi(Guid iIdDuAn, IEnumerable<VdtDaDuAnChiPhi> datas)
        //{
        //    _projectManagerRepository.UpdateDuAnChiPhi(iIdDuAn, datas);
        //}

        //public void DeleteDuAnChiPhi(Guid iIdDuAn, IEnumerable<VdtDaDuAnChiPhi> datas)
        //{
        //    _projectManagerRepository.DeleteDuAnChiPhi(iIdDuAn, datas);
        //}

        //public IEnumerable<ChiPhiHangMucQuery> GetDetailByDuAnId(Guid iIdDuAn)
        //{
        //    return _projectManagerRepository.GetDetailByDuAnId(iIdDuAn);
        //}

        public IEnumerable<VDTDaNguonVonQuery> FindListNguonVonByDuan(Guid duAnId)
        {
            return _projectManagerRepository.FindListNguonVonByDuan(duAnId);
        }

        public IEnumerable<VdtDaHangMucQuery> FindListHangMucByDuan(Guid duAnId)
        {
            return _projectManagerRepository.FindListHangMucByDuan(duAnId);
        }

        public int AddRangeDuAnNguonVon(IEnumerable<VdtDaNguonVon> entities)
        {
            return _vdtDaNguonVonRepository.AddRange(entities);
        }

        public VdtDaNguonVon FindDuAnNguonVonById(params object[] keyValues)
        {
            return _vdtDaNguonVonRepository.Find(keyValues);
        }

        public int UpdateDuAnNguonVon(VdtDaNguonVon entity)
        {
            return _vdtDaNguonVonRepository.Update(entity);
        }

        public int DeleteDuAnNguonVon(Guid id)
        {
            return _vdtDaNguonVonRepository.Delete(id);
        }

        public IEnumerable<VdtDaDuAn> FindListDuAnByDonViKHLCNT(string donviId)
        {
            return _projectManagerRepository.FindListDuAnByDonViKHLCNT(donviId);
        }

        public IEnumerable<VdtDaDuAn> FindListDuAnByDonViChuTruongDauTu(string donviId, Guid iIDKhlcNhaThau)
        {
            return _projectManagerRepository.FindListDuAnByDonViChuTruongDauTu(donviId, iIDKhlcNhaThau);
        }

        public IEnumerable<VdtDaDuAn> FindListDuAnByDonViQDDauTu(string donviId)
        {
            return _projectManagerRepository.FindListDuAnByDonViQDDauTu(donviId);
        }

        public bool CheckDuAnQuyetToanHoanThanh(Guid duAnId)
        {
            return _projectManagerRepository.CheckDuAnQuyetToanHoanThanh(duAnId);
        }

        public IEnumerable<VdtDaDuAn> FindDuAnByLoaiChungTu(string sLoaiChungTu, string sMaDonViThucHienDuAn, bool isAdd)
        {
            return _projectManagerRepository.FindDuAnByLoaiChungTu(sLoaiChungTu, sMaDonViThucHienDuAn, isAdd);
        }

        public IEnumerable<VdtKhlcNhaThauCanCuQuery> FindChungTuInKeHoachLuaChonNhaThauScreen(Guid iIdKhLcNhaThau, Guid iIdDuAn, string sLoaiChungTu)
        {
            return _projectManagerRepository.FindChungTuInKeHoachLuaChonNhaThauScreen(iIdKhLcNhaThau, iIdDuAn, sLoaiChungTu);
        }

        public IEnumerable<VdtKhlcNhaThauCanCuQuery> FindChungTuInKeHoachLuaChonNhaThauDieuChinh(Guid iIdKhLcNhaThau, string sLoaiChungTu)
        {
            return _projectManagerRepository.FindChungTuInKeHoachLuaChonNhaThauDieuChinh(iIdKhLcNhaThau, sLoaiChungTu);
        }
    }
}
