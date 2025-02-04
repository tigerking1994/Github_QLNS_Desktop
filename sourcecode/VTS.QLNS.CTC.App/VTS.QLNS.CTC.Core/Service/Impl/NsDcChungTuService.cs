using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NsDcChungTuService : INsDcChungTuService
    {
        private readonly INsDcChungTuRepository _chungTuRepository;
        public NsDcChungTuService(INsDcChungTuRepository chungTuRepository)
        {
            _chungTuRepository = chungTuRepository;
        }

        public NsDcChungTu Add(NsDcChungTu entity)
        {
            _chungTuRepository.Add(entity);
            return entity;
        }

        public int Delete(Guid id)
        {
            NsDcChungTu entity = _chungTuRepository.Find(id);
            return _chungTuRepository.Delete(entity);
        }

        public IEnumerable<NsDcChungTuQuery> FindByCondition(EstimationVoucherCriteria condition)
        {
            return _chungTuRepository.FindByCondition(condition);
        }

        public IEnumerable<NsDcChungTu> FindByCondition(Expression<Func<NsDcChungTu, bool>> predicate)
        {
            return _chungTuRepository.FindAll(predicate);
        }

        public NsDcChungTu FindById(Guid id)
        {
            return _chungTuRepository.Find(id);
        }

        public int FindNextSoChungTuIndex(Expression<Func<NsDcChungTu, bool>> predicate)
        {
            return _chungTuRepository.FindNextSoChungTuIndex(predicate);
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            return _chungTuRepository.LockOrUnLock(id, lockStatus);
        }

        public int Update(NsDcChungTu item)
        {
            return _chungTuRepository.Update(item);
        }

        public void UpdateAggregateStatus(string voucherIds)
        {
            _chungTuRepository.UpdateAggregateStatus(voucherIds);
        }
        public void BulkInsert(List<NsDcChungTu> lstData)
        {
            _chungTuRepository.BulkInsert(lstData);
        }

        public IEnumerable<NsDcChungTuQuery> FindByCondition(int namLamViec, int loaiChungTu, Guid idNhan, int namNganSach, int loaiNganSach)
        {
            return _chungTuRepository.FindByCondition(namLamViec, loaiChungTu, idNhan, namNganSach, loaiNganSach);
        }

        public List<string> GetDonViDieuChinh(string iDs, int namLamViec)
        {
            return _chungTuRepository.GetDonViDieuChinh(iDs, namLamViec);
        }

        public void UpdateRange(List<NsDcChungTu> listChungTu)
        {
            _chungTuRepository.UpdateRange(listChungTu);
        }
    }
}
