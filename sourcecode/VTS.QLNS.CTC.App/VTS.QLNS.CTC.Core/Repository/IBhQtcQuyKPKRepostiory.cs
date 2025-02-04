using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhQtcQuyKPKRepostiory : IRepository<BhQtcQuyKPK>
    {
        IEnumerable<DonVi> FindByDonViForNamLamViec(int namLamViec, int iQuy, int iLoaiChungTu, Guid loaiChiQT);
        IEnumerable<BhQtcQuyKPKQuery> FindIndex(int yearOfWork);
        int GetSoChungTuIndexByCondition(int yearOfWork);
        IEnumerable<BhQtcQuyKPK> FindByAggregateVoucher(List<string> voucherNos, int yearOfWork);
    }
}
