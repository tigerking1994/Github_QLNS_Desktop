using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TnDtdnChungTuChiTietService : ITnDtdnChungTuChiTietService
    {

        private readonly ITnDtdnChungTuChiTietRepository _tnDtdnChungTuChiTietRepository;

        public TnDtdnChungTuChiTietService(ITnDtdnChungTuChiTietRepository tnDtdnChungTuChiTietRepository)
        {
            _tnDtdnChungTuChiTietRepository = tnDtdnChungTuChiTietRepository;
        }

        public int AddRange(IEnumerable<TnDtdnChungTuChiTiet> entities)
        {
            return _tnDtdnChungTuChiTietRepository.AddRange(entities);
        }

        public void Delete(Guid id)
        {
            TnDtdnChungTuChiTiet entity = _tnDtdnChungTuChiTietRepository.Find(id);
            if (entity != null)
            {
                _tnDtdnChungTuChiTietRepository.Delete(entity);
            }
        }

        public IEnumerable<TnDtdnChungTuChiTiet> FindAll()
        {
            return _tnDtdnChungTuChiTietRepository.FindAll();
        }

        public IEnumerable<TnDtdnChungTuChiTietQuery> FindByDataDetailCondition(EstimationVoucherDetailCriteria searchCondition)
        {
            return _tnDtdnChungTuChiTietRepository.FindByDataDetailCondition(searchCondition);
        }

        //public IEnumerable<TnDtChungTuChiTiet> FindByApprovedAndPlanEstimationCondition(EstimationVoucherDetailCriteria searchCondition, int type)
        //{
        //    return _tnDtdnChungTuChiTietRepository.FindByApprovedAndPlanEstimationCondition(searchCondition, type);
        //}

        public TnDtdnChungTuChiTiet FindById(Guid id)
        {
            return _tnDtdnChungTuChiTietRepository.Find(id);
        }

        //public IEnumerable<TnDtChungTuChiTietReportQuery> FindByPlanBudgetReportCondition(EstimationVoucherDetailCriteria searchCondition, int type)
        //{
        //    return _tnDtdnChungTuChiTietRepository.FindByPlanBudgetReportCondition(searchCondition, type);
        //}

        //public IEnumerable<TnDtChungTuChiTietQuery> FindByRevenueExpendDivisionCondition(EstimationVoucherDetailCriteria searchCondition)
        //{
        //    return _tnDtdnChungTuChiTietRepository.FindByRevenueExpendDivisionCondition(searchCondition);
        // }

        public int Update(TnDtdnChungTuChiTiet entity)
        {
            return _tnDtdnChungTuChiTietRepository.Update(entity);
        }
        public IEnumerable<TnDtdnChungTuChiTietQuery> FindDataReportAgencyDetailByCondition(EstimationVoucherDetailCriteria searchCondition)
        {
            return _tnDtdnChungTuChiTietRepository.FindDataReportAgencyDetailByCondition(searchCondition);
        }

        public IEnumerable<TnDtdnChungTuChiTiet> FindByChungTuId(Guid id)
        {
            return _tnDtdnChungTuChiTietRepository.FindByChungTuId(id);
        }

        public int RemoveRange(IEnumerable<TnDtdnChungTuChiTiet> entities)
        {
            return _tnDtdnChungTuChiTietRepository.RemoveRange(entities);
        }
    }
}
