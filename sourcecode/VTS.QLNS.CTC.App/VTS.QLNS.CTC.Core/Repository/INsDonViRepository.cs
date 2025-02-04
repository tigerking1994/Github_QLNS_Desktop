using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INsDonViRepository : IRepository<DonVi>
    {
        IEnumerable<DonVi> FindByNamLamViec(int namLamViec);
        IEnumerable<DonVi> FindAllDataDonVi();
        DonVi FindByIdDonVi(string idDonVi, int namLamViec);
        IEnumerable<DonVi> FindByListIdDonVi(string idsDonVi, int namLamViec);
        IEnumerable<DonVi> FindByCondition(int loai, int trangThai, int namLamViec);
        IEnumerable<DonVi> FindByListIdDonVi(string listIdDonVi);
        IEnumerable<DonVi> FindByListIdDonVi(IEnumerable<string> listIdDonVi, int namLamViec);
        IEnumerable<DonViPlanBeginYearQuery> FindPlanBeginYearByConditon(int namLamViec, int namNganSach, int nguonNganSach, string loai, int loaiNNS, string userName);
        IEnumerable<DonViPlanBeginYearQuery> FindPlanBeginYearAgencyByConditon(int namLamViec, int namNganSach, int nguonNganSach, string loai, string userName);
        DonVi FindById(Guid idDonVi);
        IEnumerable<DonViNgayChungTuQuery> FindByNamLamViecHasCapPhatChiTiet(int namLamViec);
        IEnumerable<DonVi> FindAllChildByIdDonVi(string idDonVi, int namLamViec);
        DonVi FindByLoai(string loai, int namLamViec);
        bool IsDonViCha(string maDonVi, int namLamViec);
        IEnumerable<string> FindChildNsDonVi(string idDonVi, int yearOfWork, int status);
        IEnumerable<DonVi> FindBySettlementMonth(int yearOfWork, int yearOfBudget, int budgetSource, string quarterMonth, int quarterMonthType, string loaiQuyetToan);
        IEnumerable<DonVi> FindDonViHasDataSktSoLieuChiTiet(int namLamViec, int namNganSach, int nguonNganSach, string loaiChungTu, int loaiNNS);
        IEnumerable<DonViNgayChungTuQuery> FindByNgayChungTu(int namLamViec, DateTime ngayChungTu, bool isLuyKe);
        IEnumerable<DonVi> FindByEstimateSettlement(int yearOfWork, int yearOfBudget, int budgetSource, DateTime voucherDate, int quarterMonth, int quarterMonthType);
        DonVi FirstOrDefault(Func<DonVi, bool> condition);
        IEnumerable<DonVi> FindByLoai(int namLamViec, string loai);
        IEnumerable<DonVi> FindForReceiveSettlementReport(int yearOfWork, string yearOfBudget, int budgetSource, string lns);
        IEnumerable<DonVi> FindBySettlement(int yearOfWork, int budgetSource, int dataType, string lns);
        IEnumerable<DonVi> FindBySettlement(int yearOfWork, int budgetSource, int dataType, string lns, string type);
        List<DonVi> GetDanhSachDonViByNguoiDung(string sMaNguoiDung, int iNamLamViec);
        IEnumerable<DonVi> FindBySummaryVoucherList(int yearOfWork, int quarterMonth, string lns);
        IEnumerable<DonViQuery> FindAllDonViNotDuplicate();
        int countNsDonViByNamLamViec(int namLamViec);
        IEnumerable<NSDonViExportQuery> GetDonViExportByNamLamViec(int iNamLamViec);
        IEnumerable<DonVi> FindByUser(string userName, int namLamViec, string type);
        IEnumerable<DonVi> FindByUserCreateVoucher(string userName, int namLamViec, string type);
        IEnumerable<DonVi> FindForEstimateDivisionReport(int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu, string idChungTu, bool isLuyKe);
        IEnumerable<DonVi> FindForRevenueExpenditureDivisionReport(int namLamViec, int namNganSach, int nguonNganSach, string idChungTu, bool isLuyKe);
        IEnumerable<DonVi> FindForSocialInsuranceEstimateDivisionReport(int namLamViec, string idChungTu);
        void SaveDonViSuDung(DonVi donVi, int namLamViec);
        DonVi FindByMaDonViAndNamLamViec(string maDonVi, int namLamViec);
        int CountDonVi();
        IEnumerable<DonVi> FindByQtTongHopQuy(int yearOfWork, string yearOfBudget, int budgetSource, string quarterMonth, string khoi, string loaiQuyetToan, string userName);
        IEnumerable<DonVi> FindByCapPhatId(int yearOfWork, string listCapPhatId);
        IEnumerable<DonVi> FindByCapPhatIdForBH(int yearOfWork, string listCapPhatId);
        IEnumerable<DonVi> FindByDonViOfAllocationPlanForBH(int yearOfWork, string listMaDonVi, int iQuy);
        IEnumerable<DonVi> FindByDonViOfAllocationTongHopForBH(int yearOfWork, int quy, Guid idLoaiChi);
        IEnumerable<DonVi> FindByCapPhatId2(int yearOfWork, string listCapPhatId, int loaiNganSach);
        IEnumerable<DonVi> FindByQuanSo(int yearOfWork, string months);
        IEnumerable<DonVi> FindBySummaryAgencySettlement(int yearOfWork, int yearOfBudget, int budgetSouce, string lns, string quarterMonth, DateTime voucherDate, bool hasDuToan);
        IEnumerable<DonVi> FindHospitalTargetAgencyReportDonVi(int namLamViec, string idChungTu, int loaiChungTu);
        IEnumerable<DonVi> FindForAdjustmentEstimateReport(int yearOfWork, int yearOfBudget, int budgetSouce, int dot);
        IEnumerable<DonVi> FindForSocialInsuranceEstimateReport(int yearOfWork, Guid iIDLoaiCap);
        int CountILoaiByNamLamViec(int year, string loai);
        IEnumerable<DonVi> FindByYearAndNhiemVuChi(int namLamViec, bool HasNhiemVuChi = true);
        IEnumerable<DonVi> FindInTongHopSKTBenhVienTuChu(int yearOfWork, int loaiNNS);
        IEnumerable<DonViQuery> FindAllHopDongByDonViId(Guid idHopDong);
        IEnumerable<DonViQuery> FindAllDuAnByDonViId(Guid idDuAn);
        IEnumerable<DonVi> FindByYearAndIDNhiemVuChi(int namLamViec, Guid? IDNhiemVuChi, string sLoaiSoCu = null);
        IEnumerable<DonVi> FindByListDonViCap2KhacCha(int namLamViec);
        IEnumerable<DonVi> FindDonViCoDataSktSoLieuChiTietAllLoai(int namLamViec, int namNganSach, int nguonNganSach, int loaiNNS, int loaiChungtu);
        IEnumerable<DonVi> FindAllDataDonViCurrent(int namLamViec);
        IEnumerable<string> FindAllDonViByBaoCaoThamDinhBH(int namLamViec);
    }
}
