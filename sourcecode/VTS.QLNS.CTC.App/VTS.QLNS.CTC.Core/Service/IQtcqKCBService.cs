using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;


namespace VTS.QLNS.CTC.Core.Service
{
    public interface IQtcqKCBService
    {
        BhQtcqKCB FindById(Guid id);
        IEnumerable<BhQtcqKCB> FindByYear(int namLamViec);
        int Add(BhQtcqKCB item);
        int Delete(BhQtcqKCB item);
        int Update(BhQtcqKCB item);
        IEnumerable<BhQtcqKCB> FindByCondition(Expression<Func<BhQtcqKCB, bool>> predicate);
        int GetSoChungTuIndexByCondition(int iNamLamViec);
        int LockOrUnLock(Guid id, bool lockStatus);
        IEnumerable<BhQtcqKCBQuery> GetDanhSachQuyetToanKCB(int iNamLamViec);
        void UpdateTotalCPChungTu(string voucherId, string userModify);
        List<DonVi> FindByDonViForNamLamViec(int yearOfWork, int iQuy, int chungTu);
        IEnumerable<BhQtcqKCB> FindByAggregateVoucher(List<string> voucherNos, int yearOfWork);
        List<DonVi> FindByDonViTongChiForNamLamViec(int yearOfWork, int iQuy);
    }
}
