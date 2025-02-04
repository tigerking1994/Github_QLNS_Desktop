using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhQtcNamKinhPhiKhacService : IBhQtcNamKinhPhiKhacService
    {
        private readonly IBhQtcNamKinhPhiKhacRepostiory _repostiory;
        public BhQtcNamKinhPhiKhacService(IBhQtcNamKinhPhiKhacRepostiory repostiory)
        {
            _repostiory = repostiory;
        }

        public int Add(BhQtcNamKinhPhiKhac item)
        {
            return _repostiory.Add(item);
        }

        public void CreateQTCNamKPKFor4Quy(Guid idChungTu, string idDonVi, int iNamLamViec, string user, Guid iDLoaiCap)
        {
            _repostiory.CreateQTCNamKPKFor4Quy(idChungTu, idDonVi, iNamLamViec, user, iDLoaiCap);
        }

        public int Delete(BhQtcNamKinhPhiKhac item)
        {
            return _repostiory.Delete(item);
        }

        public IEnumerable<BhQtcNamKinhPhiKhac> FindByCondition(Expression<Func<BhQtcNamKinhPhiKhac, bool>> predicate)
        {
            return _repostiory.FindAll(predicate);
        }

        public List<DonVi> FindByDonViForNamLamViec(int yearOfWork, int chungTu, Guid idLoaiChi)
        {
            return _repostiory.FindByDonViForNamLamViec(yearOfWork, chungTu, idLoaiChi);
        }

        public BhQtcNamKinhPhiKhac FindById(Guid id)
        {
            return _repostiory.Find(id);
        }

        public List<BhQtcNamKinhPhiKhac> FindByYear(int namLamViec)
        {
            return _repostiory.FindByYear(namLamViec);
        }

        public IEnumerable<BhQtcNamKinhPhiKhacQuery> FindIndex(int iNamChungTu)
        {
            return _repostiory.FindIndex(iNamChungTu);
        }

        public int GetSoChungTuIndexByCondition(int yearOfWork)
        {
            return _repostiory.GetSoChungTuIndexByCondition(yearOfWork);
        }

        public bool IsExistChungTuTongHop(int namLamViec)
        {
            return _repostiory.IsExistChungTuTongHop(namLamViec);
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            return _repostiory.LockOrUnLock(id, lockStatus);
        }

        public int Update(BhQtcNamKinhPhiKhac item)
        {
            return _repostiory.Update(item);
        }
    }
}
