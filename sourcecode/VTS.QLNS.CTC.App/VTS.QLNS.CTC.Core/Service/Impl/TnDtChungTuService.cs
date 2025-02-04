using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TnDtChungTuService : ITnDtChungTuService
    {
        private readonly ITnDtChungTuRepository _tnDtChungTuRepository;

        public TnDtChungTuService(ITnDtChungTuRepository tnDtChungTuRepository)
        {
            _tnDtChungTuRepository = tnDtChungTuRepository;
        }

        public TnDtChungTu Add(TnDtChungTu entity)
        {
            _tnDtChungTuRepository.Add(entity);
            return entity;
        }

        public int Delete(Guid id)
        {
            TnDtChungTu entity = _tnDtChungTuRepository.Find(id);
            return _tnDtChungTuRepository.Delete(entity);
        }

        public TnDtChungTu FindAggregateVoucher(string voucherNoes)
        {
            return _tnDtChungTuRepository.FindAggregateVoucher(voucherNoes);
        }

        public IEnumerable<TnDtChungTu> FindByCondition(Expression<Func<TnDtChungTu, bool>> predicate)
        {
            return _tnDtChungTuRepository.FindAll(predicate);
        }

        public TnDtChungTu FindById(Guid id)
        {
            return _tnDtChungTuRepository.Find(id);
        }

        public IEnumerable<TnDtChungTu> FindByType(int iLoai)
        {
            return _tnDtChungTuRepository.FindByType(iLoai);
        }

        public int FindNextSoChungTuIndex(Expression<Func<TnDtChungTu, bool>> predicate)
        {
            return _tnDtChungTuRepository.FindNextSoChungTuIndex(predicate);
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            return _tnDtChungTuRepository.LockOrUnLock(id, lockStatus);
        }

        public int Update(TnDtChungTu item)
        {
            return _tnDtChungTuRepository.Update(item);
        }
        public IEnumerable<string> GetLnsHasData(List<Guid> chungTuIds)
        {
            return _tnDtChungTuRepository.GetLnsHasData(chungTuIds);
        }

        public bool CheckDeletePhanBo(Guid id)
        {
            return _tnDtChungTuRepository.CheckDeletePhanBo(id);
        }

        public TnDtChungTu FindByIdDotNhan(string sid)
        {
            return _tnDtChungTuRepository.FindByIdDotNhan(sid);
        }
        public List<string> GetAgencyCodeByVoucherDetail(Expression<Func<TnDtChungTu, bool>> predicate)
        {
            return _tnDtChungTuRepository.GetAgencyCodeByVoucherDetail(predicate);
        }
    }
}
