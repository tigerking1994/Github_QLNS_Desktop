using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhCpBsChungTuRepository : IRepository<BhCpBsChungTu>
    {
        IEnumerable<BhCpBsChungTu> FindChungTuDaTongHopBySCT(string sct, int namLamViec);
        bool IsExistChungTuTongHop(int namLamViec);
        IEnumerable<BhCpBsChungTu> FindByYear(int namLamViec);
        int GetSoChungTuIndexByCondition(int yearOfWork);
        IEnumerable<BhCpBsChungTu> FindByCondition(int namLamViec, int loaiChungTu);
        void AddAggregate(BhCpBsChungTuChiTietCriteria creation);
        IEnumerable<BhCpBsChungTu> FindByAggregateVoucher(List<string> voucherNoes, int yearOfWork);
    }
}
