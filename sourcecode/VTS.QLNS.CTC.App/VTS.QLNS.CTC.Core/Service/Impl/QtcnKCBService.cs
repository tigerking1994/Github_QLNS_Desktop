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
    public class QtcnKCBService : IQtcnKCBService
    {
        private readonly IQtcnKCBRepository _iQtcnKCBRepository;
        public QtcnKCBService(IQtcnKCBRepository iQtcnKCBRepository)
        {
            _iQtcnKCBRepository = iQtcnKCBRepository;
        }
        public BhQtcnKCB FindById(Guid id)
        {
            return _iQtcnKCBRepository.Find(id);
        }

        public IEnumerable<BhQtcnKCB> FindByYear(int namLamViec)
        {
            return _iQtcnKCBRepository.FindByYear(namLamViec);
        }
        public int Add(BhQtcnKCB item)
        {
            return _iQtcnKCBRepository.Add(item);
        }

        public int Delete(BhQtcnKCB item)
        {
            return _iQtcnKCBRepository.Delete(item);
        }
        public int Update(BhQtcnKCB item)
        {
            return _iQtcnKCBRepository.Update(item);
        }

        public IEnumerable<BhQtcnKCB> FindByCondition(Expression<Func<BhQtcnKCB, bool>> predicate)
        {
            return _iQtcnKCBRepository.FindByCondition(predicate);
        }

        public int GetSoChungTuIndexByCondition(int iNamLamViec)
        {
            return _iQtcnKCBRepository.GetSoChungTuIndexByCondition(iNamLamViec);
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            return _iQtcnKCBRepository.LockOrUnLock(id, lockStatus);
        }
        public IEnumerable<BhQtcnKCBQuery> GetDanhSachQuyetToanNamKCB(int iNamLamViec)
        {
            return _iQtcnKCBRepository.GetDanhSachQuyetToanNamKCB(iNamLamViec);
        }
        public bool IsExistChungTuTongHop(int namLamViec)
        {
            return _iQtcnKCBRepository.IsExistChungTuTongHop(namLamViec);
        }
        public void UpdateTotalCPChungTu(string voucherId, string userModify)
        {
            _iQtcnKCBRepository.UpdateTotalCPChungTu(voucherId, userModify);
        }

        public List<DonVi> FindByDonViForNamLamViec(int yearOfWork, int chungTu)
        {
            return _iQtcnKCBRepository.FindByDonViForNamLamViec(yearOfWork, chungTu);
        }
    }
}
