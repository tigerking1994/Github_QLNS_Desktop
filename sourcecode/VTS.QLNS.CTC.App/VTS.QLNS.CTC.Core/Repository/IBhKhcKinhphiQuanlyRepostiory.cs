using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhKhcKinhphiQuanlyRepostiory : IRepository<BhKhcKinhphiQuanly>
    {
        List<DonVi> FindByDonViForNamLamViec(int yearOfWork);
        IEnumerable<BhKhcKinhphiQuanlyQuery> FindIndex();
        int GetSoChungTuIndexByCondition(int NamLamViec);
        BhKhcKinhphiQuanly FindAggregateVoucher(int yearOfWork);
    }
}
