using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDaQuyetDinhKhacChiPhiRepository : IRepository<NhDaQuyetDinhKhacChiPhi>
    {
        IEnumerable<NhDaQuyetDinhKhacChiPhi> FindAll(AuthenticationInfo authenticationInfo);
        IEnumerable<NhDaQuyetDinhKhacChiPhi> FindByQuyetDinhKhacId(Guid iIdQuyetDinhKhacId);
    }
}
