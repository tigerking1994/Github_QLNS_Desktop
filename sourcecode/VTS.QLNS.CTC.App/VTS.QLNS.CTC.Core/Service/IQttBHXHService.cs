using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IQttBHXHService
    {
        BhQttBHXH FindById(Guid id);
        int Delete(BhQttBHXH item);
        void Delete(Guid id);
        IEnumerable<BhQttBHXH> FindAggregateVoucher(string sct, int namLamViec);
        int Update(BhQttBHXH item);
        bool IsExistAggregateVoucher(int namLamViec);
        IEnumerable<BhQttBHXHQuery> FindByCondition(int namLamViec);
        void LockOrUnlock(Guid id, bool isLock);
        List<string> FindLNSExist(BhQttBHXHChiTietCriteria condition, Guid voucherId, List<string> listLNSSelected);
        void Add(BhQttBHXH chungTu);
        int GetVoucherIndex(int year);
        IEnumerable<BhQttQuarterQuery> GetQuarterYearByYear(int namLamViec);
        List<int> GetVoucherYears(int year);
        List<string> FindVoucherLNSExist(BhQttBHXHChiTietCriteria condition, Guid voucherId, int loaiChungTu);
        IEnumerable<BhQttBHXH> FindChungTuDaTongHopBySCT(string sct, int namLamViec);
        IEnumerable<BhQttBHXH> FindByCondition(Expression<Func<BhQttBHXH, bool>> condition);
        IEnumerable<BhQttBHXH> FindByCondition(string sIdDonVi, int namLamViec, int selectedQuarter, int selectedQuarterType, bool isDonViCha);
        IEnumerable<BhQttBHXH> FindByCondition(int namLamViec, int quyNam, int quyNamLoai, int loaiChungTu);
        IEnumerable<BhQttBHXHQuery> FindChungTuDonVi(int namLamViec, int loaiTongHop, bool bDaTongHop, int quyNam, int loaiQuy);
        IEnumerable<BhQttBHXHQuery> FindAllChungTuDonVi(int namLamViec, int quyNam);
        IEnumerable<BhQttBHXHQuery> FindChungTuDonViTongHop(int namLamViec, int loaiTongHop, string userName, int quyNam, int loaiQuy);
        IEnumerable<BhQttBHXHQuery> GetAggregateParentUnit(int iNamLamViec, string sIdDonVi, int selectedQuarter);
        List<string> FindCurrentUnits(int namLamViec, int selectedQuarter, int loaiQuy, bool isInBudget);
        IEnumerable<BhQttBHXHQuery> FindChungTuDonViThangQuy(int namLamViec, int loaiTongHop, bool bDaTongHop, int quyNam, int loaiQuy);
        bool HasMonthlyVouchers(int iNamLamViec, int iQuy, int iLoai, bool isLuyKe, string sMaDonVi);
        IEnumerable<BhQttBHXHQuery> FindChungTuDonViTongHopThangQuy(int namLamViec, int loaiTongHop, string userName, int quyNam, int loaiQuy);
        int GetNumOfMonthlyVoucher(int year, string sMaDonVi, bool isLuyKe, int iquy, int iLoaiQuy);
    }
}
