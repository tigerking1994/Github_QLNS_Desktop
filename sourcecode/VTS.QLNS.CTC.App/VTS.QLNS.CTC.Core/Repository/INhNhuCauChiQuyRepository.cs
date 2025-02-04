using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhNhuCauChiQuyRepository : IRepository<NhNhuCauChiQuy>
    {
        IEnumerable<NhNhuCauChiQuyQuery> GetAll();
        IEnumerable<NhNhuCauChiQuyBaoCaoQuery> ReportNhuCauChiQuy(Guid idChiQuy);
        string GetSTTLAMA(int STT);
        string UpdateChilrenGeneral(Guid? Id);
    }
}
