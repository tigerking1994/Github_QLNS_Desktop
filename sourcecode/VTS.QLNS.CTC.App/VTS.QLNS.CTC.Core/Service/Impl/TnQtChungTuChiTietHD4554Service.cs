using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using static Dapper.SqlMapper;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TnQtChungTuChiTietHD4554Service : ITnQtChungTuChiTietHD4554Service
    {
        private readonly ITnQtChungTuChiTietHD4554Repository _repository;
        public TnQtChungTuChiTietHD4554Service(ITnQtChungTuChiTietHD4554Repository repository)
        {
            _repository = repository;
        }

        public void AddAggregateVoucherDetail(SettlementVoucherDetailCriteria creation)
        {
            _repository.AddAggregateVoucherDetail(creation);
        }

        public int AddRange(IEnumerable<TnQtChungTuChiTietHD4554> entities)
        {
            return _repository.AddRange(entities);
        }

        public void Delete(Guid id)
        {
            TnQtChungTuChiTietHD4554 entity = _repository.Find(id);
            if (entity != null)
            {
                _repository.Delete(entity);
            }
        }

        public List<TnQtChungTuChiTietHD4554Query> FindAllNSDCChungTuByCondition(EstimationVoucherDetailCriteria searchCondition)
        {
            return _repository.FindAllNSDCChungTuByCondition(searchCondition);
        }

        public IEnumerable<TnQtChungTuChiTietHD4554> FindByCondition(Expression<Func<TnQtChungTuChiTietHD4554, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public TnQtChungTuChiTietHD4554 FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<TnQtChungTuChiTietHD4554> FindByIdChiTiet(Guid IdChungTu)
        {
            return _repository.FindByIdChiTiet(IdChungTu);
        }

        public IEnumerable<TnQtChungTuChiTietHD4554Query> FindByRealRevenueExpenditureCondition(EstimationVoucherDetailCriteria searchCondition)
        {
            return _repository.FindByRealRevenueExpenditureCondition(searchCondition);
        }

        public IEnumerable<TnQtChungTuChiTietReportQuery> FindByRealRevenueExpenditureReportCondition(EstimationVoucherDetailCriteria searchCondition)
        {
            return _repository.FindByRealRevenueExpenditureReportCondition(searchCondition);
        }

        public IEnumerable<TnQtChungTuChiTietReportQuery> FindByRealRevenueExpenditureResultCondition(EstimationVoucherDetailCriteria searchCondition)
        {
            return _repository.FindByRealRevenueExpenditureResultCondition(searchCondition);
        }

        public IEnumerable<TnQtChungTuChiTietHD4554Query> GetDataChungTuDetail(EstimationVoucherDetailCriteria searchCondition)
        {
            return _repository.GetDataChungTuDetail(searchCondition);
        }

        public IEnumerable<string> GetLnsHasData(List<Guid> chungTuIds)
        {
            return _repository.GetLnsHasData(chungTuIds);
        }

        public void RemoveRange(List<TnQtChungTuChiTietHD4554> listChungTuChiTiet)
        {
            _repository.RemoveRange(listChungTuChiTiet);
        }

        public int Update(TnQtChungTuChiTietHD4554 entity)
        {
            return _repository.Update(entity);
        }

        public void UpdateMonth(Guid voucherId, int month, int monthType, string userName)
        {
            _repository.UpdateMonth(voucherId, month, monthType, userName);
        }
    }
}
