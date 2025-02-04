using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhQtcNamKinhPhiKhacRepostiory : IRepository<BhQtcNamKinhPhiKhac>
    {
        void CreateQTCNamKPKFor4Quy(Guid idChungTu, string idDonVi, int iNamLamViec, string user, Guid iDLoaiCap);
        List<DonVi> FindByDonViForNamLamViec(int yearOfWork, int chungTu, Guid idLoaiChi);
        List<BhQtcNamKinhPhiKhac> FindByYear(int namLamViec);
        IEnumerable<BhQtcNamKinhPhiKhacQuery> FindIndex(int iNamChungTu);
        int GetSoChungTuIndexByCondition(int yearOfWork);
        bool IsExistChungTuTongHop(int namLamViec);
        int LockOrUnLock(Guid id, bool lockStatus);
    }
}
