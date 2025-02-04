using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhDcDuToanThuRepository : IRepository<BhDcDuToanThu>
    {
        IEnumerable<BhDcDuToanThuQuery> FindIndex(int namLamViec);
        int GetSoChungTuIndexByCondition();
        IEnumerable<BhDcDuToanThu> FindByCondition(int namLamViec, string maDonVi, int loaiChungTu);
        IEnumerable<BhDcDuToanThu> FindChungTuDaTongHopBySCT(string sct, int namLamViec);
        List<string> FindCurrentUnits(int namLamViec);
        IEnumerable<BhDcDuToanThuQuery> FindByYearOfWord(int namLamViec);
        IEnumerable<BhDcDuToanThu> FindByAggregateVoucher(List<string> voucherNoes, int yearOfWork);
    }
}
