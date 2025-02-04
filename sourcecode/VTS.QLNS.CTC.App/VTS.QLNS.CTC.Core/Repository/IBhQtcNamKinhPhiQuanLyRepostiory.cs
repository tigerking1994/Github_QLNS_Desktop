using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhQtcNamKinhPhiQuanLyRepostiory : IRepository<BhQtcNamKinhPhiQuanLy>
    {
        List<BhQtcNamKinhPhiQuanLy> FindByYear(int namLamViec);
        IEnumerable<BhQtcNamKinhPhiQuanLyQuery> FindIndex(int iNamChungTu);
        int GetSoChungTuIndexByCondition(int namLamViec);
        bool IsExistChungTuTongHop(int namLamViec);
        int LockOrUnLock(Guid id, bool lockStatus);
        void CreateQTCNamKPQLFor4Quy(Guid idChungTu, string idDonVi, int iNamLamViec, string user);
        List<DonVi> FindByDonViForNamLamViec(int yearOfWork, int chungTu);
        IEnumerable<BhQtcNamKinhPhiQuanLy> FindByAggregateVoucher(List<string> voucherNos, int yearOfWork);
    }
}
