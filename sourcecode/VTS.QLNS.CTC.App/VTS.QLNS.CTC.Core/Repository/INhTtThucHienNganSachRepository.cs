using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhTtThucHienNganSachRepository : IRepository<NhTtThucHienNganSach>
    {
        IEnumerable<NhTtThucHienNganSachQuery> FindAllData(int? tabTable, int? iQuyList, int? iNam, int? iTuNam, int? iDenNam, Guid? iDonVi);
        IEnumerable<NhTtThucHienNganSachGiaiDoanQuery> GetGiaiDoan();
        string GetSTTLAMA(int STT);
        IEnumerable<NhTtThucHienNganSachQuery> ReportThucHienNganSach(int? tabindex, int? iQuyPrint, int? iNamPrint, int? iTuNamPrint, int? iDenNamPrint, Guid? iDonVi);
    }
}
