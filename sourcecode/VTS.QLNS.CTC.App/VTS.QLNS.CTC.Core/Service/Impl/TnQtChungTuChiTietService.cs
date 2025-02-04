using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TnQtChungTuChiTietService : ITnQtChungTuChiTietService
    {
        private readonly ITnQtChungTuChiTietRepository _tnQtChungTuChiTietRepository;

        public TnQtChungTuChiTietService(ITnQtChungTuChiTietRepository tnQtChungTuChiTietRepository)
        {
            _tnQtChungTuChiTietRepository = tnQtChungTuChiTietRepository;
        }

        public void AddAggregateVoucherDetail(SettlementVoucherDetailCriteria creation)
        {
            _tnQtChungTuChiTietRepository.AddAggregateVoucherDetail(creation);
        }

        public int AddRange(IEnumerable<TnQtChungTuChiTiet> entities)
        {
            return _tnQtChungTuChiTietRepository.AddRange(entities);
        }

        public void Delete(Guid id)
        {
            TnQtChungTuChiTiet entity = _tnQtChungTuChiTietRepository.Find(id);
            if (entity != null)
            {
                _tnQtChungTuChiTietRepository.Delete(entity);
            }
        }

        public TnQtChungTuChiTiet FindById(Guid id)
        {
            return _tnQtChungTuChiTietRepository.Find(id);
        }

        public IEnumerable<TnQtChungTuChiTietQuery> FindByRealRevenueExpenditureCondition(EstimationVoucherDetailCriteria searchCondition)
        {
            return _tnQtChungTuChiTietRepository.FindByRealRevenueExpenditureCondition(searchCondition);
        }

        public IEnumerable<TnQtChungTuChiTietReportQuery> FindByRealRevenueExpenditureReportCondition(EstimationVoucherDetailCriteria searchCondition)
        {
            return _tnQtChungTuChiTietRepository.FindByRealRevenueExpenditureReportCondition(searchCondition);
        }

        public IEnumerable<TnQtChungTuChiTietReportQuery> FindByRealRevenueExpenditureResultCondition(EstimationVoucherDetailCriteria searchCondition)
        {
            return _tnQtChungTuChiTietRepository.FindByRealRevenueExpenditureResultCondition(searchCondition);
        }

        public int Update(TnQtChungTuChiTiet entity)
        {
            return _tnQtChungTuChiTietRepository.Update(entity);
        }
    }
}
