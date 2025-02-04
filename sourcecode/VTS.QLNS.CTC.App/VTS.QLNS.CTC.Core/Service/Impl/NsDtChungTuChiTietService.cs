using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NsDtChungTuChiTietService : INsDtChungTuChiTietService
    {
        private readonly INsDtChungTuChiTietRepository _dtChungTuChiTietRepository;

        public NsDtChungTuChiTietService(INsDtChungTuChiTietRepository dtChungTuChiTietRepository)
        {
            _dtChungTuChiTietRepository = dtChungTuChiTietRepository;
        }

        public int AddRange(IEnumerable<NsDtChungTuChiTiet> entities)
        {
            return _dtChungTuChiTietRepository.AddRange(entities);
        }

        public void Delete(Guid id)
        {
            NsDtChungTuChiTiet entity = _dtChungTuChiTietRepository.Find(id);
            if (entity != null)
            {
                _dtChungTuChiTietRepository.Delete(entity);
            }
        }

        public int RemoveRange(IEnumerable<NsDtChungTuChiTiet> entities)
        {
            return _dtChungTuChiTietRepository.RemoveRange(entities);
        }

        public NsDtChungTuChiTiet FindById(Guid id)
        {
            return _dtChungTuChiTietRepository.Find(id);
        }

        public NsDtChungTuChiTiet FindByIdMlns(Guid id)
        {
            return _dtChungTuChiTietRepository.FindByIdMlns(id);
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindByCond(EstimationVoucherDetailCriteria searchCondition, string procedure)
        {
            return _dtChungTuChiTietRepository.FindByCond(searchCondition, procedure);
        }

        public IEnumerable<NsDtChungTuChiTiet> FindAll(Expression<Func<NsDtChungTuChiTiet, bool>> predicate)
        {
            return _dtChungTuChiTietRepository.FindAll(predicate);
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindByCondition(EstimationVoucherDetailCriteria searchCondition, string procedure)
        {
            return _dtChungTuChiTietRepository.FindByCondition(searchCondition, procedure);
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindReportNhanPhanBoDuToanTheoDot(EstimationVoucherDetailCriteria searchCondition)
        {
            return _dtChungTuChiTietRepository.FindReportNhanPhanBoDuToanTheoDot(searchCondition);
        }

        public IEnumerable<NsDtChungTuChiTietCongKhaiQuery> FindDtChungTuChiTietCongKhai(EstimationVoucherDetailCriteria searchCondition)
        {
            return _dtChungTuChiTietRepository.FindDtChungTuChiTietCongKhai(searchCondition);
        }

        public IEnumerable<NsQtCongKhaiThuChi> FindRptQtCongKhaiThuChi(EstimationVoucherDetailCriteria searchCondition)
        {
            return _dtChungTuChiTietRepository.FindRptQtCongKhaiThuChi(searchCondition);
        }

        public IEnumerable<NsQtCongKhaiThuChi> FindRptQtCongKhaiThuChiDonVi(EstimationVoucherDetailCriteria searchCondition)
        {
            return _dtChungTuChiTietRepository.FindRptQtCongKhaiThuChiDonVi(searchCondition);
        }

        public IEnumerable<NsDtChungTuChiTietCongKhaiQuery> FindDtChungTuChiTietCongKhaiClone(EstimationVoucherDetailCriteria searchCondition)
        {
            return _dtChungTuChiTietRepository.FindDtChungTuChiTietCongKhaiClone(searchCondition);
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindReportCongKhaiTaiChinh(EstimationVoucherDetailCriteria searchCondition)
        {
            return _dtChungTuChiTietRepository.FindReportCongKhaiTaiChinh(searchCondition);
        }

        public IEnumerable<DuToanDonViQuery> FindDuToanDonvi(DuToanDonViCriteria searchCondition)
        {
            return _dtChungTuChiTietRepository.FindDuToanDonvi(searchCondition);
        }

        public int Update(NsDtChungTuChiTiet entity)
        {
            return _dtChungTuChiTietRepository.Update(entity);
        }

        public IEnumerable<ReportDuToanNhanPhanBoTheoDotQuery> FindDuToanTheoDot(EstimationVoucherDetailCriteria searchCondition)
        {
            return _dtChungTuChiTietRepository.FindDuToanTheoDot(searchCondition);
        }

        public IEnumerable<NsDtChungTuChiTiet> FindByIdChungTu(string idChungTu)
        {
            return _dtChungTuChiTietRepository.FindByIdChungTu(idChungTu);
        }

        public List<NsDtChungTuChiTietQuery> FindDuToanTheoNganh(EstimationVoucherDetailCriteria searchCondition)
        {
            return _dtChungTuChiTietRepository.FindDuToanTheoNganh(searchCondition).ToList();
        }

        public IEnumerable<NsDtChungTuChiTiet> FindDuToanTongHop(EstimationVoucherDetailCriteria searchCondition)
        {
            return _dtChungTuChiTietRepository.FindDuToanTongHop(searchCondition);
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindByLuyKePhanBoTongHop(EstimationVoucherDetailCriteria searchCondition)
        {
            return _dtChungTuChiTietRepository.FindByLuyKePhanBoTongHop(searchCondition);
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindByLuyKePhanBoTongHopClone(EstimationVoucherDetailCriteria searchCondition)
        {
            return _dtChungTuChiTietRepository.FindByLuyKePhanBoTongHopClone(searchCondition);
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindByLuyKePhanBoTongHopSummary(EstimationVoucherDetailCriteria searchCondition)
        {
            return _dtChungTuChiTietRepository.FindByLuyKePhanBoTongHopSummary(searchCondition);
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindByLuyKeTongHop(EstimationVoucherDetailCriteria searchCondition)
        {
            return _dtChungTuChiTietRepository.FindByLuyKeTongHop(searchCondition);
        }
        public IEnumerable<NsDtChungTuChiTietQuery> FindByLuyKeTongHopClone(EstimationVoucherDetailCriteria searchCondition)
        {
            return _dtChungTuChiTietRepository.FindByLuyKeTongHopClone(searchCondition);
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindByLuyKeTongHopSummary(EstimationVoucherDetailCriteria searchCondition)
        {
            return _dtChungTuChiTietRepository.FindByLuyKeTongHopSummary(searchCondition);
        }
        public IEnumerable<NsDtChungTuChiTietQuery> FindByLuyKeTongHopDotSummary(EstimationVoucherDetailCriteria searchCondition)
        {
            return _dtChungTuChiTietRepository.FindByLuyKeTongHopDotSummary(searchCondition);
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindBudgetEstimateDivision(EstimationVoucherDetailCriteria searchCondition)
        {
            return _dtChungTuChiTietRepository.FindBudgetEstimateDivision(searchCondition);
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindBudgetEstimateDivisionBySoQuyetDinh(EstimationVoucherDetailCriteria searchCondition)
        {
            return _dtChungTuChiTietRepository.FindBudgetEstimateDivisionBySoQuyetDinh(searchCondition);
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindBudgetEstimateDivisionBySoQuyetDinhLNS(EstimationVoucherDetailCriteria searchCondition)
        {
            return _dtChungTuChiTietRepository.FindBudgetEstimateDivisionBySoQuyetDinhLNS(searchCondition);
        }

        public void DeleteByIdChungTu(Guid id)
        {
            _dtChungTuChiTietRepository.DeleteByIdChungTu(id);
        }

        public void DeleteByIdChungTuDuToanNhan(Guid id, String idDuToanNhan)
        {
            _dtChungTuChiTietRepository.DeleteByIdChungTuDuToanNhan(id, idDuToanNhan);
        }

        public IEnumerable<NsDtChungTuChiTiet> FindByListIdChungTu(IEnumerable<string> listIdChungTu)
        {
            return _dtChungTuChiTietRepository.FindByListIdChungTu(listIdChungTu);
        }
        public IEnumerable<NsDtChungTuChiTiet> FindByListIds(IEnumerable<string> ids)
        {
            return _dtChungTuChiTietRepository.FindByListIds(ids);
        }

        public void DeleteByIds(IEnumerable<string> ids)
        {
            _dtChungTuChiTietRepository.DeleteByIds(ids);
        }

        public IEnumerable<ReportChiTieuDuToanQuery> GetDataReportChiTieuToBia(string idChungTu, int donViTinh)
        {
            return _dtChungTuChiTietRepository.GetDataReportChiTieuToBia(idChungTu, donViTinh);
        }
        public IEnumerable<ReportChiTieuDuToanQuery> GetDataReportChiTieuToBiaLuyKe(string idChungTu, int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu)
        {
            return _dtChungTuChiTietRepository.GetDataReportChiTieuToBiaLuyKe(idChungTu, namLamViec, namNganSach, nguonNganSach, loaiChungTu);
        }

        public IEnumerable<ReportChiTieuDuToanQuery> GetDataReportChiTieuDonVi(int namLamViec, int nguonNganSach, int namNganSach, string idDonVi, string idChungTu, DateTime? ngayQuyetDinh, int donViTinh, bool isLuyKe)
        {
            return _dtChungTuChiTietRepository.GetDataReportChiTieuDonVi(namLamViec, nguonNganSach, namNganSach, idDonVi, idChungTu, ngayQuyetDinh, donViTinh, isLuyKe);
        }

        public IEnumerable<ReportChiTieuDuToanQuery> GetDataReportChiTieuDonViDuToan(int namLamViec, int nguonNganSach, int namNganSach, DateTime? ngayChungTu, string idChungTu, int donViTinh, bool isPrintTNG)
        {
            return _dtChungTuChiTietRepository.GetDataReportChiTieuDonViDuToan(namLamViec, nguonNganSach, namNganSach, ngayChungTu, idChungTu, donViTinh, isPrintTNG);
        }

        public IEnumerable<ReportDuToanTongHopSoPhanBoQuery> GetDataReportTongHopSoPhanBo(int namLamViec, int namNganSach, int nguonNganSach, string lns, DateTime? ngayQuyetDinh, double donViTinh, int loaiDuToan, string sSoQuyetDinh)
        {
            return _dtChungTuChiTietRepository.GetDataReportTongHopSoPhanBo(namLamViec, namNganSach, nguonNganSach, lns, ngayQuyetDinh, donViTinh, loaiDuToan, sSoQuyetDinh);
        }

        public IEnumerable<ReportDuToanTongHopSoPhanBoQuery> GetDataReportTongHopSoPhanBoHienVat(int namLamViec, int namNganSach, int nguonNganSach, string lns, DateTime? ngayQuyetDinh, double donViTinh, int loaiDuToan, string sSoQuyetDinh)
        {
            return _dtChungTuChiTietRepository.GetDataReportTongHopSoPhanBoHienVat(namLamViec, namNganSach, nguonNganSach, lns, ngayQuyetDinh, donViTinh, loaiDuToan, sSoQuyetDinh);
        }

        public void DeleteInputData(Guid chungTuId)
        {
            _dtChungTuChiTietRepository.DeleteInputData(chungTuId);
        }

        public IEnumerable<ReportChiTieuDuToanQuery> GetDataReportChiTieuNganh(int namLamViec, int nguonNganSach, int namNganSach, string nganh, string idChungTu, int loaiChungTu, int donViTinh, bool isLuyKe, bool haveDonVi)
        {
            return _dtChungTuChiTietRepository.GetDataReportChiTieuNganh(namLamViec, nguonNganSach, namNganSach, nganh, idChungTu, loaiChungTu, donViTinh, isLuyKe, haveDonVi);
        }

        public IEnumerable<ReportDuToanThongKeSoQuyetDinhQuery> GetDataReportDuToanThongKeSoQuyetDinh(int yearOfWork, int yearOfBudget, int budgetSource, string soQuyetDinh, string lns, int dvt)
        {
            return _dtChungTuChiTietRepository.GetDataReportDuToanThongKeSoQuyetDinh(yearOfWork, yearOfBudget, budgetSource, soQuyetDinh, lns, dvt);
        }

        public IEnumerable<ReportChiTieuDuToanDynamicQuery> GetDataReportChiTieuNganhAll(int namLamViec, int nguonNganSach, int namNganSach, string nganh, string idChungTu, int donViTinh, bool isLuyKe, bool haveDonVi)
        {
            return _dtChungTuChiTietRepository.GetDataReportChiTieuNganhAll(namLamViec, nguonNganSach, namNganSach, nganh, idChungTu, donViTinh, isLuyKe, haveDonVi);
        }

        public IEnumerable<NsDuToanChungTuChiTietQuery> FindChungTuChiTiet(EstimationVoucherDetailCriteria searchCondition)
        {
            return _dtChungTuChiTietRepository.FindChungTuChiTiet(searchCondition);
        }

        public IEnumerable<NsDuToanChungTuChiTietDieuChinhQuery> FindChungTuChiTietDieuChinh(EstimationVoucherDetailCriteria searchCondition)
        {
            return _dtChungTuChiTietRepository.FindChungTuChiTietDieuChinh(searchCondition);
        }

        public IEnumerable<string> GetLnsHasData(List<Guid> chungTuIds)
        {
            return _dtChungTuChiTietRepository.GetLnsHasData(chungTuIds);
        }

        public IEnumerable<ReportChiTieuDuToanDynamicMLNSQuery> GetDataReportChiTieuNganhAllMLNS(int namLamViec, int nguonNganSach, int namNganSach, string nganh, string idChungTu, int donViTinh, bool isLuyKe)
        {
            return _dtChungTuChiTietRepository.GetDataReportChiTieuNganhAllMLNS(namLamViec, nguonNganSach, namNganSach, nganh, idChungTu, donViTinh, isLuyKe);
        }

        public IEnumerable<NsDtChungTuChiTietQuery> GetDataTongHopPhanBoTheoDot(EstimationVoucherDetailCriteria searchCondition)
        {
            return _dtChungTuChiTietRepository.GetDataTongHopPhanBoTheoDot(searchCondition);
        }

        public bool isExistEstimate(Guid id, Guid estimateId)
        {
            return _dtChungTuChiTietRepository.IsExistEstimate(id, estimateId);
        }

        public void BulkInsert(List<NsDtChungTuChiTiet> lstData)
        {
            _dtChungTuChiTietRepository.BulkInsert(lstData);
        }
        public IEnumerable<NsDtChungTuCongKhaiQuery> GetDataBaoCaoDanhMucCongKhai02(int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, int iQuarterMonths, string sIdDanhMucCongKhai, int dvt)
        {
            return _dtChungTuChiTietRepository.GetDataBaoCaoDanhMucCongKhai02(iNamLamViec, iNamNganSach, iMaNguonNganSach, iQuarterMonths, sIdDanhMucCongKhai, dvt);
        }

        public IEnumerable<NsDtChungTuCongKhaiQuery> GetDataBaoCaoDanhMucCongKhai02Clone(int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, int iQuarterMonths, string sIdDanhMucCongKhai, int dvt, string sIdDotNhan)
        {
            return _dtChungTuChiTietRepository.GetDataBaoCaoDanhMucCongKhai02Clone(iNamLamViec, iNamNganSach, iMaNguonNganSach, iQuarterMonths, sIdDanhMucCongKhai, dvt, sIdDotNhan);
        }

        public IEnumerable<string> GetLnsHasSpendData(List<Guid> chungTuIds)
        {
            return _dtChungTuChiTietRepository.GetLnsHasSpendData(chungTuIds);
        }

        public IEnumerable<string> GetLnsHasCollectData(List<Guid> chungTuIds)
        {
            return _dtChungTuChiTietRepository.GetLnsHasCollectData(chungTuIds);
        }

        public IEnumerable<NsDtPhuongAnPhanBoQuery> ExportMau01PhuLucI(string maCongKhai, int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, string sTuDot, string sDenDot, int dvt)
        {
            return _dtChungTuChiTietRepository.ExportMau01PhuLucI(maCongKhai, iNamLamViec, iNamNganSach, iMaNguonNganSach, sTuDot, sDenDot, dvt);
        }

        public IEnumerable<TnDtDuToanReportQuery> ExportPhuongAnPhanBo4554(string agencies, int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, string sIdChungTuDutoan, string sIdChungTuThuNop, int dvt, string voucherType)
        {
            return _dtChungTuChiTietRepository.ExportPhuongAnPhanBo4554(agencies, iNamLamViec, iNamNganSach, iMaNguonNganSach, sIdChungTuDutoan, sIdChungTuThuNop, dvt, voucherType);
        }
        public IEnumerable<NsDtPhuongAnPhanBoQuery> ExportMau01PhuLucII(string sLns, int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, string sTuDot, string sDenDot, int dvt)
        {
            return _dtChungTuChiTietRepository.ExportMau01PhuLucII(sLns, iNamLamViec, iNamNganSach, iMaNguonNganSach, sTuDot, sDenDot, dvt);
        }
        public IEnumerable<NsDtPhuongAnPhanBoQuery> ExportMau01PhuLucIIDonVi(string sLns, int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, string sTuDot, string sDenDot, string maDonVi, int dvt)
        {
            return _dtChungTuChiTietRepository.ExportMau01PhuLucIIDonVi(sLns, iNamLamViec, iNamNganSach, iMaNguonNganSach, sTuDot, sDenDot, maDonVi, dvt);
        }

        public List<string> GetReportUnitPhuLucII(string sLns, int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, string sTuDot, string sDenDot)
        {
            return _dtChungTuChiTietRepository.GetReportUnitPhuLucII(sLns, iNamLamViec, iNamNganSach, iMaNguonNganSach, sTuDot, sDenDot);
        }

        public List<string> GetReportSelfUnitPhuLucII(int iNamLamViec)
        {
            return _dtChungTuChiTietRepository.GetReportSelfUnitPhuLucII(iNamLamViec);
        }

        public IEnumerable<string> GetXauNoiMaHasSpendData(List<Guid> chungTuIds)
        {
            return _dtChungTuChiTietRepository.GetXauNoiMaHasSpendData(chungTuIds);
        }

        public IEnumerable<NsDtPhuongAnPhanBoQuery> ExportMau02(string maCongKhai, int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, string sTuDot, string sDenDot, int dvt)
        {
            return _dtChungTuChiTietRepository.ExportMau02(maCongKhai, iNamLamViec, iNamNganSach, iMaNguonNganSach, sTuDot, sDenDot, dvt);
        }

        public IEnumerable<string> GetXauNoiMaHasCollectData(List<Guid> chungTuIds)
        {
            return _dtChungTuChiTietRepository.GetXauNoiMaHasCollectData(chungTuIds);
        }

        public IEnumerable<NsDtPhuongAnPhanBoQuery> ExportMau01PhuLucIIDonViExcel(string sLns, int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, string sTuDot, string sDenDot, string maDonVi, int dvt)
        {
            return _dtChungTuChiTietRepository.ExportMau01PhuLucIIDonViExcel(sLns, iNamLamViec, iNamNganSach, iMaNguonNganSach, sTuDot, sDenDot, maDonVi, dvt);
        }
    }
}
