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
    public interface INhTtThongTriCapPhatRepository : IRepository<NhTtThongTriCapPhat>
    {
        IEnumerable<NhTtThongTriCapPhatQuery> FindAllThongTri();
        DataTable ReportThongTriCapPhat(Guid idThongTri);
    }
}
