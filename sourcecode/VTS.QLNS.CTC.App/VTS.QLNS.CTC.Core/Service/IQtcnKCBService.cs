using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;


namespace VTS.QLNS.CTC.Core.Service
{
    public interface IQtcnKCBService
    {
        BhQtcnKCB FindById(Guid id);
        IEnumerable<BhQtcnKCB> FindByYear(int namLamViec);
        int Add(BhQtcnKCB item);
        int Delete(BhQtcnKCB item);
        int Update(BhQtcnKCB item);
        IEnumerable<BhQtcnKCB> FindByCondition(Expression<Func<BhQtcnKCB, bool>> predicate);
        int GetSoChungTuIndexByCondition(int iNamLamViec);
        int LockOrUnLock(Guid id, bool lockStatus);
        IEnumerable<BhQtcnKCBQuery> GetDanhSachQuyetToanNamKCB(int iNamLamViec);
        bool IsExistChungTuTongHop(int namLamViec);
        void UpdateTotalCPChungTu(string voucherId, string userModify);
        List<DonVi> FindByDonViForNamLamViec(int yearOfWork, int chungTu);
    }
}
