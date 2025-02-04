using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhKhcKChiTietService : IBhKhcKChiTietService
    {
        private readonly IBhKhcKChiTietRepository _repostiory;
        public BhKhcKChiTietService(IBhKhcKChiTietRepository repostiory)
        {
            _repostiory = repostiory;
        }

        public void AddAggregate(KhcKChiTietCriteria creation)
        {
            _repostiory.AddAggregate(creation);
        }

        public int AddRange(IEnumerable<BhKhcKChiTiet> khcKinhphiQuanlyChiTiets)
        {
            return _repostiory.AddRange(khcKinhphiQuanlyChiTiets);
        }

        public void Delete(Guid id)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                BhKhcKChiTiet entity = _repostiory.Find(id);
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

        public IEnumerable<BhKhcKChiTiet> FindAll(Expression<Func<BhKhcKChiTiet, bool>> predicate)
        {
            return _repostiory.FindAll(predicate);
        }

        public IEnumerable<BhKhcKChiTiet> FindByCondition(Expression<Func<BhKhcKChiTiet, bool>> predicate)
        {
            return _repostiory.FindAll(predicate);
        }

        public IEnumerable<BhKhcKChiTiet> FindByConditionForChildUnit(KhcKChiTietCriteria searchModel)
        {
            return _repostiory.FindByConditionForChildUnit(searchModel);
        }

        public BhKhcKChiTiet FindById(Guid id)
        {
            return _repostiory.Find(id);
        }

        public IEnumerable<BhKhcKChiTiet> FindByIdChiTiet(Guid id)
        {
            return _repostiory.FindByIdChiTiet(id);
        }

        public IEnumerable<ReportKhcKQuery> FindChungTuTongHopForDonVi(string listTenDonVi, int iNamlamViec, Guid IDLoaichi, int donViTinh, string lstLNS)
        {
            return _repostiory.FindChungTuTongHopForDonVi(listTenDonVi, iNamlamViec, IDLoaichi, donViTinh, lstLNS);
        }
        public IEnumerable<ReportKhcKQuery> FindChungTuHSSVNLDForDonVi(string listTenDonVi, int iNamlamViec, int iLoaiTongHop, string lstSLNS)
        {
            return _repostiory.FindChungTuHSSVNLDForDonVi(listTenDonVi, iNamlamViec, iLoaiTongHop, lstSLNS);
        }

        public IEnumerable<BhKhcKChiTiet> GetReportKeHoach(KhcKChiTietCriteria searchModel)
        {
            return _repostiory.GetReportKeHoach(searchModel);
        }

        public int RemoveRange(IEnumerable<BhKhcKChiTiet> bhKhcKcbChiTiets)
        {
            return _repostiory.RemoveRange(bhKhcKcbChiTiets);
        }

        public void Update(BhKhcKChiTiet entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repostiory.Update(entity);

                transactionScope.Complete();
            }
        }
    }
}
