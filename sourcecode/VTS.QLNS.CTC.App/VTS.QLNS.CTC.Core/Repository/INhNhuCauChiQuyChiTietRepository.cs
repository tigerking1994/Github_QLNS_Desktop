using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhNhuCauChiQuyChiTietRepository : IRepository<NhNhuCauChiQuyChiTiet>
    {
        IEnumerable<NhuCauChiQuyNhiemVuChiQuery> FindNhiemVuChiByIdDonVi(Guid? idDonVi);
        NhNhuCauChiQuyChiTiet FindByIdHopDong(Guid? idHopDong);
        IEnumerable<NhNhuCauChiQuyKinhPhiDaChiQuery> KinhPhiDaChi(Guid idHopDong, int nam);
    }
}
