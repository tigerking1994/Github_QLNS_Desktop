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
    public class QtcqBHXHService : IQtcqBHXHService
    {
        private readonly IQtcqBHXHRepository _iQtcqBHXHRepository;
        public QtcqBHXHService(IQtcqBHXHRepository iQtcqBHXHRepository)
        {
            _iQtcqBHXHRepository = iQtcqBHXHRepository;
        }
        public BhQtcqBHXH FindById(Guid id)
        {
            return _iQtcqBHXHRepository.Find(id);
        }

        public IEnumerable<BhQtcqBHXH> FindByYear(int namLamViec)
        {
            return _iQtcqBHXHRepository.FindByYear(namLamViec);
        }
        public int Add(BhQtcqBHXH item)
        {
            return _iQtcqBHXHRepository.Add(item);
        }

        public int Delete(BhQtcqBHXH item)
        {
            return _iQtcqBHXHRepository.Delete(item);
        }
        public int Update(BhQtcqBHXH item)
        {
            return _iQtcqBHXHRepository.Update(item);
        }

        public IEnumerable<BhQtcqBHXH> FindByCondition(Expression<Func<BhQtcqBHXH, bool>> predicate)
        {
            return _iQtcqBHXHRepository.FindByCondition(predicate);
        }

        public int GetSoChungTuIndexByCondition(int iNamLamViec)
        {
            return _iQtcqBHXHRepository.GetSoChungTuIndexByCondition(iNamLamViec);
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            return _iQtcqBHXHRepository.LockOrUnLock(id, lockStatus);
        }
        
        public IEnumerable<BhQtcqBHXHQuery> GetDanhSachQuyetToanQuyBHXH(int iNamLamViec)
        {
            return _iQtcqBHXHRepository.GetDanhSachQuyetToanQuyBHXH(iNamLamViec);
        }
        public void UpdateTotalCPChungTu(string voucherId, string userModify)
        {
            _iQtcqBHXHRepository.UpdateTotalCPChungTu(voucherId, userModify);
        }
        public IEnumerable<DonVi> FindByDonViForNamLamViec(int namLamViec, int iQuy, int iLoaiChungTu)
        {
            return _iQtcqBHXHRepository.FindByDonViForNamLamViec(namLamViec, iQuy, iLoaiChungTu);
        }

        public int DeleteDupItem(Guid voucherID)
        {
            return _iQtcqBHXHRepository.DeleteDupItem(voucherID);
        }
    }
}
