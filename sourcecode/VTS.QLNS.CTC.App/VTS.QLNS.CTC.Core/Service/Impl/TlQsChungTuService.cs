using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlQsChungTuService : ITlQsChungTuService
    {
        private ITlQsChungTuRepository _tlQsChungTuRepository;
        public TlQsChungTuService(ITlQsChungTuRepository tlQsChungTuRepository)
        {
            _tlQsChungTuRepository = tlQsChungTuRepository;
        }
        public int Add(TlQsChungTu entity)
        {
            return _tlQsChungTuRepository.Add(entity);
        }

        public int Delete(Guid id)
        {
            return _tlQsChungTuRepository.Delete(id);
        }

        public TlQsChungTu Find(Guid id)
        {
            return _tlQsChungTuRepository.Find(id);
        }

        public IEnumerable<TlQsChungTu> FindAll()
        {
            return _tlQsChungTuRepository.FindAll();
        }

        public IEnumerable<TlQsChungTu> FindAll(Expression<Func<TlQsChungTu, bool>> predicate)
        {
            return _tlQsChungTuRepository.FindAll(predicate);
        }

        public int Update(TlQsChungTu entity)
        {
            return _tlQsChungTuRepository.Update(entity);
        }

        public int UpDateRange(List<TlQsChungTu> entities)
        {
            return _tlQsChungTuRepository.UpdateRange(entities);
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            var chungTu = _tlQsChungTuRepository.Find(id);
            chungTu.IsLock = isLock;
            _tlQsChungTuRepository.Update(chungTu);
        }

        public int GetSoChungTuIndexByCondition(int namLamViec)
        {
            return _tlQsChungTuRepository.GetSoChungTuIndexByCondition(namLamViec);
        }
    }
}
