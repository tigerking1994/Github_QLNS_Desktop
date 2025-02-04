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


namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class QtcnBHXHService : IQtcnBHXHService
    {
        private readonly IQtcnBHXHRepository _iQtcnBHXHRepository;
        public QtcnBHXHService(IQtcnBHXHRepository iQtcnBHXHRepository)
        {
            _iQtcnBHXHRepository = iQtcnBHXHRepository;
        }
        public BhQtcnBHXH FindById(Guid id)
        {
            return _iQtcnBHXHRepository.Find(id);
        }

        public IEnumerable<BhQtcnBHXH> FindByYear(int namLamViec)
        {
            return _iQtcnBHXHRepository.FindByYear(namLamViec);
        }
        public int Add(BhQtcnBHXH item)
        {
            return _iQtcnBHXHRepository.Add(item);
        }

        public int Delete(BhQtcnBHXH item)
        {
            return _iQtcnBHXHRepository.Delete(item);
        }
        public int Update(BhQtcnBHXH item)
        {
            return _iQtcnBHXHRepository.Update(item);
        }

        public IEnumerable<BhQtcnBHXH> FindByCondition(Expression<Func<BhQtcnBHXH, bool>> predicate)
        {
            return _iQtcnBHXHRepository.FindByCondition(predicate);
        }

        public int GetSoChungTuIndexByCondition(int iNamLamViec)
        {
            return _iQtcnBHXHRepository.GetSoChungTuIndexByCondition(iNamLamViec);
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            return _iQtcnBHXHRepository.LockOrUnLock(id, lockStatus);
        }
        public IEnumerable<BhQtcnBHXHQuery> GetDanhSachQuyetToanNamBHXH(int iNamLamViec)
        {
            return _iQtcnBHXHRepository.GetDanhSachQuyetToanNamBHXH(iNamLamViec);
        }
        public bool IsExistChungTuTongHop(int namLamViec)
        {
            return _iQtcnBHXHRepository.IsExistChungTuTongHop(namLamViec);
        }
        public void UpdateTotalCPChungTu(string voucherId, string userModify)
        {
            _iQtcnBHXHRepository.UpdateTotalCPChungTu(voucherId, userModify);
        }

        public List<DonVi> FindByDonViForNamLamViec(int yearOfWork, int chungTu)
        {
            return _iQtcnBHXHRepository.FindByDonViForNamLamViec(yearOfWork, chungTu);
        }
        
        public IEnumerable<BhQtcnBHXH> FindByAggregateVoucher(List<string> voucherNos, int yearOfWork)
        {
            return _iQtcnBHXHRepository.FindByAggregateVoucher(voucherNos, yearOfWork);
        }
    }
}
