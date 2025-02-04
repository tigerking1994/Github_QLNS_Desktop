using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class SktSoLieuService : ISktSoLieuService
    {
        private readonly ISktSoLieuRepository _sktSoLieuRepository;

        public SktSoLieuService(ISktSoLieuRepository sktSoLieuRepository)
        {
            _sktSoLieuRepository = sktSoLieuRepository;
        }

        public int AddRange(IEnumerable<NsDtdauNamChungTuChiTiet> entities)
        {
            return _sktSoLieuRepository.AddRange(entities);
        }

        public void CreateDataReportTotal(int namLamViec, int namNganSach, int nguonNganSach, int loai, int typeGet, string idDonVi, string loaiChungTu, string listDonViTongHop, string nguoiTao)
        {
            _sktSoLieuRepository.CreateDataReportTotal(namLamViec, namNganSach, nguonNganSach, loai, typeGet, idDonVi, loaiChungTu, listDonViTongHop, nguoiTao);
        }

        public int Delete(Guid id)
        {
            NsDtdauNamChungTuChiTiet entity = _sktSoLieuRepository.Find(id);
            if (entity != null)
            {
                return _sktSoLieuRepository.Delete(entity);
            }
            else
            {
                return 0;
            }
        }

        public NsDtdauNamChungTuChiTiet Find(params object[] keyValues)
        {
            return _sktSoLieuRepository.Find(keyValues);
        }

        public IEnumerable<SktSoLieuChiTietMlnsQuery> FindByCondition(int namLamViec, int namNganSach, int nguonNganSach, int loai, int typeGet, string idDonVi, string loaiChungTu)
        {
            return _sktSoLieuRepository.FindByCondition(namLamViec, namNganSach, nguonNganSach, loai, typeGet, idDonVi, loaiChungTu);
        }

        public IEnumerable<SktSoLieuChiTietMlnsQuery> FindByCondition(int namLamViec, int namNganSach, int nguonNganSach, int loai, int typeGet, string idDonVi, string loaiChungTu, string lns, string voucherId)
        {
            return _sktSoLieuRepository.FindByCondition(namLamViec, namNganSach, nguonNganSach, loai, typeGet, idDonVi, loaiChungTu, lns, voucherId);
        }

        public IEnumerable<SktSoLieuChiTietMlnsQuery> FindByConditionDonVi0(int namLamViec, int namNganSach, int nguonNganSach, int loai, int typeGet, string idDonVi, string loaiChungTu, string listIdChungTu, string lns)
        {
            return _sktSoLieuRepository.FindByConditionDonVi0(namLamViec, namNganSach, nguonNganSach, loai, typeGet, idDonVi, loaiChungTu, listIdChungTu, lns);
        }
        public IEnumerable<SktSoLieuChiTietMLNSBudget> FindForFillBudget(EstimationVoucherDetailCriteria condition, string procedure)
        {
            return _sktSoLieuRepository.FindForFillBudget(condition, procedure);
        }

        public IEnumerable<NsDtdauNamChungTuChiTiet> FindDataDonViLoai0ByCondition(int namLamViec, string loaiChungTu, string idDonVi)
        {
            return _sktSoLieuRepository.FindDataDonViLoai0ByCondition(namLamViec, loaiChungTu, idDonVi);
        }

        public IEnumerable<ReportDuToanDauNamTongHopQuery> GetDataReportDuToanDauNamTongHop(int namLamViec, string idDonvi, string loaiChungTu, int loaiNNS, double donViTinh)
        {
            return _sktSoLieuRepository.GetDataReportDuToanDauNamTongHop(namLamViec, idDonvi, loaiChungTu, loaiNNS, donViTinh);
        }

        public IEnumerable<ReportDuToanDauNamTongHopQuery> GetDataReportDuToanDauNamTongHop_2(int namLamViec, string idDonvi, string loaiChungTu, int loaiNNS, double donViTinh)
        {
            return _sktSoLieuRepository.GetDataReportDuToanDauNamTongHop_2(namLamViec, idDonvi, loaiChungTu, loaiNNS, donViTinh);
        }


        public IEnumerable<ReportDuToanDauNamTongHopQuery> GetDataReportDuToanDauNamTongHop_1(int namLamViec, string idDonvi, string loaiChungTu, int loaiNNS, double donViTinh, bool isInTheoTongHop)
        {
            return _sktSoLieuRepository.GetDataReportDuToanDauNamTongHop_1(namLamViec, idDonvi, loaiChungTu, loaiNNS, donViTinh, isInTheoTongHop);
        }

        public IEnumerable<NsMucLucNganSach> GetParentReportTongHop(int namLamViec, string xauNoiMa)
        {
            return _sktSoLieuRepository.GetParentReportTongHop(namLamViec, xauNoiMa);
        }

        public bool IsLockDonViStatus(string idDonVi, int namLamViec, string loaiChungTu, int namNganSach, int nguonNganSach)
        {
            return _sktSoLieuRepository.IsLockDonViStatus(idDonVi, namLamViec, loaiChungTu, namNganSach, nguonNganSach);
        }

        public void UnLockDataReportTotal(int namLamViec, int namNganSach, int nguonNganSach, int loai, int typeGet, string idDonVi, string loaiChungTu)
        {
            _sktSoLieuRepository.UnLockDataReportTotal(namLamViec, namNganSach, nguonNganSach, loai, typeGet, idDonVi, loaiChungTu);
        }

        public int Update(NsDtdauNamChungTuChiTiet entity)
        {
            return _sktSoLieuRepository.Update(entity);
        }

        public int UpdateRange(List<NsDtdauNamChungTuChiTiet> entity)
        {
            return _sktSoLieuRepository.UpdateRange(entity);
        }

        public IEnumerable<NsMucLucNganSach> GetParentReportByLNS(int namLamViec, string lns)
        {
            return _sktSoLieuRepository.GetParentReportByLNS(namLamViec, lns);
        }

        public IEnumerable<ReportDuToanDauNamSoSanhQuery> GetDataReportDuToanDauNamSoSanh(string loai, string idDonvi, int namLamViec, double donViTinh, string loaiChungTu)
        {
            return _sktSoLieuRepository.GetDataReportDuToanDauNamSoSanh(loai, idDonvi, namLamViec, donViTinh, loaiChungTu);
        }

        public IEnumerable<ReportDuToanDauNamSoSanhQuery> GetDataReportDuToanDauNamSoSanh_1(string loai, string idDonvi, int namLamViec, double donViTinh, string loaiChungTu, string lns = "")
        {
            return _sktSoLieuRepository.GetDataReportDuToanDauNamSoSanh_1(loai, idDonvi, namLamViec, donViTinh, loaiChungTu, lns);
        }

        public IEnumerable<ReportDuToanDauNamSoSanhQuery> GetDataReportDuToanDauNamSoSanhAll(string loai, string idDonvi, int namLamViec, double donViTinh, string loaiChungTu)
        {
            return _sktSoLieuRepository.GetDataReportDuToanDauNamSoSanhAll(loai, idDonvi, namLamViec, donViTinh, loaiChungTu);
        }

        public IEnumerable<ReportDuToanDauNamSoSanhQuery> GetDataReportDuToanDauNamSoSanhAll_1(string loai, string idDonvi, int namLamViec, double donViTinh, string loaiChungTu, string lns = "")
        {
            return _sktSoLieuRepository.GetDataReportDuToanDauNamSoSanhAll_1(loai, idDonvi, namLamViec, donViTinh, loaiChungTu, lns);
        }

        public IEnumerable<NsDtdauNamChungTuChiTiet> FindByCondition(Expression<Func<NsDtdauNamChungTuChiTiet, bool>> predicate)
        {
            return _sktSoLieuRepository.FindAll(predicate);
        }

        public List<SktSoLieuChiTietMlnsQuery> GetDataReportChiNganSach(int namLamViec, int namNganSach, int nguonNganSach, string idDonVi, string loaiChungTu)
        {
            return _sktSoLieuRepository.GetDataReportChiNganSach(namLamViec, namNganSach, nguonNganSach, idDonVi, loaiChungTu);
        }

        public void GetHeaderReportChiNganSach(string kyHieu, int namLamViec, ref string header1, ref string header2)
        {
            _sktSoLieuRepository.GetHeaderReportChiNganSach(kyHieu, namLamViec, ref header1, ref header2);
        }

        public int RemoveRange(IEnumerable<NsDtdauNamChungTuChiTiet> sktChungTuChiTiets)
        {
            return _sktSoLieuRepository.RemoveRange(sktChungTuChiTiets);
        }

        public void DeleteByVoucherId(Guid voucherId)
        {
            _sktSoLieuRepository.DeleteByVoucherId(voucherId);
        }

        public IEnumerable<SktSoLieuChiTietMlnsQuery> FindByConditionDonVi0ChiTietDonVi(int namLamViec, int namNganSach, int nguonNganSach, int loai, int typeGet, string idDonVi, string loaiChungTu, string listChungTuTongHop, string lns)
        {
            return _sktSoLieuRepository.FindByConditionDonVi0ChiTietDonVi(namLamViec, namNganSach, nguonNganSach, loai, typeGet, idDonVi, loaiChungTu, listChungTuTongHop, lns);
        }

        public void CreateDataReportTotalSummary(string id, int namLamViec, int namNganSach, int nguonNganSach, int loai, int typeGet, string idDonVi, string loaiChungTu, string listChungTuTongHop, string nguoiTao)
        {
            _sktSoLieuRepository.CreateDataReportTotalSummary(id, namLamViec, namNganSach, nguonNganSach, loai, typeGet, idDonVi, loaiChungTu, listChungTuTongHop, nguoiTao);
        }

        public IEnumerable<CanCuDuToanNamTruocQuery> FindCanCuSoLapDuToanDauNam(int loaiChungTu, int loai,
            string idDonVi, int namLamViec, int namNganSach, int nguonNganSach)
        {
            return _sktSoLieuRepository.FindCanCuSoLapDuToanDauNam(loaiChungTu, loai, idDonVi, namLamViec, namNganSach, nguonNganSach);
        }

        public IEnumerable<ReportChungTuDacThuDauNamPhanCapQuery> GetDataBaoCaoDuToanPhanBoNganSachDacThuPhanCap(List<string> listNganh, int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu, string maDonVi, bool IsInTongHop)
        {
            return _sktSoLieuRepository.GetDataBaoCaoDuToanPhanBoNganSachDacThuPhanCap(listNganh, namLamViec, namNganSach, nguonNganSach, loaiChungTu, maDonVi, IsInTongHop);
        }

        public void BulkInsertNsDtdauNamChungTuChiTiet(List<NsDtdauNamChungTuChiTiet> lstData)
        {
            _sktSoLieuRepository.BulkInsert(lstData);
        }

        public IEnumerable<ReportDuToanDauNamTheoNganhPhuLucQuery> FindReportDuToanDauNamTheoNganhPhuLuc(string nganh, string idDonVi, int namLamViec, int namNganSach, int nguonNganSach, int loai, int donViTinh, bool bTongHop)
        {
            return _sktSoLieuRepository.FindReportDuToanDauNamTheoNganhPhuLuc(nganh, idDonVi, namLamViec, namNganSach, nguonNganSach, loai, donViTinh, bTongHop);
        }

        public IEnumerable<ReportDuToanDauNamTheoNganhPhuLucQuery> FindReportDuToanDauNamTheoNganhPhuLuc(string nganh, string idDonVi, string lstIdChungTu, int namLamViec, int namNganSach, int nguonNganSach, int loai, int donViTinh, bool bTongHop)
        {
            return _sktSoLieuRepository.FindReportDuToanDauNamTheoNganhPhuLuc(nganh, idDonVi, lstIdChungTu, namLamViec, namNganSach, nguonNganSach, loai, donViTinh, bTongHop);
        }
        public IEnumerable<ReportDuToanDauNamTheoNganhPhuLucQuery> FindReportDuToanDauNamPhanCapTheoNganh(string nganh, string idDonVi, string lstIdChungTu, int namLamViec, int namNganSach, int nguonNganSach, int loai, int donViTinh, bool bTongHop)
        {
            return _sktSoLieuRepository.FindReportDuToanDauNamPhanCapTheoNganh(nganh, idDonVi, lstIdChungTu, namLamViec, namNganSach, nguonNganSach, loai, donViTinh, bTongHop);
        }
        public IEnumerable<ReportDuToanDauNamTongHopQuery> GetDataReportDuToanDauNamTongHop_TatCa(int namLamViec, string idDonvi, string loaiChungTu, int loaiNNS, double donViTinh, bool isInTheoTongHop)
        {
            return _sktSoLieuRepository.GetDataReportDuToanDauNamTongHop_TatCa(namLamViec, idDonvi, loaiChungTu, loaiNNS, donViTinh, isInTheoTongHop);
        }

        public IEnumerable<ReportBudgetEstimateQuery> ExportDuToanNganSach(int namLamViec, int namNganSach, int nguonNganSach, int maNguonNS, string maDonVi, int donViTinh)
        {
            return _sktSoLieuRepository.ExportDuToanNganSach(namLamViec, namNganSach, nguonNganSach, maNguonNS, maDonVi, donViTinh);
        }

        public IEnumerable<ReportBudgetEstimateQuery> ExportSoSanhSKTDTDN(int namLamViec, int namNganSach, int nguonNganSach, int maNguonNS, string maDonVi, int loaiChungTu, bool inTheoTongHop, int donViTinh)
        {
            return _sktSoLieuRepository.ExportSoSanhSKTDTDN(namLamViec, namNganSach, nguonNganSach, maNguonNS, maDonVi, loaiChungTu, inTheoTongHop, donViTinh);
        }
        public IEnumerable<ReportBudgetEstimateQuery> ExportDuToanNganSachDonViNgang(int namLamViec, int namNganSach, int nguonNganSach, int maNguonNS, string maDonVi, int donViTinh)
        {
            return _sktSoLieuRepository.ExportDuToanNganSachDonViNgang(namLamViec, namNganSach, nguonNganSach, maNguonNS, maDonVi, donViTinh);
        }

        public IEnumerable<ReportBudgetEstimateQuery> ExportDuToanNganSachDonViNgangExcel(int namLamViec, int namNganSach, int nguonNganSach, int maNguonNS, string maDonVi, int donViTinh)
        {
            return _sktSoLieuRepository.ExportDuToanNganSachDonViNgangExcel(namLamViec, namNganSach, nguonNganSach, maNguonNS, maDonVi, donViTinh);
        }

        public IEnumerable<ReportBudgetEstimateQuery> ExportDuToanNganSachNSDTN(int namLamViec, int namNganSach, int nguonNganSach, int maNguonNS, string maDonVi, int donViTinh)
        {
            return _sktSoLieuRepository.ExportDuToanNganSachNSDTN(namLamViec, namNganSach, nguonNganSach, maNguonNS, maDonVi, donViTinh);
        }

        public IEnumerable<ReportBudgetEstimateQuery> ExportDuToanNganSachDonViNgangNSDTN(int namLamViec, int namNganSach, int nguonNganSach, int maNguonNS, string maDonVi, int donViTinh)
        {
            return _sktSoLieuRepository.ExportDuToanNganSachDonViNgangNSDTN(namLamViec, namNganSach, nguonNganSach, maNguonNS, maDonVi, donViTinh);
        }

        public IEnumerable<ReportBudgetEstimateQuery> ExportDuToanNganSachDonViNgangNSDTNExcel(int namLamViec, int namNganSach, int nguonNganSach, int maNguonNS, string maDonVi, int donViTinh)
        {
            return _sktSoLieuRepository.ExportDuToanNganSachDonViNgangNSDTNExcel(namLamViec, namNganSach, nguonNganSach, maNguonNS, maDonVi, donViTinh);
        }
    }
}
