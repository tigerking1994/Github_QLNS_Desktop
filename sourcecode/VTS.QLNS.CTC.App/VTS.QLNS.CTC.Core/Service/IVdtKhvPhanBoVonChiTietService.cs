using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility.Criteria;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtKhvPhanBoVonChiTietService
    {
        /// <summary>
        /// Get all list DuAn in PhanBoVonChiTietInsert View
        /// </summary>
        /// <param name="dNgayLap"></param>
        /// <param name="iIdDonViQuanLyId"></param>
        /// <param name="iIdLoaiCongTrinhId"></param>
        /// <param name="iNguonVonId"></param>
        /// <returns></returns>
        IEnumerable<PhanBoVonChiTietQuery> GetAllDuAnInPhanBoVon(string idPhanBoVonDeXuat, int iNguonVonId);

        IEnumerable<PhanBoVonChiTietQuery> GetAllDuAnInPhanBoVonByEdit(string idPhanBoVonDeXuat, int iNguonVonId);

        IEnumerable<MucLucNganSachChungTuThanhToanQuery> GetAllMucLucNganSachByDuAnId(List<TongHopNguonNSDauTuQuery> lstData);

        /// <summary>
        /// get duan info when change fChiTieuNganSachDuocDuyet
        /// </summary>
        /// <param name="iIdDuAn"></param>
        /// <param name="iIdDonViQuanLy"></param>
        /// <param name="iNamKeHoach"></param>
        /// <param name="dNgayLap"></param>
        /// <param name="iIdLoaiNguonVon"></param>
        /// <param name="sM"></param>
        /// <param name="sTM"></param>
        /// <param name="sTTM"></param>
        /// <param name="sNG"></param>
        /// <returns></returns>
        DuAnInfoQuery GetVonDaBoTriByDuAnIdAnMucLucNganSach(int iLoai, Guid iIdDuAn, string iIdDonViQuanLy, int iNamKeHoach, DateTime dNgayLap, Guid iIdLoaiNguonVon, int iIdNguonVon, string sL, string sK, string sM, string sTM, string sTTM, string sNG);

        /// <summary>
        /// Insert VDT_KH_PhanBoVonChiTiet
        /// </summary>
        /// <param name="lstDetail">list data detail</param>
        /// <param name="sUserLogin">userLogin</param>
        /// <returns></returns>
        bool CreatePhanBoVonChiTiet(Guid iIdPhanBoVon, int iLoaiKeHoach, List<PhanBoVonChiTietInsertQuery> lstDetail, string sUserLogin, bool bIsEdit);//, int iTypeInsert, Guid? iIdPhanBoVon = null);
        
        /// <summary>
        /// Insert VDT_KH_PhanBoVonChiTiet
        /// </summary>
        /// <param name="lstDetail">list data detail</param>
        /// <param name="sUserLogin">userLogin</param>
        /// <returns></returns>
        bool CreatePhanBoVonChiTietClone(Guid iIdPhanBoVon, int iLoaiKeHoach, List<PhanBoVonChiTietInsertQuery> lstDetail, string sUserLogin, bool bIsEdit);//, int iTypeInsert, Guid? iIdPhanBoVon = null);

        /// <summary>
        /// Insert VDT_KH_PhanBoVonChiTiet
        /// </summary>
        /// <param name="lstDetail">list data detail</param>
        /// <param name="sUserLogin">userLogin</param>
        /// <returns></returns>
        bool CreatePhanBoVonChiTietNew(Guid iIdPhanBoVon, int iLoaiKeHoach, List<PhanBoVonChiTietInsertQueryNew> lstDetail, string sUserLogin, bool bIsEdit);//, int iTypeInsert, Guid? iIdPhanBoVon = null)
        /// <summary>
        /// Delete Vdt_Kh_PhanBoVonChiTiet by parent_id
        /// </summary>
        /// <param name="iIdPhanBoVonId">Vdt_Kh_PhanBoVon</param>
        /// <returns></returns>
        int RemovePhanBoVonChiTietByIidPhanBoVonID(Guid iIdPhanBoVonId);

        List<PhanBoVonChiTietQuery> GetPhanBoVonChiTietByParentId(Guid iIdPhanBoVonChiTiet, DateTime? dNgayQuyetDinh = null);
        IEnumerable<PhanBoVonDuocDuyetChiTietQuery> GetPhanBoVonChiTietDieuChinhByParentId(Guid idPhanBoVon);

        IEnumerable<VdtKhvPhanBoVonChiTietProAdjustementReportQuery> FindByProjectAdjustementReport(ProjectAdjustementSearch condition);
        IEnumerable<VdtKhvPhanBoVonChiTietProAdjustementReportQuery> FindByProjectAdjustementReportParent(ProjectAdjustementSearch condition);
        IEnumerable<RptDieuChinhKeHoachQuery> GetRptDieuChinhKeHoach(int iIdNguonVonId, int iNamKeHoach, string sLNS, string sUserLogin);
        IEnumerable<KeHoachNamDuocDuyetDetailQuery> GetAllKeHoachVonNamDuocDuyetDetail(DateTime dNgayLap, string iIdMaDonViQuanLyId, int iNguonVonId);
        IEnumerable<KeHoachNamDuocDuyetDetailQuery> GetKeHoachVonNamDuocDuyetByParentId(Guid iIdPhanBoVonChiTiet);
        IEnumerable<VdtKhvVonNamDuocDuyetQuery> GetKeHoachVonNamDuocDuyetReport(string listId, string lct, int type, int loaiDuAn, string lstDonVi, double donViTinh);
        bool CreateSettlementVoucherApprovedDetail(MidiumTermPlanCriteria creation);
        IEnumerable<PhanBoVonDuocDuyetChiTietQuery> GetPhanBoVonChiTietParent(Guid idPhanBoVon);
        IEnumerable<VdtKhvPhanBoVonChiTiet> GetKeHoachVonNamDuocDuyet(YearPlanCriteria condition);
        IEnumerable<ChiTieuNganSachQuery> GetChiTieuNganSach(string idDuAn, DateTime @dNgayQuyetDinh);
    }
}
