using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INsDtChungTuChiTietService
    {
        IEnumerable<NsDtChungTuChiTiet> FindAll(Expression<Func<NsDtChungTuChiTiet, bool>> predicate);
        IEnumerable<NsDtChungTuChiTietQuery> FindByCondition(EstimationVoucherDetailCriteria searchCondition, string procedure = "sp_dt_chungtu_chitiet");
        IEnumerable<NsDtChungTuChiTietQuery> FindByCond(EstimationVoucherDetailCriteria searchCondition, string procedure = "sp_dt_phanbo_dutoan_chitiet");
        IEnumerable<NsDtChungTuChiTietQuery> FindReportNhanPhanBoDuToanTheoDot(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsDtChungTuChiTietCongKhaiQuery> FindDtChungTuChiTietCongKhai(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsQtCongKhaiThuChi> FindRptQtCongKhaiThuChi(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsQtCongKhaiThuChi> FindRptQtCongKhaiThuChiDonVi(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsDtChungTuChiTietCongKhaiQuery> FindDtChungTuChiTietCongKhaiClone(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsDtChungTuChiTietQuery> FindReportCongKhaiTaiChinh(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<DuToanDonViQuery> FindDuToanDonvi(DuToanDonViCriteria searchCondition);
        IEnumerable<ReportDuToanNhanPhanBoTheoDotQuery> FindDuToanTheoDot(EstimationVoucherDetailCriteria searchCondition);
        NsDtChungTuChiTiet FindById(Guid id);
        NsDtChungTuChiTiet FindByIdMlns(Guid id);
        int Update(NsDtChungTuChiTiet entity);
        int AddRange(IEnumerable<NsDtChungTuChiTiet> entities);
        void Delete(Guid id);
        int RemoveRange(IEnumerable<NsDtChungTuChiTiet> entities);
        IEnumerable<NsDtChungTuChiTiet> FindByIdChungTu(string idChungTu);
        List<NsDtChungTuChiTietQuery> FindDuToanTheoNganh(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsDtChungTuChiTiet> FindDuToanTongHop(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsDtChungTuChiTietQuery> FindByLuyKePhanBoTongHop(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsDtChungTuChiTietQuery> FindByLuyKePhanBoTongHopClone(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsDtChungTuChiTietQuery> FindByLuyKePhanBoTongHopSummary(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsDtChungTuChiTietQuery> FindByLuyKeTongHop(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsDtChungTuChiTietQuery> FindByLuyKeTongHopClone(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsDtChungTuChiTietQuery> FindByLuyKeTongHopSummary(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsDtChungTuChiTietQuery> FindByLuyKeTongHopDotSummary(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsDtChungTuChiTietQuery> FindBudgetEstimateDivision(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsDtChungTuChiTietQuery> FindBudgetEstimateDivisionBySoQuyetDinh(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsDtChungTuChiTietQuery> FindBudgetEstimateDivisionBySoQuyetDinhLNS(EstimationVoucherDetailCriteria searchCondition);
        void DeleteByIdChungTu(Guid id);
        void DeleteByIdChungTuDuToanNhan(Guid id, String idDuToanNhan);
        IEnumerable<NsDtChungTuChiTiet> FindByListIdChungTu(IEnumerable<string> listIdChung);
        void DeleteByIds(IEnumerable<string> ids);
        IEnumerable<NsDtChungTuChiTiet> FindByListIds(IEnumerable<string> ids);
        IEnumerable<ReportChiTieuDuToanQuery> GetDataReportChiTieuToBia(string idChungTu, int donViTinh);
        IEnumerable<ReportChiTieuDuToanQuery> GetDataReportChiTieuToBiaLuyKe(string idChungTu, int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu);
        IEnumerable<ReportChiTieuDuToanQuery> GetDataReportChiTieuDonVi(int namLamViec, int nguonNganSach, int namNganSach, string idDonVi,
            string idChungTu, DateTime? ngayQuyetDinh, int donViTinh, bool isLuyKe);
        IEnumerable<ReportChiTieuDuToanQuery> GetDataReportChiTieuNganh(int namLamViec, int nguonNganSach, int namNganSach, string nganh,
           string idChungTu, int loaiChungTu, int donViTinh, bool isLuyKe, bool haveDonVi);
        IEnumerable<ReportChiTieuDuToanQuery> GetDataReportChiTieuDonViDuToan(int namLamViec, int nguonNganSach, int namNganSach, DateTime? ngayChungTu,
            string idChungTu, int donViTinh, bool isPrintTNG);
        IEnumerable<ReportDuToanTongHopSoPhanBoQuery> GetDataReportTongHopSoPhanBo(int namLamViec, int namNganSach, int nguonNganSach, string lns, DateTime? ngayQuyetDinh, double donViTinh, int loaiDuToan, string sSoQuyetDinh);
        IEnumerable<ReportDuToanTongHopSoPhanBoQuery> GetDataReportTongHopSoPhanBoHienVat(int namLamViec, int namNganSach,
            int nguonNganSach, string lns, DateTime? ngayQuyetDinh, double donViTinh, int loaiDuToan, string sSoQuyetDinh);
        void DeleteInputData(Guid chungTuId);
        IEnumerable<ReportDuToanThongKeSoQuyetDinhQuery> GetDataReportDuToanThongKeSoQuyetDinh(int yearOfWork, int yearOfBudget, int budgetSource, string soQuyetDinh, string lns, int dvt);
        IEnumerable<NsDuToanChungTuChiTietQuery> FindChungTuChiTiet(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsDuToanChungTuChiTietDieuChinhQuery> FindChungTuChiTietDieuChinh(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<ReportChiTieuDuToanDynamicQuery> GetDataReportChiTieuNganhAll(int namLamViec, int nguonNganSach, int namNganSach, string nganh, string idChungTu, int donViTinh, bool isLuyKe, bool haveDonVi);
        IEnumerable<string> GetLnsHasData(List<Guid> chungTuIds);
        IEnumerable<string> GetLnsHasSpendData(List<Guid> chungTuIds);
        IEnumerable<string> GetLnsHasCollectData(List<Guid> chungTuIds);
        IEnumerable<ReportChiTieuDuToanDynamicMLNSQuery> GetDataReportChiTieuNganhAllMLNS(int namLamViec, int nguonNganSach, int namNganSach, string nganh, string idChungTu, int donViTinh, bool isLuyKe);
        IEnumerable<NsDtChungTuChiTietQuery> GetDataTongHopPhanBoTheoDot(EstimationVoucherDetailCriteria searchCondition);
        bool isExistEstimate(Guid id, Guid estimateId);
        void BulkInsert(List<NsDtChungTuChiTiet> lstData);
        IEnumerable<NsDtChungTuCongKhaiQuery> GetDataBaoCaoDanhMucCongKhai02(int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, int iQuarterMonths, string sIdDanhMucCongKhai, int dvt);
        IEnumerable<NsDtChungTuCongKhaiQuery> GetDataBaoCaoDanhMucCongKhai02Clone(int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, int iQuarterMonths, string sIdDanhMucCongKhai, int dvt, string sIdDotNhan);
        IEnumerable<NsDtPhuongAnPhanBoQuery> ExportMau01PhuLucI(string maCongKhai, int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, string sTuDot, string sDenDot, int dvt);
        IEnumerable<TnDtDuToanReportQuery> ExportPhuongAnPhanBo4554(string agencies, int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, string sIdChungTuDutoan, string sIdChungTuThuNop, int dvt, string voucherType);
        IEnumerable<NsDtPhuongAnPhanBoQuery> ExportMau01PhuLucII(string sLns, int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, string sTuDot, string sDenDot, int dvt);
        IEnumerable<NsDtPhuongAnPhanBoQuery> ExportMau01PhuLucIIDonViExcel(string sLns, int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, string sTuDot, string sDenDot, string maDonVi, int dvt);
        IEnumerable<NsDtPhuongAnPhanBoQuery> ExportMau01PhuLucIIDonVi(string sLns, int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, string sTuDot, string sDenDot, string maDonVi, int dvt);
        List<string> GetReportUnitPhuLucII(string sLns, int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, string sTuDot, string sDenDot);
        List<string> GetReportSelfUnitPhuLucII(int iNamLamViec);
        IEnumerable<string> GetXauNoiMaHasSpendData(List<Guid> chungTuIds);
        IEnumerable<string> GetXauNoiMaHasCollectData(List<Guid> chungTuIds);
        IEnumerable<NsDtPhuongAnPhanBoQuery> ExportMau02(string maCongKhai, int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, string sTuDot, string sDenDot, int dvt);
    }
}
