using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhKhcCheDoBhXhChiTietService : IBhKhcCheDoBhXhChiTietService
    {
        private readonly IBhKhcCheDoBhXhChiTietRepository _repository;
        public BhKhcCheDoBhXhChiTietService(IBhKhcCheDoBhXhChiTietRepository repository)
        {
            _repository = repository;
        }

        public void Add(BhKhcCheDoBhXhChiTiet entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Add(entity);

                transactionScope.Complete();
            }
        }

        public void Delete(Guid id)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                BhKhcCheDoBhXhChiTiet entity = _repository.Find(id);
                if (entity != null)
                {
                    _repository.Delete(entity);
                    // Xóa chi tiết
                }

                transactionScope.Complete();
            }
        }

        public BhKhcCheDoBhXhChiTiet FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public void LockOrUnlock(Guid id, bool status)
        {
            BhKhcCheDoBhXhChiTiet entity = _repository.Find(id);
        }

        public void Update(BhKhcCheDoBhXhChiTiet entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Update(entity);

                transactionScope.Complete();
            }
        }

        public IEnumerable<BhKhcCheDoBhXhChiTiet> FindByIdChiTiet(Guid id)
        {
            return _repository.FindByIdChiTiet(id);
        }

        public IEnumerable<BhKhcCheDoBhXhChiTiet> FindByConditionForChildUnit(KhcCheDoBhXhChiTietCriteria searchModel)
        {
            return _repository.FindByConditionForChildUnit(searchModel);
        }

        public IEnumerable<BhKhcCheDoBhXhChiTiet> FindByConditionForDonVi(KhcCheDoBhXhChiTietCriteria searchModel)
        {
            return _repository.FindByConditionForDonVi(searchModel);
        }


        public bool ExistBHXHChiTiet(Guid bhxhId)
        {
            return _repository.ExistBHXHChiTiet(bhxhId);
        }

        public int AddRange(IEnumerable<BhKhcCheDoBhXhChiTiet> khcBhxhChiTiets)
        {
            return _repository.AddRange(khcBhxhChiTiets);
        }

        public int RemoveRange(IEnumerable<BhKhcCheDoBhXhChiTiet> khcBhxhChiTiets)
        {
            return _repository.RemoveRange(khcBhxhChiTiets);
        }

        public IEnumerable<BhKhcCheDoBhXhChiTiet> FindByCondition(Expression<Func<BhKhcCheDoBhXhChiTiet, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public IEnumerable<BhKhcCheDoBhXhChiTiet> FindAll(Expression<Func<BhKhcCheDoBhXhChiTiet, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public void AddAggregate(KhcCheDoBhXhChiTietCriteria creation)
        {
            _repository.AddAggregate(creation);
        }

        public IEnumerable<ReportKhcTongHopBHXHQuery> FindChungTuTongHopForDonVi(int iNamlamViec, string listTenDonVi, int iLoaiTongHop)
        {
            return _repository.FindChungTuTongHopForDonVi(iNamlamViec, listTenDonVi, iLoaiTongHop);
        }

        public List<BhKhcCheDoBhXhChiTiet> GetDataDetailVoucher(KhcCheDoBhXhChiTietCriteria searchCondition)
        {
            return _repository.GetDataDetailVoucher(searchCondition);
        }

        public List<BhKhcCheDoBhXhChiTiet> GetDataSummaryDetailVoucher(KhcCheDoBhXhChiTietCriteria searchCondition)
        {
            return _repository.GetDataSummaryDetailVoucher(searchCondition);
        }
        public IEnumerable<BhKhcCheDoBhXhChiTietQuery> GetPlanData(int iNam, string sSoChungTu, string sLNS)
        {
            return _repository.GetPlanData(iNam, sSoChungTu, sLNS);
        }
    }
}
