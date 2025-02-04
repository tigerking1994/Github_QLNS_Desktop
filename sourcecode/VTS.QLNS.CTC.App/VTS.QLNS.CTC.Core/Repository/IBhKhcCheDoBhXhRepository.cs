using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhKhcCheDoBhXhRepository : IRepository<BhKhcCheDoBhXh>
    {
        List<DonVi> FindByDonViForNamLamViec(int namLamViec);
        IEnumerable<BhKhcCheDoBhXhQuery> FindIndex();
        int GetSoChungTuIndexByCondition(int iNamLamViec);
        bool IsExistChungTuTongHop(int loai, int namLamViec);
        BhKhcCheDoBhXh FindAggregateVoucher(int yearOfWork);
    }
}