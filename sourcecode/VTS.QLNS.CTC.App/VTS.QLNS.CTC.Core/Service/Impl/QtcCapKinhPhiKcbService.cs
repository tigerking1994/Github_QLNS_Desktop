using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class QtcCapKinhPhiKcbService : IQtcCapKinhPhiKcbService
    {
        private readonly IQtcCapKinhPhiKcbRepository _repository;
        public QtcCapKinhPhiKcbService(IQtcCapKinhPhiKcbRepository iQtcCapKinhPhiKcbRepository)
        {
            _repository = iQtcCapKinhPhiKcbRepository;
        }

        public int Add(BhQtCapKinhPhiKcb entity)
        {
            return _repository.Add(entity);
        }

        public int Delete(BhQtCapKinhPhiKcb item)
        {
            return _repository.Delete(item);
        }

        public BhQtCapKinhPhiKcb FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<BhQtCapKinhPhiKcb> FindByYear(int namLamViec)
        {
            return _repository.FindByYear(namLamViec);
        }

        public int GetVoucherIndex(int yearOfWork)
        {
            return _repository.GetVoucherIndex(yearOfWork);
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            var chungTuKhtBHXH = _repository.Find(id);
            chungTuKhtBHXH.BKhoa = isLock;
            _repository.Update(chungTuKhtBHXH);
        }

        public int Update(BhQtCapKinhPhiKcb item)
        {
            return _repository.Update(item);
        }
    }
}
