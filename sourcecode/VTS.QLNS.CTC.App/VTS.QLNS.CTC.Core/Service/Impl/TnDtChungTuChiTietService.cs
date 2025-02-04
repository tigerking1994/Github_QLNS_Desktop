using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TnDtChungTuChiTietService : ITnDtChungTuChiTietService
    {
        private readonly ITnDtChungTuChiTietRepository _tnDtChungTuChiTietRepository;

        public TnDtChungTuChiTietService(ITnDtChungTuChiTietRepository tnDtChungTuChiTietRepository)
        {
            _tnDtChungTuChiTietRepository = tnDtChungTuChiTietRepository;
        }

        public void AddAggregateVoucherDetail(SettlementVoucherDetailCriteria creation)
        {
            _tnDtChungTuChiTietRepository.AddAggregateVoucherDetail(creation);
        }

        public int AddRange(IEnumerable<TnDtChungTuChiTiet> entities)
        {
            return _tnDtChungTuChiTietRepository.AddRange(entities);
        }

        public void Delete(Guid id)
        {
            TnDtChungTuChiTiet entity = _tnDtChungTuChiTietRepository.Find(id);
            if (entity != null)
            {
                _tnDtChungTuChiTietRepository.Delete(entity);
            }
        }

        public IEnumerable<TnDtChungTuChiTiet> FindAll()
        {
            return _tnDtChungTuChiTietRepository.FindAll();
        }

        public IEnumerable<TnDtChungTuChiTietQuery> FindByApprovedAndPlanEstimationCondition(EstimationVoucherDetailCriteria searchCondition, int type)
        {
            return _tnDtChungTuChiTietRepository.FindByApprovedAndPlanEstimationCondition(searchCondition, type);
        }

        public IEnumerable<TnDtChungTuChiTiet> FindByChungtuId(Guid chungTuId)
        {
            return _tnDtChungTuChiTietRepository.FindByChungtuId(chungTuId);
        }
        public IEnumerable<TnDtChungTuChiTiet> FindByChungtuNhanId(Guid chungTuNhanId)
        {
            return _tnDtChungTuChiTietRepository.FindByChungtuNhanId(chungTuNhanId);
        }

        public TnDtChungTuChiTiet FindById(Guid id)
        {
            return _tnDtChungTuChiTietRepository.Find(id);
        }

        public IEnumerable<TnDtChungTuChiTietReportQuery> FindByPlanBudgetReportCondition(EstimationVoucherDetailCriteria searchCondition, int type)
        {
            return _tnDtChungTuChiTietRepository.FindByPlanBudgetReportCondition(searchCondition, type);
        }

        public IEnumerable<TnDtChungTuChiTietQuery> FindByRevenueExpendDivisionCondition(EstimationVoucherDetailCriteria searchCondition)
        {
            return _tnDtChungTuChiTietRepository.FindByRevenueExpendDivisionCondition(searchCondition);
        }

        public IEnumerable<TnDtChungTuChiTietQuery> FindByRevenueExpendDivisionReport(EstimationVoucherDetailCriteria searchCondition)
        {
            return _tnDtChungTuChiTietRepository.FindByRevenueExpendDivisionReport(searchCondition);
        }

        public int RemoveRange(IEnumerable<TnDtChungTuChiTiet> entities)
        {
            return _tnDtChungTuChiTietRepository.RemoveRange(entities);
        }

        public int Update(TnDtChungTuChiTiet entity)
        {
            return _tnDtChungTuChiTietRepository.Update(entity);
        }



    }
}
