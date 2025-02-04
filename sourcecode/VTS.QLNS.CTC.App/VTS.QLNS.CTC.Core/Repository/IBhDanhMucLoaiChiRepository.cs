using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhDanhMucLoaiChiRepository : IRepository<BhDanhMucLoaiChi>
    {
        int AddOrUpdateRange(IEnumerable<BhDanhMucLoaiChi> listEntities, AuthenticationInfo authenticationInfo);
        IEnumerable<BhDanhMucLoaiChi> FindAll(AuthenticationInfo authenticationInfo);
        IEnumerable<BhDanhMucLoaiChi> FindByNamLamViec(int namLamViec);
    }
}
