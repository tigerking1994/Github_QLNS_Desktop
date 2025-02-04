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
    public class NsQtChungTuService : INsQtChungTuService
    {
        private INsQtChungTuRepository _chungTuRepository;
        private INsQtChungTuChiTietRepository _detailRepository;
        public NsQtChungTuService(INsQtChungTuRepository chungTuRepository,
            INsQtChungTuChiTietRepository detailRepository)
        {
            _chungTuRepository = chungTuRepository;
            _detailRepository = detailRepository;
        }

        /// <summary>
        /// Thêm mới chứng từ
        /// </summary>
        /// <param name="chungTu"></param>
        public void Add(NsQtChungTu chungTu)
        {
            _chungTuRepository.Add(chungTu);
        }

        /// <summary>
        /// Cập nhật chứng từ
        /// </summary>
        /// <param name="chungTu"></param>
        public void Update(NsQtChungTu chungTu)
        {
            _chungTuRepository.Update(chungTu);
        }

        /// <summary>
        /// Lấy ra chứng từ theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NsQtChungTu FindById(Guid id)
        {
            return _chungTuRepository.Find(id);
        }

        /// <summary>
        /// Lấy ra danh sách chứng từ theo loại
        /// </summary>
        /// <param name="type">Loại chứng từ</param>
        /// <returns></returns>
        public List<NsQtChungTu> FindByType(string type)
        {
            return _chungTuRepository.FindByType(type).ToList();
        }

        /// <summary>
        /// Khóa hoặc mở khóa chứng từ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isLock"></param>
        public void LockOrUnlock(Guid id, bool isLock)
        {
            NsQtChungTu chungTu = _chungTuRepository.Find(id);
            chungTu.BKhoa = isLock;
            _chungTuRepository.Update(chungTu);
        }

        /// <summary>
        /// Tìm kiếm chứng từ theo điều kiện
        /// </summary>
        /// <returns></returns>
        public List<NsQtChungTuQuery> FindByCondition(SettlementVoucherCriteria condition)
        {
            return _chungTuRepository.FindByCondition(condition).ToList();
        }

        public IEnumerable<NsQtChungTu> FindByCondition(Expression<Func<NsQtChungTu, bool>> predicate)
        {
            return _chungTuRepository.FindAll(predicate);
        }

        public void BulkInsertNsQtChungTu(List<NsQtChungTu> lstData)
        {
            _chungTuRepository.BulkInsert(lstData);
        }

        /// <summary>
        /// xóa chứng từ theo id
        /// </summary>
        /// <param name="id"></param>
        public void Delete(Guid id)
        {
            NsQtChungTu chungTu = _chungTuRepository.Find(id);
            _chungTuRepository.Delete(chungTu);
        }

        public NsQtChungTu FindAggregateVoucher(string voucherNoes)
        {
            return _chungTuRepository.FindAggregateVoucher(voucherNoes);
        }

        public List<string> FindAgencyIdByMonth(ReportSettlementCriteria condition)
        {
            return _chungTuRepository.FindAgencyIdByMonth(condition);
        }

        public List<string> FindLNSExist(SettlementVoucherCriteria condition, Guid voucherId, List<string> listLNSSelected)
        {
            var predicate = PredicateBuilder.True<NsQtChungTu>();
            predicate = predicate.And(x => x.INamLamViec == condition.YearOfWork && x.INamNganSach == condition.YearOfBudget
                        && x.IIdMaNguonNganSach == condition.BudgetSource && x.IIdMaDonVi == condition.AgencyId
                        && x.ILoaiChungTu == condition.AdjustType
                        && x.IThangQuy == condition.QuarterMonth && x.IThangQuyLoai == condition.QuarterMonthType && x.SLoai == condition.SettlementType);
            if (voucherId != Guid.Empty)
                predicate = predicate.And(x => x.Id != voucherId);

            List<string> listLNSExist = new List<string>();
            List<NsQtChungTu> chungTus = _chungTuRepository.FindAll(predicate).ToList();
            chungTus.ForEach(x =>
            {
                listLNSExist.AddRange(x.SDslns.Split(','));
            });

            return listLNSSelected.Where(x => listLNSExist.Contains(x)).ToList();
        }

        public bool HasSTongHop(SettlementVoucherCriteria condition)
        {
            var predicate = PredicateBuilder.True<NsQtChungTu>();
            predicate = predicate.And(x => x.INamLamViec == condition.YearOfWork && x.INamNganSach == condition.YearOfBudget
                        && x.IIdMaNguonNganSach == condition.BudgetSource && x.IIdMaDonVi == condition.AgencyId
                        && x.IThangQuy == condition.QuarterMonth && x.IThangQuyLoai == condition.QuarterMonthType && x.SLoai == condition.SettlementType);

            var chungTus = _chungTuRepository.FirstOrDefault(predicate);

            return !string.IsNullOrEmpty(chungTus?.STongHop);
        }

        public void DeleteRange(List<NsQtChungTu> chungTus)
        {
            _chungTuRepository.DeleteRange(chungTus);
        }

        public int CreateVoucherIndex(SettlementVoucherCriteria condition)
        {
            return _chungTuRepository.FindLastIndex(condition) + 1;
        }

        public int CreateAdjustVoucherIndex(SettlementVoucherCriteria condition)
        {
            return _chungTuRepository.FindLastAdjustIndex(condition) + 1;
        }

        public void LockOrUnlockMultiple(List<NsQtChungTu> chungTus, bool isLock)
        {
            _chungTuRepository.LockOrUnlockMultiple(chungTus, isLock);
        }

        public IEnumerable<NsQtChungTu> FindByAggregateVoucher(List<string> voucherNoes, int yearOfWork, int yearOfBudget, int budgetSource, string voucherType)
        {
            return _chungTuRepository.FindByAggregateVoucher(voucherNoes, yearOfWork, yearOfBudget, budgetSource, voucherType);
        }

        public void UpdateAggregateStatus(string voucherIds)
        {
            _chungTuRepository.UpdateAggregateStatus(voucherIds);
        }

        public List<NsQtChungTu> GetDataExportJson(List<Guid> iIds)
        {
            List<NsQtChungTu> resuilt = new List<NsQtChungTu>();
            if (iIds == null || iIds.Count == 0) return new List<NsQtChungTu>();
            var datas = _chungTuRepository.FindAll().Where(n => iIds.Contains(n.Id));
            var detail = _detailRepository.FindByParentId(iIds);
            Dictionary<Guid, List<NsQtChungTuChiTiet>> _dicDetail = new Dictionary<Guid, List<NsQtChungTuChiTiet>>();

            if (datas != null) resuilt = datas.ToList();
            if (detail != null && detail.Any())
            {
                _dicDetail = detail.GroupBy(n => n.IIdQtchungTu.Value).ToDictionary(n => n.Key, n => n.ToList());
            }
            foreach (var item in resuilt)
            {
                if (_dicDetail.ContainsKey(item.Id))
                {
                    item.Details = new List<NsQtChungTuChiTiet>();
                    item.Details.AddRange(_dicDetail[item.Id]);
                }
            }
            return resuilt;
        }

        /// <summary>
        /// Cập nhật list chứng từ
        /// </summary>
        /// <param name="listChungTu"></param>
        public void UpdateRange(List<NsQtChungTu> listChungTu)
        {
            _chungTuRepository.UpdateRange(listChungTu);
        }
    }
}
