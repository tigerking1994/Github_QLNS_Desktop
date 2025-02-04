using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;
using static VTS.QLNS.CTC.Utility.Enum.BaoHiemDuToanTypeEnum;


namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class CptuBHYTService : ICptuBHYTService
    {
        private readonly ICptuBHYTRepository _iCptuBHYTRepository;
        public CptuBHYTService(ICptuBHYTRepository iCptuBHYTRepository)
        {
            _iCptuBHYTRepository = iCptuBHYTRepository;
        }

        public IEnumerable<BhCptuBHYT> FindByCondition(Expression<Func<BhCptuBHYT, bool>> predicate)
        {
            return _iCptuBHYTRepository.FindByCondition(predicate);
        }

        public int GetSoChungTuIndexByCondition(int iNamLamViec)
        {
            return _iCptuBHYTRepository.GetSoChungTuIndexByCondition(iNamLamViec);
        }

        public int Add(BhCptuBHYT item)
        {
            return _iCptuBHYTRepository.Add(item);
        }

        public int Update(BhCptuBHYT item)
        {
            return _iCptuBHYTRepository.Update(item);
        }

        public int Delete(BhCptuBHYT item)
        {
            return _iCptuBHYTRepository.Delete(item);
        }

        public BhCptuBHYT FindById(Guid id)
        {
            return _iCptuBHYTRepository.Find(id);
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            return _iCptuBHYTRepository.LockOrUnLock(id, lockStatus);
        }    

        public void UpdateTotalCPChungTu(string voucherId, string userModify)
        {
            _iCptuBHYTRepository.UpdateTotalCPChungTu(voucherId, userModify);
        }

        public void UpdateAggregateStatus(string voucherIds)
        {
            _iCptuBHYTRepository.UpdateAggregateStatus(voucherIds);
        }

        public IEnumerable<BhCptuBHYT> FindChungTuDaTongHopBySCT(string sct, int namLamViec)
        {
            return _iCptuBHYTRepository.FindChungTuDaTongHopBySCT(sct, namLamViec);
        }

        public IEnumerable<BhCptuBHYT> FindByYear(int namLamViec)
        {
            return _iCptuBHYTRepository.FindByYear(namLamViec);
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            var chungTuKhtBHXH = _iCptuBHYTRepository.Find(id);
            chungTuKhtBHXH.BIsKhoa = isLock;
            _iCptuBHYTRepository.Update(chungTuKhtBHXH);
        }

        public IEnumerable<BhCptuBHYT> FindAggregateVoucher(int namLamViec)
        {
            return _iCptuBHYTRepository.FindAggregateVoucher(namLamViec);
        }

        public void AddAggregate(BhCpTUChungTuChiTietCriteria creation)
        {
            _iCptuBHYTRepository.AddAggregate(creation);
        }

        public bool IsExistChungTuTongHop(int namLamViec)
        {
            return _iCptuBHYTRepository.IsExistChungTuTongHop(namLamViec);
        }
    }
}
