using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ISktSoLieuRepository : IRepository<NsDtdauNamChungTuChiTiet>
    {
        IEnumerable<SktSoLieuChiTietMlnsQuery> FindByCondition(int namLamViec, int namNganSach, int nguonNganSach, int loai, int typeGet, string idDonVi, string loaiChungTu);
        IEnumerable<SktSoLieuChiTietMlnsQuery> FindByCondition(int namLamViec, int namNganSach, int nguonNganSach, int loai, int typeGet, string idDonVi, string loaiChungTu, string lns, string voucherId);
        bool IsLockDonViStatus(string idDonVi, int namLamViec, string loaiChungTu, int namNganSach, int nguonNganSach);
        void CreateDataReportTotal(int namLamViec, int namNganSach, int nguonNganSach, int loai, int typeGet, string idDonVi, string loaiChungTu, string listDonViTongHop, string nguoiTao);
        void CreateDataReportTotalSummary(string id, int namLamViec, int namNganSach, int nguonNganSach, int loai, int typeGet, string idDonVi, string loaiChungTu, string listChungTuTongHop, string nguoiTao);
        void UnLockDataReportTotal(int namLamViec, int namNganSach, int nguonNganSach, int loai, int typeGet, string idDonVi, string loaiChungTu);
        IEnumerable<SktSoLieuChiTietMlnsQuery> FindByConditionDonVi0(int namLamViec, int namNganSach, int nguonNganSach, int loai, int typeGet, string idDonVi, string loaiChungTu, string listIdChungTu, string lns);
        IEnumerable<SktSoLieuChiTietMlnsQuery> FindByConditionDonVi0ChiTietDonVi(int namLamViec, int namNganSach, int nguonNganSach, int loai, int typeGet, string idDonVi, string loaiChungTu, string listChungTuTongHop, string lns);
        IEnumerable<SktSoLieuChiTietMLNSBudget> FindForFillBudget(EstimationVoucherDetailCriteria condition, string procedure);
        IEnumerable<ReportDuToanDauNamTongHopQuery> GetDataReportDuToanDauNamTongHop(int namLamViec, string idDonvi, string loaiChungTu, int loaiNNS, double donViTinh);
        IEnumerable<ReportDuToanDauNamTongHopQuery> GetDataReportDuToanDauNamTongHop_2(int namLamViec, string idDonvi, string loaiChungTu, int loaiNNS, double donViTinh);
        IEnumerable<ReportDuToanDauNamTongHopQuery> GetDataReportDuToanDauNamTongHop_1(int namLamViec, string idDonvi, string loaiChungTu, int loaiNNS, double donViTinh, bool isInTheoTongHop);
        IEnumerable<NsMucLucNganSach> GetParentReportTongHop(int namLamViec, string xauNoiMa);
        IEnumerable<NsDtdauNamChungTuChiTiet> FindDataDonViLoai0ByCondition(int namLamViec, string loaiChungTu, string idDonVi);
        IEnumerable<NsMucLucNganSach> GetParentReportByLNS(int namLamViec, string lns);
        IEnumerable<ReportDuToanDauNamSoSanhQuery> GetDataReportDuToanDauNamSoSanh(string loai, string idDonvi, int namLamViec, double donViTinh, string loaiChungTu);
        IEnumerable<ReportDuToanDauNamSoSanhQuery> GetDataReportDuToanDauNamSoSanh_1(string loai, string idDonvi, int namLamViec, double donViTinh, string loaiChungTu, string lns);
        IEnumerable<ReportDuToanDauNamSoSanhQuery> GetDataReportDuToanDauNamSoSanhAll(string loai, string idDonvi, int namLamViec, double donViTinh, string loaiChungTu);
        IEnumerable<ReportDuToanDauNamSoSanhQuery> GetDataReportDuToanDauNamSoSanhAll_1(string loai, string idDonvi, int namLamViec, double donViTinh, string loaiChungTu, string lns);
        List<SktSoLieuChiTietMlnsQuery> GetDataReportChiNganSach(int namLamViec, int namNganSach, int nguonNganSach, string idDonVi, string loaiChungTu);
        void GetHeaderReportChiNganSach(string kyHieu, int namLamViec, ref string header1, ref string header2);
        void DeleteByVoucherId(Guid voucherId);
        IEnumerable<CanCuDuToanNamTruocQuery> FindCanCuSoLapDuToanDauNam(int loaiChungTu, int loai, string idDonVi,
            int namLamViec, int namNganSach, int nguonNganSach);
        IEnumerable<ReportChungTuDacThuDauNamPhanCapQuery> GetDataBaoCaoDuToanPhanBoNganSachDacThuPhanCap(List<string> listNganh, int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu, string maDonVi, bool IsInTongHop);
        IEnumerable<ReportDuToanDauNamTheoNganhPhuLucQuery> FindReportDuToanDauNamTheoNganhPhuLuc(string nganh, string idDonVi, int namLamViec, int namNganSach, int nguonNganSach, int loai, int donViTinh, bool bTongHop);
        IEnumerable<ReportDuToanDauNamTheoNganhPhuLucQuery> FindReportDuToanDauNamTheoNganhPhuLuc(string nganh, string idDonVi, string lstIdChungTu, int namLamViec, int namNganSach, int nguonNganSach, int loai, int donViTinh, bool bTongHop);
        IEnumerable<ReportDuToanDauNamTheoNganhPhuLucQuery> FindReportDuToanDauNamPhanCapTheoNganh(string nganh, string idDonVi, string lstIdChungTu, int namLamViec, int namNganSach, int nguonNganSach, int loai, int donViTinh, bool bTongHop);
        IEnumerable<ReportDuToanDauNamTongHopQuery> GetDataReportDuToanDauNamTongHop_TatCa(int namLamViec, string idDonvi, string loaiChungTu, int loaiNNS, double donViTinh, bool isInTheoTongHop);
        IEnumerable<ReportBudgetEstimateQuery> ExportDuToanNganSach(int namLamViec, int namNganSach, int nguonNganSach, int maNguonNS, string maDonVi, int donViTinh);
        IEnumerable<ReportBudgetEstimateQuery> ExportSoSanhSKTDTDN(int namLamViec, int namNganSach, int nguonNganSach, int maNguonNS, string maDonVi, int loaiChungTu, bool inTheoTongHop, int donViTinh);
        IEnumerable<ReportBudgetEstimateQuery> ExportDuToanNganSachDonViNgang(int namLamViec, int namNganSach, int nguonNganSach, int maNguonNS, string maDonVi, int donViTinh);
        IEnumerable<ReportBudgetEstimateQuery> ExportDuToanNganSachDonViNgangExcel(int namLamViec, int namNganSach, int nguonNganSach, int maNguonNS, string maDonVi, int donViTinh);
        IEnumerable<ReportBudgetEstimateQuery> ExportDuToanNganSachNSDTN(int namLamViec, int namNganSach, int nguonNganSach, int maNguonNS, string maDonVi, int donViTinh);
        IEnumerable<ReportBudgetEstimateQuery> ExportDuToanNganSachDonViNgangNSDTN(int namLamViec, int namNganSach, int nguonNganSach, int maNguonNS, string maDonVi, int donViTinh);
        IEnumerable<ReportBudgetEstimateQuery> ExportDuToanNganSachDonViNgangNSDTNExcel(int namLamViec, int namNganSach, int nguonNganSach, int maNguonNS, string maDonVi, int donViTinh);
    }
}
