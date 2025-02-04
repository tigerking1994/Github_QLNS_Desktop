using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlQsChungTuNq104Service : ITlQsChungTuNq104Service
    {
        private ITlQsChungTuNq104Repository _tlQsChungTuRepository;
        public TlQsChungTuNq104Service(ITlQsChungTuNq104Repository tlQsChungTuRepository)
        {
            _tlQsChungTuRepository = tlQsChungTuRepository;
        }
        public int Add(TlQsChungTuNq104 entity)
        {
            return _tlQsChungTuRepository.Add(entity);
        }

        public int Delete(Guid id)
        {
            return _tlQsChungTuRepository.Delete(id);
        }

        public TlQsChungTuNq104 Find(Guid id)
        {
            return _tlQsChungTuRepository.Find(id);
        }

        public IEnumerable<TlQsChungTuNq104> FindAll()
        {
            return _tlQsChungTuRepository.FindAll();
        }

        public IEnumerable<TlQsChungTuNq104> FindAll(Expression<Func<TlQsChungTuNq104, bool>> predicate)
        {
            return _tlQsChungTuRepository.FindAll(predicate);
        }

        public int Update(TlQsChungTuNq104 entity)
        {
            return _tlQsChungTuRepository.Update(entity);
        }

        public int UpDateRange(List<TlQsChungTuNq104> entities)
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
