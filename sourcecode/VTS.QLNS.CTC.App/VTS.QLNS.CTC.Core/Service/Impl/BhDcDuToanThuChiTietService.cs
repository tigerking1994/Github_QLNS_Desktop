using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using static Dapper.SqlMapper;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhDcDuToanThuChiTietService : IBhDcDuToanThuChiTietService
    {
        private readonly IBhDcDuToanThuChiTietRepository _repository;

        public BhDcDuToanThuChiTietService(IBhDcDuToanThuChiTietRepository repostiory)
        {
            _repository = repostiory;
        }

        public void AddAggregate(BhDcDuToanThuChiTietCriteria creation)
        {
            _repository.AddAggregate(creation);
        }

        public int AddRange(IEnumerable<BhDcDuToanThuChiTiet> item)
        {
            return _repository.AddRange(item);
        }

        public void Delete(Guid id)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                BhDcDuToanThuChiTiet entity = _repository.Find(id);
                if (entity != null)
                {
                    // Xóa chi tiết
                    _repository.Delete(entity);
                }

                transactionScope.Complete();
            }
        }

        public bool ExistKhcKcbChiTiet(Guid bhxhId)
        {
            return _repository.ExistKhcKcbChiTiet(bhxhId);
        }

        public IEnumerable<RptDcDuToanThuChiTietQuery> ExportDieuChinhDtTheoDoiTuong(string maDonVi, int dvt, int namLamViec, bool isAggregate)
        {
            return _repository.ExportDieuChinhDtTheoDoiTuong(maDonVi, dvt, namLamViec, isAggregate);
        }

        public IEnumerable<BhDcDuToanThuChiTiet> FindAllChungTuDuToan()
        {
            return _repository.FindAll();
        }

        public IEnumerable<RptDcDuToanThuChiTietQuery> FindBhDcDttChiTietByAgencySummaryDetail(string maDonVi, int dvt, int namLamViec)
        {
            return _repository.FindBhDcDttChiTietByAgencySummaryDetail(maDonVi, dvt, namLamViec);
        }

        public IEnumerable<RptDcDuToanThuChiTietQuery> FindBhDcDttChiTietByCondition(BhDcDuToanThuChiTietCriteria searchModel)
        {
            return _repository.FindBhDcDttChiTietByCondition(searchModel);
        }

        public IEnumerable<RptDcDuToanThuChiTietQuery> FindBhDcDttChiTietByUnit(string maDonVi, int dvt, int namLamViec, bool isAggregate)
        {
            return _repository.FindBhDcDttChiTietByUnit(maDonVi, dvt, namLamViec, isAggregate);
        }

        public IEnumerable<BhDcDuToanThuChiTiet> FindByCondition(Expression<Func<BhDcDuToanThuChiTiet, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public IEnumerable<BhDcDuToanThuChiTiet> FindByConditionForChildUnit(BhDcDuToanThuChiTietCriteria searchModel)
        {
            return _repository.FindByConditionForChildUnit(searchModel);
        }

        public BhDcDuToanThuChiTiet FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<BhDcDuToanThuChiTiet> FindByIdChiTiet(Guid id)
        {
            return _repository.FindByIdChiTiet(id);
        }

        public IEnumerable<BhDcDuToanThuChiTietQuery> GetAggregateAdjustData(int iNam, string sMaDonVi)
        {
            return _repository.GetAggregateAdjustData(iNam, sMaDonVi);
        }

        public IEnumerable<BhDcDuToanThuChiTietQuery> GetSettlementData(int namLamViec, string maDonVi, int thangQuy, int loaiThangQuy)
        {
            return _repository.GetSettlementData(namLamViec, maDonVi, thangQuy, loaiThangQuy);
        }

        public IEnumerable<BhDcDuToanThuChiTietQuery> GetUnitAggregateAdjustData(int iNam, string sMaDonVi)
        {
            return _repository.GetUnitAggregateAdjustData(iNam, sMaDonVi);
        }

        public int RemoveRange(IEnumerable<BhDcDuToanThuChiTiet> chungTuChiTiets)
        {
            return _repository.RemoveRange(chungTuChiTiets);
        }

        public void Update(BhDcDuToanThuChiTiet entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Update(entity);
                transactionScope.Complete();
            }
        }
    }
}
