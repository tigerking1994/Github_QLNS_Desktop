using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class QttBHXHService : IQttBHXHService
    {
        private readonly IQttBHXHRepository _repository;
        public QttBHXHService(IQttBHXHRepository iQttBHXHRepository)
        {
            _repository = iQttBHXHRepository;
        }

        public void Add(BhQttBHXH chungTu)
        {
            _repository.Add(chungTu);
        }

        public int GetVoucherIndex(int year)
        {
            return _repository.GetVoucherIndex(year);
        }

        public int Delete(BhQttBHXH item)
        {
            return _repository.Delete(item);
        }

        public IEnumerable<BhQttBHXH> FindAggregateVoucher(string sct, int namLamViec)
        {
            return _repository.FindAggregateVoucher(sct, namLamViec);
        }

        public IEnumerable<BhQttBHXHQuery> FindByCondition(int namLamViec)
        {
            return _repository.FindByCondition(namLamViec);
        }

        public BhQttBHXH FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public List<string> FindLNSExist(BhQttBHXHChiTietCriteria condition, Guid voucherId, List<string> listLNSSelected)
        {
            var predicate = PredicateBuilder.True<BhQttBHXH>();
            predicate = predicate.And(x => x.INamLamViec == condition.INamLamViec && x.IIDMaDonVi == condition.IIDMaDonVi
                        && x.IQuyNam == condition.IQuyNam && x.IQuyNamLoai == condition.IQuyNamLoai);
            if (voucherId != Guid.Empty)
                predicate = predicate.And(x => x.Id != voucherId);

            List<string> listLNSExist = new List<string>();
            List<BhQttBHXH> chungTus = _repository.FindAll(predicate).ToList();
            chungTus.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x.SDsMlns))
                    listLNSExist.AddRange(x.SDsMlns.Split(','));
            });

            return listLNSSelected.Where(x => listLNSExist.Contains(x)).ToList();
        }

        public bool IsExistAggregateVoucher(int namLamViec)
        {
            return _repository.IsExistAggregateVoucher(namLamViec);
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            var voucher = _repository.Find(id);
            voucher.BIsKhoa = isLock;
            _repository.Update(voucher);
        }

        public int Update(BhQttBHXH item)
        {
            return _repository.Update(item);
        }

        public void Delete(Guid id)
        {
            BhQttBHXH chungTu = _repository.Find(id);
            _repository.Delete(chungTu);
        }

        public IEnumerable<BhQttQuarterQuery> GetQuarterYearByYear(int namLamViec)
        {
            return _repository.GetQuarterYearByYear(namLamViec);
        }

        public List<int> GetVoucherYears(int year)
        {
            return _repository.GetVoucherYears(year);
        }

        public List<string> FindVoucherLNSExist(BhQttBHXHChiTietCriteria condition, Guid voucherId, int loaiChungTu)
        {
            var predicate = PredicateBuilder.True<BhQttBHXH>();
            predicate = predicate.And(x => x.INamLamViec == condition.INamLamViec && x.IIDMaDonVi == condition.IIDMaDonVi
                        && x.IQuyNam == condition.IQuyNam && x.IQuyNamLoai == condition.IQuyNamLoai && x.ILoaiTongHop == loaiChungTu);
            if (voucherId != Guid.Empty)
                predicate = predicate.And(x => x.Id != voucherId);
            List<BhQttBHXH> chungTus = _repository.FindAll(predicate).ToList();
            List<string> lstLNS = new List<string> { "9020001", "9020002" };
            if (chungTus != null && chungTus.Count > 0)
            {
                return lstLNS;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<BhQttBHXH> FindChungTuDaTongHopBySCT(string sct, int namLamViec)
        {
            return _repository.FindChungTuDaTongHopBySCT(sct, namLamViec);
        }

        public IEnumerable<BhQttBHXH> FindByCondition(Expression<Func<BhQttBHXH, bool>> condition)
        {
            return _repository.FindAll(condition);
        }
        public IEnumerable<BhQttBHXH> FindByCondition(string sIdDonVi, int namLamViec, int selectedQuarter, int selectedQuarterType, bool isDonViCha)
        {
            return _repository.FindByCondition(sIdDonVi, namLamViec, selectedQuarter, selectedQuarterType, isDonViCha);
        }
        public IEnumerable<BhQttBHXH> FindByCondition(int namLamViec, int quyNam, int quyNamLoai, int loaiChungTu)
        {
            return _repository.FindByCondition(namLamViec, quyNam, quyNamLoai, loaiChungTu);
        }
        public IEnumerable<BhQttBHXHQuery> FindAllChungTuDonVi(int namLamViec, int quyNam)
        {
            return _repository.FindAllChungTuDonVi(namLamViec, quyNam);
        }

        public IEnumerable<BhQttBHXHQuery> FindChungTuDonViTongHop(int namLamViec, int loaiTongHop, string userName, int quyNam, int loaiQuy)
        {
            return _repository.FindChungTuDonViTongHop(namLamViec, loaiTongHop, userName, quyNam, loaiQuy);
        }

        public IEnumerable<BhQttBHXHQuery> FindChungTuDonVi(int namLamViec, int loaiTongHop, bool bDaTongHop, int quyNam, int loaiQuy)
        {
            return _repository.FindChungTuDonVi(namLamViec, loaiTongHop, bDaTongHop, quyNam, loaiQuy);
        }

        public IEnumerable<BhQttBHXHQuery> GetAggregateParentUnit(int iNamLamViec, string sIdDonVi, int selectedQuarter)
        {
            return _repository.GetAggregateParentUnit(iNamLamViec, sIdDonVi, selectedQuarter);
        }

        public List<string> FindCurrentUnits(int namLamViec, int selectedQuarter, int loaiQuy, bool isInBudget)
        {
            return _repository.FindCurrentUnits(namLamViec, selectedQuarter, loaiQuy, isInBudget);
        }

        public IEnumerable<BhQttBHXHQuery> FindChungTuDonViThangQuy(int namLamViec, int loaiTongHop, bool bDaTongHop, int quyNam, int loaiQuy)
        {
            return _repository.FindChungTuDonViThangQuy(namLamViec, loaiTongHop, bDaTongHop, quyNam, loaiQuy);
        }

        public bool HasMonthlyVouchers(int iNamLamViec, int iQuy, int iLoai, bool isLuyKe, string sMaDonVi)
        {
            return _repository.HasMonthlyVouchers(iNamLamViec, iQuy, iLoai, isLuyKe, sMaDonVi);
        }

        public IEnumerable<BhQttBHXHQuery> FindChungTuDonViTongHopThangQuy(int namLamViec, int loaiTongHop, string userName, int quyNam, int loaiQuy)
        {
            return _repository.FindChungTuDonViTongHopThangQuy(namLamViec, loaiTongHop, userName, quyNam, loaiQuy);
        }

        public int GetNumOfMonthlyVoucher(int year, string sMaDonVi, bool isLuyKe, int iquy, int iLoaiQuy)
        {
            return _repository.GetNumOfMonthlyVoucher(year, sMaDonVi, isLuyKe, iquy, iLoaiQuy);
        }
    }
}
