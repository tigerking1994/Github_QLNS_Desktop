using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhQtcNamKinhPhiQuanLyService : IBhQtcNamKinhPhiQuanLyService
    {
        private readonly IBhQtcNamKinhPhiQuanLyRepostiory _repostiory;
        public BhQtcNamKinhPhiQuanLyService(IBhQtcNamKinhPhiQuanLyRepostiory repostiory)
        {
            _repostiory = repostiory;
        }

        public int Add(BhQtcNamKinhPhiQuanLy item)
        {
            return _repostiory.Add(item);
        }

        public int Delete(BhQtcNamKinhPhiQuanLy item)
        {
            return _repostiory.Delete(item);
        }

        public IEnumerable<BhQtcNamKinhPhiQuanLy> FindByCondition(Expression<Func<BhQtcNamKinhPhiQuanLy, bool>> predicate)
        {
            return _repostiory.FindAll(predicate);
        }

        public BhQtcNamKinhPhiQuanLy FindById(Guid id)
        {
            return _repostiory.Find(id);
        }

        public List<BhQtcNamKinhPhiQuanLy> FindByYear(int namLamViec)
        {
            return _repostiory.FindByYear(namLamViec);
        }

        public IEnumerable<BhQtcNamKinhPhiQuanLyQuery> FindIndex(int iNamChungTu)
        {
            return _repostiory.FindIndex(iNamChungTu);
        }

        public int GetSoChungTuIndexByCondition(int namLamViec)
        {
            return _repostiory.GetSoChungTuIndexByCondition(namLamViec);
        }

        public bool IsExistChungTuTongHop(int namLamViec)
        {
            return _repostiory.IsExistChungTuTongHop(namLamViec);
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            return _repostiory.LockOrUnLock(id, lockStatus);
        }

        public int Update(BhQtcNamKinhPhiQuanLy item)
        {
            return _repostiory.Update(item);
        }
        public void CreateQTCNamKPQLFor4Quy(Guid idChungTu, string idDonVi, int iNamLamViec, string user)
        {
            _repostiory.CreateQTCNamKPQLFor4Quy(idChungTu, idDonVi, iNamLamViec, user);
        }

        public List<DonVi> FindByDonViForNamLamViec(int yearOfWork, int chungTu)
        {
            return _repostiory.FindByDonViForNamLamViec(yearOfWork, chungTu);
        }
        
        public IEnumerable<BhQtcNamKinhPhiQuanLy> FindByAggregateVoucher(List<string> voucherNos, int yearOfWork)
        {
            return _repostiory.FindByAggregateVoucher(voucherNos, yearOfWork);
        }
    }
}
