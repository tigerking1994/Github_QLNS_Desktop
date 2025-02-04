using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhKhcKRepostiory : IRepository<BhKhcK>
    {
        List<DonVi> FindByDonViForNamLamViec(int yearOfWork, Guid IdLoaiChi);
        IEnumerable<BhKhcKQuery> FindIndex();
        int GetSoChungTuIndexByCondition(int yearOfWork);
        List<BhKhcK> FindAggregateVoucher(int yearOfWork);
    }
}
