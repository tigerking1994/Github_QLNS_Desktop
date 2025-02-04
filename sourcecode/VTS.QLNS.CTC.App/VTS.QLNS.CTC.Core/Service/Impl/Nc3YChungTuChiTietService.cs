using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class Nc3YChungTuChiTietService : INc3YChungTuChiTietService
    {
        private readonly INc3YChungTuChiTietRepository _nc3YChungTuChiTietRepository;

        public Nc3YChungTuChiTietService(INc3YChungTuChiTietRepository nc3YChungTuChiTietRepository)
        {
            _nc3YChungTuChiTietRepository = nc3YChungTuChiTietRepository;
        }
        public int AddRange(IEnumerable<NsNc3YChungTuChiTiet> nc3YChungTuChiTiets)
        {
            return _nc3YChungTuChiTietRepository.AddRange(nc3YChungTuChiTiets);
        }
        public int RemoveRange(IEnumerable<NsNc3YChungTuChiTiet> nc3YChungTuChiTiets)
        {
            return _nc3YChungTuChiTietRepository.RemoveRange(nc3YChungTuChiTiets);
        }
        public int UpdateRange(IEnumerable<NsNc3YChungTuChiTiet> nc3YChungTuChiTiets)
        {
            return _nc3YChungTuChiTietRepository.UpdateRange(nc3YChungTuChiTiets);
        } 
        public int Add(NsNc3YChungTuChiTiet entity)
        {
            return _nc3YChungTuChiTietRepository.Add(entity);
        }
        public int Delete(NsNc3YChungTuChiTiet entity)
        {
            return _nc3YChungTuChiTietRepository.Delete(entity);
        }
        public int Update(NsNc3YChungTuChiTiet entity)
        {
            return _nc3YChungTuChiTietRepository.Update(entity);
        }
        public NsNc3YChungTuChiTiet FindById(Guid id)
        {
            return _nc3YChungTuChiTietRepository.FindById(id);
        }
        public IEnumerable<NsNc3YChungTuChiTiet> FindByConditionForChildUnit(Nc3YChungTuChiTietCriteria searchModel)
        {
            return _nc3YChungTuChiTietRepository.FindByConditionForChildUnit(searchModel);
        }

        public bool ExistChungTuChiTiet(Guid chungtuId)
        {
            return _nc3YChungTuChiTietRepository.ExistChungTuChiTiet(chungtuId);
        }

        public IEnumerable<NsNc3YChungTuChiTiet> FindByCondition(Expression<Func<NsNc3YChungTuChiTiet, bool>> predicate)
        {
            return _nc3YChungTuChiTietRepository.FindByCondition(predicate);
        }

        public void AddAggregate(DemandVoucherDetailCriteria creation)
        {
            _nc3YChungTuChiTietRepository.AddAggregate(creation);
        }
    }
}