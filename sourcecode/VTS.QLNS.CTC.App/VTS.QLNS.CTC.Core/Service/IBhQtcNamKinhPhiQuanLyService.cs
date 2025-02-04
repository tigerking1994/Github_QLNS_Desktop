using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhQtcNamKinhPhiQuanLyService
    {
        IEnumerable<BhQtcNamKinhPhiQuanLy> FindByCondition(Expression<Func<BhQtcNamKinhPhiQuanLy, bool>> predicate);
        IEnumerable<BhQtcNamKinhPhiQuanLyQuery> FindIndex(int iNamChungTu);
        int GetSoChungTuIndexByCondition(int namLamViec);
        int Add(BhQtcNamKinhPhiQuanLy item);
        int Delete(BhQtcNamKinhPhiQuanLy item);
        int Update(BhQtcNamKinhPhiQuanLy item);
        bool IsExistChungTuTongHop(int namLamViec);
        BhQtcNamKinhPhiQuanLy FindById(Guid id);
        List<BhQtcNamKinhPhiQuanLy> FindByYear(int namLamViec);
        int LockOrUnLock(Guid id, bool lockStatus);
        public void CreateQTCNamKPQLFor4Quy(Guid idChungTu, string idDonVi, int iNamLamViec, string user);
        List<DonVi> FindByDonViForNamLamViec(int yearOfWork, int chungTu);
        IEnumerable<BhQtcNamKinhPhiQuanLy> FindByAggregateVoucher(List<string> voucherNos, int yearOfWork);
    }
}
