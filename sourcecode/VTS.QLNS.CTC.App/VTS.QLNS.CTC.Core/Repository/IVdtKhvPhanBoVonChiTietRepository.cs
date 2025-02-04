using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility.Criteria;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtKhvPhanBoVonChiTietRepository : IRepository<VdtKhvPhanBoVonChiTiet>
    {
        /// <summary>
        /// Get all list DuAn in PhanBoVonChiTietInsert View
        /// </summary>
        /// <param name="dNgayLap"></param>
        /// <param name="iIdDonViQuanLyId"></param>
        /// <param name="iNguonVonId"></param>
        /// <returns></returns>
        IEnumerable<PhanBoVonChiTietQuery> GetAllDuAnInPhanBoVon(string idPhanBoVonDeXuat, int iNguonVonId);

        IEnumerable<PhanBoVonChiTietQuery> GetAllDuAnInPhanBoVonByEdit(string idPhanBoVonDeXuat, int iNguonVonId);

        IEnumerable<MucLucNganSachChungTuThanhToanQuery> GetAllMucLucNganSachByDuAnId(List<TongHopNguonNSDauTuQuery> lstData);

        /// <summary>
        /// Get Vdt_Kh_PhanBoVonChiTiet by PhanBoVonID
        /// </summary>
        /// <param name="iIdPhanBoVonChiTiet"></param>
        /// <param name="dNgayQuyetDinh">if is adjust , have value</param>
        /// <returns></returns>
        List<PhanBoVonChiTietQuery> GetPhanBoVonChiTietByParentId(Guid iIdPhanBoVonChiTiet, DateTime? dNgayQuyetDinh);

        /// <summary>
        /// Insert PhanBoVonChiTiet
        /// </summary>
        /// <param name="dt">VDT_KH_PhanBoVonChiTiet</param>
        /// <param name="sUserLogin">user login</param>
        /// <returns></returns>
        bool CreatePhanBoVonChiTiet(int iLoaiKeHoach, DataTable dt, string sUserLogin, bool bIsEdit);

        /// <summary>
        /// Insert PhanBoVonChiTietClone
        /// </summary>
        /// <param name="dt">VDT_KH_PhanBoVonChiTiet</param>
        /// <param name="sUserLogin">user login</param>
        /// <returns></returns>
        bool CreatePhanBoVonChiTietClone(int iLoaiKeHoach, DataTable dt, string sUserLogin, bool bIsEdit);

        /// <summary>
        /// get duan info when change fChiTieuNganSachDuocDuyet
        /// </summary>
        /// <param name="iIdDuAn"></param>
        /// <param name="iIdDonViQuanLy"></param>
        /// <param name="iNamKeHoach"></param>
        /// <param name="dNgayLap"></param>
        /// <param name="iIdLoaiNguonVon"></param>
        /// <param name="sL"></param>
        /// <param name="sK"></param>
        /// <param name="sM"></param>
        /// <param name="sTM"></param>
        /// <param name="sTTM"></param>
        /// <param name="sNG"></param>
        /// <returns></returns>
        DuAnInfoQuery GetVonDaBoTriByDuAnIdAnMucLucNganSach(int iLoai, Guid iIdDuAn, string iIdDonViQuanLy, int iNamKeHoach, DateTime dNgayLap, Guid iIdLoaiNguonVon, int iIdNguonVon, string sL, string sK, string sM, string sTM, string sTTM, string sNG);

        /// <summary>
        /// Get VDT_KHV_PhanBoVonChiTiet by VDT_KHV_PhanBoVon
        /// </summary>
        /// <param name="iIdPhanBoVonId">VDT_KHV_PhanBoVon.iIdPhanBoVon_Id</param>
        /// <returns></returns>
        IEnumerable<VdtKhvPhanBoVonChiTiet> GetPhanBoVonChiTietByIidPhanBoVonID(Guid iIdPhanBoVonId);

        /// <summary>
        /// Remove VDT_KHV_PhanBoVonChiTiet
        /// </summary>
        /// <param name="data">VDT_KHV_PhanBoVonChiTiet</param>
        /// <returns></returns>
        int RemovePhanBoVonChiTiet(VdtKhvPhanBoVonChiTiet data);

        /// <summary>
        /// Update list VDT_KHV_PhanBoVonChiTiet
        /// </summary>
        /// <param name="datas">VDT_KHV_PhanBoVonChiTiets</param>
        /// <returns></returns>
        int Update(IEnumerable<VdtKhvPhanBoVonChiTiet> datas);

        /// <summary>
        /// Remove list VDT_KHV_PhanBoVonChiTiet
        /// </summary>
        /// <param name="datas">VDT_KHV_PhanBoVonChiTiets</param>
        /// <returns></returns>
        int RemovePhanBoVonChiTiet(IEnumerable<VdtKhvPhanBoVonChiTiet> datas);

        IEnumerable<VdtKhvPhanBoVonChiTietProAdjustementReportQuery> FindByProjectAdjustementReport(ProjectAdjustementSearch condition);
        IEnumerable<VdtKhvPhanBoVonChiTietProAdjustementReportQuery> FindByProjectAdjustementReportParent(ProjectAdjustementSearch condition);
        IEnumerable<RptDieuChinhKeHoachQuery> GetRptDieuChinhKeHoach(int iIdNguonVonId, int iNamKeHoach, string sLNS, string sUserLogin);
        IEnumerable<KeHoachNamDuocDuyetDetailQuery> GetAllKeHoachVonNamDuocDuyetDetail(DateTime dNgayLap, string iIdMaDonViQuanLyId, int iNguonVonId);
        IEnumerable<KeHoachNamDuocDuyetDetailQuery> GetKeHoachVonNamDuocDuyetByParentId(Guid iIdPhanBoVonChiTiet);
        IEnumerable<PhanBoVonDuocDuyetChiTietQuery> GetPhanBoVonChiTietDieuChinhByParentId(Guid idPhanBoVon);
        IEnumerable<VdtKhvVonNamDuocDuyetQuery> GetKeHoachVonNamDuocDuyetReport(string listId, string lct, int type, int loaiDuAn, string lstDonVi, double donViTinh);
        void CreateSettlementVoucherApprovedDetail(MidiumTermPlanCriteria creation);
        IEnumerable<PhanBoVonDuocDuyetChiTietQuery> GetPhanBoVonChiTietParent(Guid idPhanBoVon);
        IEnumerable<VdtKhvPhanBoVonChiTiet> GetKeHoachVonNamDuocDuyet(YearPlanCriteria condition);
        IEnumerable<ChiTieuNganSachQuery> GetChiTieuNganSach(string idDuAn, DateTime dNgayQuyetDinh);

        bool CreatePhanBoVonChiTietNew(int iLoaiKeHoach, DataTable dt, string sUserLogin, bool bIsEdit);
    }
}
