using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhCpBsChungTuService : IBhCpBsChungTuService
    {
        private readonly IBhCpBsChungTuRepository _cpChungTuRepository;

        public BhCpBsChungTuService(IBhCpBsChungTuRepository chungTuRepository)
        {
            _cpChungTuRepository = chungTuRepository;
        }

        public int Add(BhCpBsChungTu entity)
        {
            return _cpChungTuRepository.Add(entity);
        }

        public void AddAggregate(BhCpBsChungTuChiTietCriteria creation)
        {
            _cpChungTuRepository.AddAggregate(creation);
        }

        public int Delete(BhCpBsChungTu item)
        {
            return _cpChungTuRepository.Delete(item);
        }

        public IEnumerable<BhCpBsChungTu> FindByAggregateVoucher(List<string> voucherNoes, int yearOfWork)
        {
            return _cpChungTuRepository.FindByAggregateVoucher(voucherNoes, yearOfWork);
        }

        public IEnumerable<BhCpBsChungTu> FindByCondition(int namLamViec, int loaiChungTu)
        {
            return _cpChungTuRepository.FindByCondition(namLamViec, loaiChungTu);
        }

        public BhCpBsChungTu FindById(Guid id)
        {
            return _cpChungTuRepository.Find(id);
        }

        public IEnumerable<BhCpBsChungTu> FindByYear(int namLamViec)
        {
            return _cpChungTuRepository.FindByYear(namLamViec);
        }

        public IEnumerable<BhCpBsChungTu> FindChungTuDaTongHopBySCT(string sct, int namLamViec)
        {
            return _cpChungTuRepository.FindChungTuDaTongHopBySCT(sct, namLamViec);
        }

        public int GetSoChungTuIndexByCondition(int yearOfWork)
        {
            return _cpChungTuRepository.GetSoChungTuIndexByCondition(yearOfWork);
        }

        public bool IsExistChungTuTongHop(int namLamViec)
        {
            return _cpChungTuRepository.IsExistChungTuTongHop(namLamViec);
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            var chungTuKhtBHXH = _cpChungTuRepository.Find(id);
            chungTuKhtBHXH.BKhoa = isLock;
            _cpChungTuRepository.Update(chungTuKhtBHXH);
        }

        public int Update(BhCpBsChungTu item)
        {
            return _cpChungTuRepository.Update(item);
        }
    }
}
