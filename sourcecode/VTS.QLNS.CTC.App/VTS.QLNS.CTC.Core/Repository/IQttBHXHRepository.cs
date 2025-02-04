using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IQttBHXHRepository : IRepository<BhQttBHXH>
    {
        IEnumerable<BhQttBHXH> FindAggregateVoucher(string sct, int namLamViec);
        bool IsExistAggregateVoucher(int namLamViec);
        IEnumerable<BhQttBHXHQuery> FindByCondition(int namLamViec);
        int GetVoucherIndex(int year);
        IEnumerable<BhQttQuarterQuery> GetQuarterYearByYear(int namLamViec);
        List<int> GetVoucherYears(int year);
        IEnumerable<BhQttBHXH> FindChungTuDaTongHopBySCT(string sct, int namLamViec);
        IEnumerable<BhQttBHXH> FindByCondition(string sIdDonVi, int namLamViec, int selectedQuarter, int selectedQuarterType, bool isDonViCha);
        IEnumerable<BhQttBHXH> FindByCondition(int namLamViec, int quyNamLoai, int quyNam, int loaiChungTu);
        IEnumerable<BhQttBHXHQuery> FindChungTuDonVi(int namLamViec, int loaiTongHop, bool bDaTongHop, int quyNam, int loaiQuy);
        IEnumerable<BhQttBHXHQuery> FindChungTuDonViTongHop(int namLamViec, int loaiTongHop, string userName, int quyNam, int loaiQuy);
        IEnumerable<BhQttBHXHQuery> GetAggregateParentUnit(int iNamLamViec, string sIdDonVi, int selectedQuarter);
        IEnumerable<BhQttBHXHQuery> FindAllChungTuDonVi(int namLamViec, int quyNam);
        List<string> FindCurrentUnits(int namLamViec, int selectedQuarter, int loaiQuy, bool isInBudget);
        IEnumerable<BhQttBHXHQuery> FindChungTuDonViThangQuy(int namLamViec, int loaiTongHop, bool bDaTongHop, int quyNam, int loaiQuy);
        bool HasMonthlyVouchers(int iNamLamViec, int iQuy, int iLoai, bool isLuyKe, string sMaDonVi);
        IEnumerable<BhQttBHXHQuery> FindChungTuDonViTongHopThangQuy(int namLamViec, int loaiTongHop, string userName, int quyNam, int loaiQuy);
        int GetNumOfMonthlyVoucher(int year, string sMaDonVi, bool isLuyKe, int iquy, int iLoaiQuy);
    }
}
