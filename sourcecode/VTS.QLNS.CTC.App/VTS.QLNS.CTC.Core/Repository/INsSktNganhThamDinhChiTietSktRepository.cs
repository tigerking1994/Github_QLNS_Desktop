using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INsSktNganhThamDinhChiTietSktRepository : IRepository<NsSktNganhThamDinhChiTietSkt>
    {
        void DeleteByIdChungTuSkt(Guid voucherId);
        void DeleteByYearOfWork(int namLamViec);
        List<JsonNsSktNganhThamDinhChiTietSktQuery> GetNsSktNganhThamDinhChiTietByChungTuId(List<Guid> iIds);
    }
}
