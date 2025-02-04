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
    public class QtcqKCBService : IQtcqKCBService
    {
        private readonly IQtcqKCBRepository _iIQtcqKCBRepository;
        public QtcqKCBService(IQtcqKCBRepository iIQtcqKCBRepository)
        {
            _iIQtcqKCBRepository = iIQtcqKCBRepository;
        }
        public BhQtcqKCB FindById(Guid id)
        {
            return _iIQtcqKCBRepository.Find(id);
        }

        public IEnumerable<BhQtcqKCB> FindByYear(int namLamViec)
        {
            return _iIQtcqKCBRepository.FindByYear(namLamViec);
        }
        public int Add(BhQtcqKCB item)
        {
            return _iIQtcqKCBRepository.Add(item);
        }

        public int Delete(BhQtcqKCB item)
        {
            return _iIQtcqKCBRepository.Delete(item);
        }
        public int Update(BhQtcqKCB item)
        {
            return _iIQtcqKCBRepository.Update(item);
        }

        public IEnumerable<BhQtcqKCB> FindByCondition(Expression<Func<BhQtcqKCB, bool>> predicate)
        {
            return _iIQtcqKCBRepository.FindByCondition(predicate);
        }

        public int GetSoChungTuIndexByCondition(int iNamLamViec)
        {
            return _iIQtcqKCBRepository.GetSoChungTuIndexByCondition(iNamLamViec);
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            return _iIQtcqKCBRepository.LockOrUnLock(id, lockStatus);
        }
        public IEnumerable<BhQtcqKCBQuery> GetDanhSachQuyetToanKCB(int iNamLamViec)
        {
            return _iIQtcqKCBRepository.GetDanhSachQuyetToanKCB(iNamLamViec);
        }
        public void UpdateTotalCPChungTu(string voucherId, string userModify)
        {
            _iIQtcqKCBRepository.UpdateTotalCPChungTu(voucherId, userModify);
        }

        public List<DonVi> FindByDonViForNamLamViec(int yearOfWork, int iQuy, int chungTu)
        {
            return _iIQtcqKCBRepository.FindByDonViForNamLamViec(yearOfWork, iQuy, chungTu);
        }
        
        public IEnumerable<BhQtcqKCB> FindByAggregateVoucher(List<string> voucherNos, int yearOfWork)
        {
            return _iIQtcqKCBRepository.FindByAggregateVoucher(voucherNos, yearOfWork);
        }

        public List<DonVi> FindByDonViTongChiForNamLamViec(int yearOfWork, int iQuy)
        {
            return _iIQtcqKCBRepository.FindByDonViTongChiForNamLamViec(yearOfWork, iQuy);
        }
    }
}
