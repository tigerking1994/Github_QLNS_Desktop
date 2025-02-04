using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhKhcKinhphiQuanlyChiTietService : IBhKhcKinhphiQuanlyChiTietService
    {
        private readonly IBhKhcKinhphiQuanlyChiTietRepostiory _repostiory;
        public BhKhcKinhphiQuanlyChiTietService(IBhKhcKinhphiQuanlyChiTietRepostiory repostiory)
        {
            _repostiory = repostiory;
        }

        public int AddRange(IEnumerable<BhKhcKinhphiQuanlyChiTiet> khcKinhphiQuanlyChiTiets)
        {
            return _repostiory.AddRange(khcKinhphiQuanlyChiTiets);
        }

        public void Delete(Guid id)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                BhKhcKinhphiQuanlyChiTiet entity = _repostiory.Find(id);
                if (entity != null)
                {
                    // Xóa chi tiết
                    _repostiory.Delete(entity);
                }

                transactionScope.Complete();
            }
        }

        public bool ExistKhcKinhphiQuanlyChiTiet(Guid bhxhId)
        {
            return _repostiory.ExistKhcKinhphiQuanlyChiTiet(bhxhId);
        }

        public int RemoveRange(IEnumerable<BhKhcKinhphiQuanlyChiTiet> khcKinhphiQuanlyChiTiets)
        {
            return _repostiory.RemoveRange(khcKinhphiQuanlyChiTiets);
        }

        public IEnumerable<BhKhcKinhphiQuanlyChiTiet> FindByCondition(Expression<Func<BhKhcKinhphiQuanlyChiTiet, bool>> predicate)
        {
            return _repostiory.FindAll(predicate);
        }

        public IEnumerable<BhKhcKinhphiQuanlyChiTiet> FindByConditionForChildUnit(KhcQuanlyKinhphiChiTietCriteria searchModel)
        {
            return _repostiory.FindByConditionForChildUnit(searchModel);
        }

        public BhKhcKinhphiQuanlyChiTiet FindById(Guid id)
        {
            return _repostiory.Find(id);
        }

        public IEnumerable<BhKhcKinhphiQuanlyChiTiet> FindByIdChiTiet(Guid id)
        {
            return _repostiory.FindByIdChiTiet(id);
        }

        public void Update(BhKhcKinhphiQuanlyChiTiet entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repostiory.Update(entity);

                transactionScope.Complete();
            }
        }

        public void AddAggregate(KhcQuanlyKinhphiChiTietCriteria creation)
        {
            _repostiory.AddAggregate(creation);
        }

        public IEnumerable<BhKhcKinhphiQuanlyChiTiet> FindAll(Expression<Func<BhKhcKinhphiQuanlyChiTiet, bool>> predicate)
        {
            return _repostiory.FindAll(predicate);
        }

        public IEnumerable<ReportKhcQuanLyKinhPhiTongHopBHXHQuery> FindChungTuTongHopForDonVi(int iNamlamViec, string listTenDonVi, int iLoaiTongHop)
        {
            return _repostiory.FindChungTuTongHopForDonVi(iNamlamViec, listTenDonVi, iLoaiTongHop);
        }

        public List<BhKhcKinhphiQuanlyChiTiet> GetDataDetailVoucher(KhcQuanlyKinhphiChiTietCriteria searchCondition)
        {
            return _repostiory.GetDataDetailVoucher(searchCondition);
        }
    }
}
