using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class DttBHXHService : IDttBHXHService
    {
        private readonly IDttBHXHRepository _iDttBHXHRepository;
        public DttBHXHService(IDttBHXHRepository iDttBHXHRepository)
        {
            _iDttBHXHRepository = iDttBHXHRepository;
        }

        public int Add(BhDttBHXH entity)
        {
            return _iDttBHXHRepository.Add(entity);
        }

        public int Delete(BhDttBHXH item)
        {
            return _iDttBHXHRepository.Delete(item);
        }

        public IEnumerable<BhDttBHXHQuery> FindByCondition(int namLamViec)
        {
            return _iDttBHXHRepository.FindByCondition(namLamViec);
        }

        public BhDttBHXH FindById(Guid id)
        {
            return _iDttBHXHRepository.Find(id);
        }

        public int GetSoChungTuIndexByCondition(int yearOfWork)
        {
            return _iDttBHXHRepository.GetSoChungTuIndexByCondition(yearOfWork);
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            var chungTuBHXH = _iDttBHXHRepository.Find(id);
            chungTuBHXH.BIsKhoa = isLock;
            _iDttBHXHRepository.Update(chungTuBHXH);
        }

        public int Update(BhDttBHXH item)
        {
            return _iDttBHXHRepository.Update(item);
        }
    }
}
