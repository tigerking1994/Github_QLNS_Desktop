using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhTtThongTriCapPhatService
    {
        IEnumerable<NhTtThongTriCapPhatQuery> FindAllThongTri();
        public void Add(NhTtThongTriCapPhat entity);
        int Delete(NhTtThongTriCapPhat entity);
        DataTable ReportThongTriCapPhat(Guid idThongTri);
        public void Update(NhTtThongTriCapPhat entity);
        NhTtThongTriCapPhat FindById(Guid id);
    }
}
