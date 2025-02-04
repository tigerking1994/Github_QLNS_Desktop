using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhQtcNamKinhPhiKhacService
    {
        IEnumerable<BhQtcNamKinhPhiKhac> FindByCondition(Expression<Func<BhQtcNamKinhPhiKhac, bool>> predicate);
        IEnumerable<BhQtcNamKinhPhiKhacQuery> FindIndex(int iNamChungTu);
        int GetSoChungTuIndexByCondition(int yearOfWork);
        int Add(BhQtcNamKinhPhiKhac item);
        int Delete(BhQtcNamKinhPhiKhac item);
        int Update(BhQtcNamKinhPhiKhac item);
        bool IsExistChungTuTongHop(int namLamViec);
        BhQtcNamKinhPhiKhac FindById(Guid id);
        List<BhQtcNamKinhPhiKhac> FindByYear(int namLamViec);
        int LockOrUnLock(Guid id, bool lockStatus);
        void CreateQTCNamKPKFor4Quy(Guid idChungTu, string idDonVi, int iNamLamViec, string user,Guid iDLoaiCap);
        List<DonVi> FindByDonViForNamLamViec(int yearOfWork, int chungTu, Guid idLoaiChi);
    }
}
