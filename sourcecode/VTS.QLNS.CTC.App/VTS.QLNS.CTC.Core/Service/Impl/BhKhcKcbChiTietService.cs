using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhKhcKcbChiTietService : IBhKhcKcbChiTietService
    {
        private readonly IBhKhcKcbChiTietRepository _repostiory;
        public BhKhcKcbChiTietService(IBhKhcKcbChiTietRepository repostiory)
        {
            _repostiory = repostiory;
        }
        public void AddAggregate(KhcKcbChiTietCriteria creation)
        {
            _repostiory.AddAggregate(creation);
        }

        public int AddRange(IEnumerable<BhKhcKcbChiTiet> khcKinhphiQuanlyChiTiets)
        {
            return _repostiory.AddRange(khcKinhphiQuanlyChiTiets);
        }

        public void Delete(Guid id)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                BhKhcKcbChiTiet entity = _repostiory.Find(id);
                if (entity != null)
                {
                    // Xóa chi tiết
                    _repostiory.Delete(entity);
                }

                transactionScope.Complete();
            }
        }

        public bool ExistKhcKcbChiTiet(Guid bhxhId)
        {
            return _repostiory.ExistKhcKcbChiTiet(bhxhId);
        }

        public IEnumerable<BhKhcKcbChiTiet> FindAll(Expression<Func<BhKhcKcbChiTiet, bool>> predicate)
        {
            return _repostiory.FindAll(predicate);
        }

        public IEnumerable<BhKhcKcbChiTiet> FindByCondition(Expression<Func<BhKhcKcbChiTiet, bool>> predicate)
        {
            return _repostiory.FindAll(predicate);
        }

        public IEnumerable<BhKhcKcbChiTiet> FindByConditionForChildUnit(KhcKcbChiTietCriteria searchModel)
        {
            return _repostiory.FindByConditionForChildUnit(searchModel);
        }

        public BhKhcKcbChiTiet FindById(Guid id)
        {
            return _repostiory.Find(id);
        }

        public IEnumerable<BhKhcKcbChiTiet> FindByIdChiTiet(Guid id)
        {
            return _repostiory.FindByIdChiTiet(id);
        }

        public IEnumerable<ReportKhcKcbBHXHQuery> FindChungTuTongHopForDonVi(int iNamlamViec, string listTenDonVi, int iLoaiTongHop)
        {
            return _repostiory.FindChungTuTongHopForDonVi(iNamlamViec, listTenDonVi, iLoaiTongHop);
        }

        public List<BhKhcKcbChiTiet> GetDataDetailVoucher(KhcKcbChiTietCriteria searchCondition)
        {
            return _repostiory.GetDataDetailVoucher(searchCondition);
        }

        public int RemoveRange(IEnumerable<BhKhcKcbChiTiet> bhKhcKcbChiTiets)
        {
            return _repostiory.RemoveRange(bhKhcKcbChiTiets);
        }

        public void Update(BhKhcKcbChiTiet entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repostiory.Update(entity);

                transactionScope.Complete();
            }
        }
        public IEnumerable<BhKhcKcbChiTietQuery> FindGiaTriKeHoachThuBHXH(string sMaDonVi, int iNamLamViec, double fTyLeThu)
        {
            return _repostiory.FindGiaTriKeHoachThuBHXH(sMaDonVi, iNamLamViec,fTyLeThu);
        }
    }
}
