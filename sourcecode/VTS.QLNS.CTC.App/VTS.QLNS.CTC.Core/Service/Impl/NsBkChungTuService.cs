using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NsBkChungTuService : INsBkChungTuService
    {
        private INsBkChungTuRepository _chungTuRepository;

        public NsBkChungTuService(INsBkChungTuRepository chungTuRepository)
        {
            _chungTuRepository = chungTuRepository;
        }

        public void Add(NsBkChungTu chungTu)
        {
            _chungTuRepository.Add(chungTu);
        }

        public void Delete(Guid id)
        {
            NsBkChungTu chungTu = _chungTuRepository.Find(id);
            _chungTuRepository.Delete(chungTu);
        }

        public void DeleteRange(List<NsBkChungTu> chungTus)
        {
            _chungTuRepository.RemoveRange(chungTus);
        }

        public List<NsBkChungTu> FindByCondition(Expression<Func<NsBkChungTu, bool>> predicate)
        {
            return _chungTuRepository.FindAll(predicate).ToList();
        }

        public NsBkChungTu FindById(Guid id)
        {
            return _chungTuRepository.Find(id);
        }

        /// <summary>
        /// Khóa hoặc mở khóa chứng từ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isLock"></param>
        public void LockOrUnlock(Guid id, bool isLock)
        {
            NsBkChungTu chungTu = _chungTuRepository.Find(id);
            chungTu.BKhoa = isLock;
            _chungTuRepository.Update(chungTu);
        }

        public void LockOrUnlockMultiple(List<NsBkChungTu> chungTus, bool isLock)
        {
            _chungTuRepository.LockOrUnlockMultiple(chungTus, isLock);
        }

        public void Update(NsBkChungTu chungTu)
        {
            _chungTuRepository.Update(chungTu);
        }

        public int UpdateRange(IEnumerable<NsBkChungTu> chungTu)
        {
            return _chungTuRepository.UpdateRange(chungTu);
        }
    }
}
