using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhDcDuToanThuService
    {
        IEnumerable<BhDcDuToanThuQuery> FindIndex(int namLamViec);
        void LockOrUnlock(Guid id, bool status);
        BhDcDuToanThu FindById(Guid id);
        void Delete(Guid id);
        IEnumerable<BhDcDuToanThu> FindByCondition(Expression<Func<BhDcDuToanThu, bool>> predicate);
        IEnumerable<BhDcDuToanThuQuery> FindByYearOfWord(int namLamViec);
        void Update(BhDcDuToanThu entity);
        int GetSoChungTuIndexByCondition();
        void Add(BhDcDuToanThu entity);
        IEnumerable<BhDcDuToanThu> FindByCondition(int namLamViec, string maDonVi, int loaiChungTu);
        IEnumerable<BhDcDuToanThu> FindChungTuDaTongHopBySCT(string sct, int namLamViec);
        int Delete(BhDcDuToanThu item);
        List<string> FindCurrentUnits(int namLamViec);
        IEnumerable<BhDcDuToanThu> FindByAggregateVoucher(List<string> voucherNoes, int yearOfWork);
    }
}
