using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TnQtChungTuService : ITnQtChungTuService
    {
        private readonly ITnQtChungTuRepository _tnQtChungTuRepository;

        public TnQtChungTuService(ITnQtChungTuRepository tnQtChungTuRepository)
        {
            _tnQtChungTuRepository = tnQtChungTuRepository;
        }

        public TnQtChungTu Add(TnQtChungTu entity)
        {
            _tnQtChungTuRepository.Add(entity);
            return entity;
        }

        public int Delete(Guid id)
        {
            TnQtChungTu entity = _tnQtChungTuRepository.Find(id);
            return _tnQtChungTuRepository.Delete(entity);
        }

        public TnQtChungTu FindAggregateVoucher(string voucherNoes)
        {
            return _tnQtChungTuRepository.FindAggregateVoucher(voucherNoes);
        }

        public IEnumerable<TnQtChungTu> FindAll()
        {
            return _tnQtChungTuRepository.FindAll();
        }

        public IEnumerable<TnQtChungTu> FindByCondition(Expression<Func<TnQtChungTu, bool>> predicate)
        {
            return _tnQtChungTuRepository.FindAll(predicate);
        }

        public TnQtChungTu FindById(Guid id)
        {
            return _tnQtChungTuRepository.Find(id);
        }

        public IEnumerable<TnQtChungTu> FindByIdDonVi(string idDonVi, int iThangQuyLoai)
        {
            return _tnQtChungTuRepository.FindByIdDonVi(idDonVi, iThangQuyLoai);
        }

        public int FindNextSoChungTuIndex(Expression<Func<TnQtChungTu, bool>> predicate)
        {
            return _tnQtChungTuRepository.FindNextSoChungTuIndex(predicate);
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            return _tnQtChungTuRepository.LockOrUnLock(id, lockStatus);
        }

        public int Update(TnQtChungTu item)
        {
            return _tnQtChungTuRepository.Update(item);
        }
    }
}
