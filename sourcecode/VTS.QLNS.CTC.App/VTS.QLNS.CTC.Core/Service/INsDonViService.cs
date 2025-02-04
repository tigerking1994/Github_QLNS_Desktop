using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INsDonViService
    {
        IEnumerable<DonVi> FindAll();
        IEnumerable<DonVi> FindAll(Expression<Func<DonVi, bool>> predicate);
        DonVi FindById(Guid idDonVi);
        IEnumerable<DonViQuery> FindAllHopDongByDonViId(Guid idHopDong);
        IEnumerable<DonViQuery> FindAllDuAnByDonViId(Guid idDuAn);
        IEnumerable<DonVi> FindAllChildByIdDonVi(string idDonVi, int namLamViec);
        DonVi FindByIdDonVi(string idDonVi, int namLamViec);
        IEnumerable<DonVi> FindInternalByNamLamViec(int namLamViec);
        IEnumerable<DonVi> FindByNamLamViec(int namLamViec);
        IEnumerable<DonVi> FindByAllDataDonVi();
        IEnumerable<DonVi> FindByCondition(Expression<Func<DonVi, bool>> predicate);
        IEnumerable<DonVi> FindByListIdDonVi(string listIdDonVi);
        IEnumerable<DonVi> FindByListIdDonVi(IEnumerable<string> listIdDonVi, int namLamViec);
        IEnumerable<DonVi> FindByListIdDonVi(string idsDonVi, int namLamViec);
        IEnumerable<DonViPlanBeginYearQuery> FindPlanBeginYearByConditon(int namLamViec, int namNganSach, int nguonNganSach, string loai, int loaiNNS, string userName);
        IEnumerable<DonViPlanBeginYearQuery> FindPlanBeginYearAgencyByConditon(int namLamViec, int namNganSach, int nguonNganSach, string loai, string userName);
        IEnumerable<DonViNgayChungTuQuery> FindByNamLamViecHasCapPhatChiTiet(int namLamViec);
        DonVi FindByLoai(string loai, int namLamViec);
        bool IsDonViCha(string maDonVi, int namLamViec);
        IEnumerable<string> FindChildNsDonVi(string idDonVi, int yearOfWork, int status);
        IEnumerable<DonVi> FindByCondition(int estimateDivision, int status, int namLamViec);
        IEnumerable<DonVi> FindBySettlementMonth(int yearOfWork, int yearOfBudget, int budgetSource, string quarterMonth, int quarterMonthType, string loaiQuyetToan);
        IEnumerable<DonVi> FindDonViHasDataSktSoLieuChiTiet(int namLamViec, int namNganSach, int nguonNganSach, string loaiChungTu, int loaiNNS);
        IEnumerable<DonViNgayChungTuQuery> FindByNgayChungTu(int namLamViec, DateTime ngayChungTu, bool isLuyKe);
        IEnumerable<DonVi> FindByEstimateSettlement(int yearOfWork, int yearOfBudget, int budgetSource, DateTime voucherDate, int quarterMonth, int quarterMonthType);
        IEnumerable<DonVi> FindByLoai(int namLamViec, string loai);
        IEnumerable<DonVi> FindForReceiveSettlementReport(int yearOfWork, string yearOfBudget, int budgetSource, string lns);
        IEnumerable<DonVi> FindBySettlement(int yearOfWork, int budgetSource, int dataType, string lns);
        IEnumerable<DonVi> FindBySettlement(int yearOfWork, int budgetSource, int dataType, string lns, string type);
        List<DonVi> GetDanhSachDonViByNguoiDung(string sMaNguoiDung, int iNamLamViec);
        DonVi Find(Guid id);
        IEnumerable<DonVi> FindBySummaryVoucherList(int yearOfWork, int quarterMonth, string lns);
        IEnumerable<DonViQuery> FindAllDonViNotDuplicate();
        int countNsDonViByNamLamViec(int namLamViec);
        IEnumerable<NSDonViExportQuery> GetDonViExportByNamLamViec(int iNamLamViec);
        List<DonVi> FindByUser(string userName, int namLamViec, string type);
        List<DonVi> FindByUserCreateVoucher(string userName, int namLamViec, string type);
        IEnumerable<DonVi> FindForEstimateDivisionReport(int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu, string idChungTu, bool isLuyKe);
        IEnumerable<DonVi> FindForRevenueExpenditureDivisionReport(int namLamViec, int namNganSach, int nguonNganSach, string idChungTu, bool isLuyKe);
        IEnumerable<DonVi> FindForSocialInsuranceEstimateDivisionReport(int namLamViec, string idChungTu);
        void SaveDonViSuDung(DonVi donVi, int namLamViec);
        DonVi FindByMaDonViAndNamLamViec(string maDonVi, int namLamViec);
        void SaveAllDonViCon(IEnumerable<DonVi> items, int yearOfWork);
        DonVi FindCurrentDonViSuDungByNamLamViec(int namLamViec);
        IEnumerable<DonVi> FindDonViConByNamLamViec(int namLamViec);
        void CopyDataToDonViThucHienDuAn(int namLamViec);
        int CountDonVi();
        IEnumerable<DonVi> FindByQtTongHopQuy(int yearOfWork, string yearOfBudget, int budgetSource, string quarterMonth, string khoi, string loaiQuyetToan, string userName);
        IEnumerable<DonVi> FindByCapPhatId(int yearOfWork, string listCapPhatId);
        IEnumerable<DonVi> FindByCapPhatIdForBH(int yearOfWork, string listCapPhatId);
        IEnumerable<DonVi> FindByDonViOfAllocationTongHopForBH(int yearOfWork, int quy, Guid idLoaiChi);
        IEnumerable<DonVi> FindByDonViOfAllocationPlanForBH(int yearOfWork, string listMaDonVi, int iQuy);
        IEnumerable<DonVi> FindByCapPhatId2(int yearOfWork, string listCapPhatId, int loaiNganSach);
        IEnumerable<DonVi> FindByQuanSo(int yearOfWork, string months);
        IEnumerable<DonVi> FindBySummaryAgencySettlement(int yearOfWork, int yearOfBudget, int budgetSouce, string lns, string quarterMonth, DateTime voucherDate, bool hasDuToan);
        IEnumerable<DonVi> FindHospitalTargetAgencyReportDonVi(int namLamViec, string idChungTu, int loaiChungTu);
        IEnumerable<DonVi> FindForAdjustmentEstimateReport(int yearOfWork, int yearOfBudget, int budgetSouce, int dot);
        int CountILoaiByNamLamViec(int year, string type);
        IEnumerable<DonVi> FindByYearAndNhiemVuChi(int namLamViec, bool HasNhiemVuChi = true);
        IEnumerable<DonVi> FindInTongHopSKTBenhVienTuChu(int yearOfWork, int loaiNNS);
        IEnumerable<DonVi> FindByYearAndIDNhiemVuChi(int namLamViec, Guid? IDNhiemVuChi, string sLoaiSoCu = null);
        IEnumerable<DonVi> FindForSocialInsuranceEstimateReport(int yearOfWork, Guid iIDLoaiCap);
        IEnumerable<DonVi> FindByListDonViCap2KhacCha(int namLamViec);
        IEnumerable<DonVi> FindDonViCoDataSktSoLieuChiTietAllLoai(int namLamViec, int namNganSach, int nguonNganSach, int loaiNNS, int loaiChungtu);
        IEnumerable<DonVi> FindAllDataDonViCurrent(int namLamViec);
        IEnumerable<string> FindAllDonViByBaoCaoThamDinhBH(int namLamViec);

    }
}
