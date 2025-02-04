using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhKhcKcbRepostiory : IRepository<BhKhcKcb>
    {
        List<DonVi> FindByDonViForNamLamViec(int yearOfWork);
        IEnumerable<BhKhcKcbQuery> FindIndex();
        int GetSoChungTuIndexByCondition(int yearOfWork);
        BhKhcKcb FindAggregateVoucher(int yearOfWork);
    }
}
