using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhDtcDcdToanChiService
    {
        void Add(BhDtcDcdToanChi entity);
        void Delete(Guid id);
        void Update(BhDtcDcdToanChi entity);
        IEnumerable<BhDtcDcdToanChiQuery> FindIndex( int yearOfWork);
        BhDtcDcdToanChi FindById(Guid id);
        IEnumerable<BhDtcDcdToanChi> FindByCondition(Expression<Func<BhDtcDcdToanChi, bool>> predicate);
        void LockOrUnlock(Guid id, bool status);
        int GetSoChungTuIndexByCondition(int namLamViec);
        List<DonVi> FindByDonViForNamLamViec(int yearOfWork, Guid iDLoaiChi);
        IEnumerable<BhDtcDcdToanChi> FindByAggregateVoucher(List<string> voucherNos, int yearOfWork);
        List<BhDtcDcdToanChi> FindAggregateAdjustVoucher(int yearOfWork, Guid? iIDLoaiDanhMucChi, string sMaLoaiChi);
        //object FindAggregateVoucher(int yearOfWork, Guid? iIDLoaiDanhMucChi);
    }
}
