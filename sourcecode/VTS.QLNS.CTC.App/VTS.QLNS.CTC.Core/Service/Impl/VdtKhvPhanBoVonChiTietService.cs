using System;
using System.Collections.Generic;
using System.Data;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Criteria;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtKhvPhanBoVonChiTietService : IVdtKhvPhanBoVonChiTietService
    {
        private readonly IVdtKhvPhanBoVonChiTietRepository _vdtKhvPhanBoVonChiTietRepository;
        private readonly ITongHopNguonNSDauTuRepository _tonghopRepository;

        public VdtKhvPhanBoVonChiTietService(
            IVdtKhvPhanBoVonChiTietRepository vdtKhvPhanBoVonChiTietRepository,
            ITongHopNguonNSDauTuRepository tonghopRepository)
        {
            _vdtKhvPhanBoVonChiTietRepository = vdtKhvPhanBoVonChiTietRepository;
            _tonghopRepository = tonghopRepository;
        }

        public IEnumerable<PhanBoVonChiTietQuery> GetAllDuAnInPhanBoVon(string idPhanBoVonDeXuat, int iNguonVonId)
        {
            return _vdtKhvPhanBoVonChiTietRepository.GetAllDuAnInPhanBoVon(idPhanBoVonDeXuat, iNguonVonId);
        }

        public IEnumerable<PhanBoVonChiTietQuery> GetAllDuAnInPhanBoVonByEdit(string idPhanBoVonDeXuat, int iNguonVonId)
        {
            return _vdtKhvPhanBoVonChiTietRepository.GetAllDuAnInPhanBoVonByEdit(idPhanBoVonDeXuat, iNguonVonId);
        }

        public IEnumerable<MucLucNganSachChungTuThanhToanQuery> GetAllMucLucNganSachByDuAnId(List<TongHopNguonNSDauTuQuery> lstData)
        {
            return _vdtKhvPhanBoVonChiTietRepository.GetAllMucLucNganSachByDuAnId(lstData);
        }

        public List<PhanBoVonChiTietQuery> GetPhanBoVonChiTietByParentId(Guid iIdPhanBoVonChiTiet, DateTime? dNgayQuyetDinh)
        {
            return _vdtKhvPhanBoVonChiTietRepository.GetPhanBoVonChiTietByParentId(iIdPhanBoVonChiTiet, dNgayQuyetDinh);
        }

        public DuAnInfoQuery GetVonDaBoTriByDuAnIdAnMucLucNganSach(int iLoai, Guid iIdDuAn, string iIdDonViQuanLy, int iNamKeHoach, DateTime dNgayLap, Guid iIdLoaiNguonVon, int iIdNguonVon, string sL, string sK, string sM, string sTM, string sTTM, string sNG)
        {
            return _vdtKhvPhanBoVonChiTietRepository.GetVonDaBoTriByDuAnIdAnMucLucNganSach(iLoai, iIdDuAn, iIdDonViQuanLy, iNamKeHoach, dNgayLap, iIdLoaiNguonVon, iIdNguonVon, sL, sK, sM, sTM, sTTM, sNG);
        }

        public bool CreatePhanBoVonChiTiet(Guid iIdPhanBoVon, int iLoaiKeHoach, List<PhanBoVonChiTietInsertQuery> lstDetail, string sUserLogin, bool bIsEdit)//, int iTypeInsert, Guid? iIdPhanBoVon = null)
        {
            if (iLoaiKeHoach == (int)LoaiKeHoachNam.KeHoachVonNamDuocDuyet)
                _tonghopRepository.DeleteTongHopNguonDauTu_Giam(LOAI_CHUNG_TU.KE_HOACH_VON_NAM, iIdPhanBoVon);
            DataTable dt = DBExtension.ConvertDataToTableDefined("t_tbl_phanbovonchitiet5", lstDetail);
            return _vdtKhvPhanBoVonChiTietRepository.CreatePhanBoVonChiTiet(iLoaiKeHoach, dt, sUserLogin, bIsEdit);
        }
        public bool CreatePhanBoVonChiTietClone(Guid iIdPhanBoVon, int iLoaiKeHoach, List<PhanBoVonChiTietInsertQuery> lstDetail, string sUserLogin, bool bIsEdit)//, int iTypeInsert, Guid? iIdPhanBoVon = null)
        {
            if (iLoaiKeHoach == (int)LoaiKeHoachNam.KeHoachVonNamDuocDuyet)
                _tonghopRepository.DeleteTongHopNguonDauTu_Giam(LOAI_CHUNG_TU.KE_HOACH_VON_NAM, iIdPhanBoVon);
            DataTable dt = DBExtension.ConvertDataToTableDefined("t_tbl_phanbovonchitiet5", lstDetail);
            return _vdtKhvPhanBoVonChiTietRepository.CreatePhanBoVonChiTietClone(iLoaiKeHoach, dt, sUserLogin, bIsEdit);
        }

        public bool CreatePhanBoVonChiTietNew(Guid iIdPhanBoVon, int iLoaiKeHoach, List<PhanBoVonChiTietInsertQueryNew> lstDetail, string sUserLogin, bool bIsEdit)//, int iTypeInsert, Guid? iIdPhanBoVon = null)
        {
            if (iLoaiKeHoach == (int)LoaiKeHoachNam.KeHoachVonNamDuocDuyet)
                _tonghopRepository.DeleteTongHopNguonDauTu_Giam(LOAI_CHUNG_TU.KE_HOACH_VON_NAM, iIdPhanBoVon);
            DataTable dt = DBExtension.ConvertDataToTableDefined("t_tbl_phanbovonchitiet7", lstDetail);
            return _vdtKhvPhanBoVonChiTietRepository.CreatePhanBoVonChiTietNew(iLoaiKeHoach, dt, sUserLogin, bIsEdit);
        }

        public int RemovePhanBoVonChiTietByIidPhanBoVonID(Guid iIdPhanBoVonId)
        {
            IEnumerable<VdtKhvPhanBoVonChiTiet> datas = _vdtKhvPhanBoVonChiTietRepository.GetPhanBoVonChiTietByIidPhanBoVonID(iIdPhanBoVonId);
            return _vdtKhvPhanBoVonChiTietRepository.RemovePhanBoVonChiTiet(datas);
        }

        public IEnumerable<VdtKhvPhanBoVonChiTietProAdjustementReportQuery> FindByProjectAdjustementReport(ProjectAdjustementSearch condition)
        {
            return _vdtKhvPhanBoVonChiTietRepository.FindByProjectAdjustementReport(condition);
        }

        public IEnumerable<VdtKhvPhanBoVonChiTietProAdjustementReportQuery> FindByProjectAdjustementReportParent(ProjectAdjustementSearch condition)
        {
            return _vdtKhvPhanBoVonChiTietRepository.FindByProjectAdjustementReportParent(condition);
        }

        public IEnumerable<RptDieuChinhKeHoachQuery> GetRptDieuChinhKeHoach(int iIdNguonVonId, int iNamKeHoach, string sLNS, string sUserLogin)
        {
            return _vdtKhvPhanBoVonChiTietRepository.GetRptDieuChinhKeHoach(iIdNguonVonId, iNamKeHoach, sLNS, sUserLogin);
        }

        public IEnumerable<KeHoachNamDuocDuyetDetailQuery> GetAllKeHoachVonNamDuocDuyetDetail(DateTime dNgayLap, string iIdMaDonViQuanLyId, int iNguonVonId)
        {
            return _vdtKhvPhanBoVonChiTietRepository.GetAllKeHoachVonNamDuocDuyetDetail(dNgayLap, iIdMaDonViQuanLyId, iNguonVonId);
        }

        public IEnumerable<KeHoachNamDuocDuyetDetailQuery> GetKeHoachVonNamDuocDuyetByParentId(Guid iIdPhanBoVonChiTiet)
        {
            return _vdtKhvPhanBoVonChiTietRepository.GetKeHoachVonNamDuocDuyetByParentId(iIdPhanBoVonChiTiet);
        }

        public IEnumerable<VdtKhvVonNamDuocDuyetQuery> GetKeHoachVonNamDuocDuyetReport(string listId, string lct, int type, int loaiDuAn, string lstDonVi, double donViTinh)
        {
            return _vdtKhvPhanBoVonChiTietRepository.GetKeHoachVonNamDuocDuyetReport(listId, lct, type, loaiDuAn, lstDonVi, donViTinh);
        }

        public bool CreateSettlementVoucherApprovedDetail(MidiumTermPlanCriteria creation)
        {
            try
            {
                _vdtKhvPhanBoVonChiTietRepository.CreateSettlementVoucherApprovedDetail(creation);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<PhanBoVonDuocDuyetChiTietQuery> GetPhanBoVonChiTietDieuChinhByParentId(Guid idPhanBoVon)
        {
            return _vdtKhvPhanBoVonChiTietRepository.GetPhanBoVonChiTietDieuChinhByParentId(idPhanBoVon);
        }

        public IEnumerable<PhanBoVonDuocDuyetChiTietQuery> GetPhanBoVonChiTietParent(Guid idPhanBoVon)
        {
            return _vdtKhvPhanBoVonChiTietRepository.GetPhanBoVonChiTietParent(idPhanBoVon);
        }

        public IEnumerable<VdtKhvPhanBoVonChiTiet> GetKeHoachVonNamDuocDuyet(YearPlanCriteria condition)
        {
            return _vdtKhvPhanBoVonChiTietRepository.GetKeHoachVonNamDuocDuyet(condition);
        }

        public IEnumerable<ChiTieuNganSachQuery> GetChiTieuNganSach(string idDuAn, DateTime dNgayQuyetDinh)
        {
            return _vdtKhvPhanBoVonChiTietRepository.GetChiTieuNganSach(idDuAn, dNgayQuyetDinh);
        }
    }
}
