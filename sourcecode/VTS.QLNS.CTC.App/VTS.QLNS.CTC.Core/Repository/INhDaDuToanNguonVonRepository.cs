using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDaDuToanNguonVonRepository : IRepository<NhDaDuToanNguonVon>
    {
        void AddOrUpdate(Guid duToanId, IEnumerable<NhDaDuToanNguonVon> entities);
        void DeleteByDuAnId(Guid duToanId);
        IEnumerable<NhDaDetailNguonVonQuery> FindByDuToanId(Guid duToanId);
    }
}
