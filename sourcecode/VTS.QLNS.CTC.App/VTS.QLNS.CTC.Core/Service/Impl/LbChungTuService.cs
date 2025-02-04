using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class LbChungTuService : ILbChungTuService
    {
        private readonly ILbChungTuRepository _chungTuRepository;

        public LbChungTuService(ILbChungTuRepository chungTuRepository)
        {
            _chungTuRepository = chungTuRepository;
        }

        public NsNganhChungTu Add(NsNganhChungTu entity)
        {
            _chungTuRepository.Add(entity);
            return entity;
        }

        public IEnumerable<NsNganhChungTu> FindByCondition(Expression<Func<NsNganhChungTu, bool>> predicate)
        {
            return _chungTuRepository.FindAll(predicate);
        }

        public int Delete(Guid id)
        {
            NsNganhChungTu entity = _chungTuRepository.Find(id);
            return _chungTuRepository.Delete(entity);
        }

        public int UpdateRange(IEnumerable<NsNganhChungTu> entities)
        {
            return _chungTuRepository.UpdateRange(entities);
        }

        public NsNganhChungTu FindById(Guid id)
        {
            return _chungTuRepository.Find(id);
        }

        public int Update(NsNganhChungTu item)
        {
            return _chungTuRepository.Update(item);
        }

        public int UpdateStatusDisable(Guid id)
        {
            NsNganhChungTu item = _chungTuRepository.Find(id);
            if (item != null)
            {
                return _chungTuRepository.Update(item);
            }
            return 0;
        }

        public int GetSoChungTuIndexByCondition(int namLamViec, int nguonNganSach)
        {
            return _chungTuRepository.GetSoChungTuIndexByCondition(namLamViec, nguonNganSach);
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            return _chungTuRepository.LockOrUnLock(id, lockStatus);
        }

        public void UpdateTotalLbChungTu(string voucherId, string userModify)
        {
            _chungTuRepository.UpdateTotalLbChungTu(voucherId, userModify);
        }

        IEnumerable<LbChungTuQuery> ILbChungTuService.FindByCondition(int namLamViec, int nguonNganSach, string donviUserId, int namNganSach, string userName)
        {
            return _chungTuRepository.FindByCondition(namLamViec, nguonNganSach, donviUserId, namNganSach, userName);
        }

        public IEnumerable<LbChungTuCanCuQuery> FindByCondition(int namLamViec, Guid idChungTu, string idDonVi)
        {
            return _chungTuRepository.FindByCondition(namLamViec, idChungTu, idDonVi);
        }

        public IEnumerable<LbChungTuCanCuDuToanDataQuery> GetCanCuDuToanData(int namLamViec, string idChungTu, int loaiChungTu, string idDonVi)
        {
            return _chungTuRepository.GetCanCuDuToanData(namLamViec, idChungTu, loaiChungTu, idDonVi);
        }
    }
}
