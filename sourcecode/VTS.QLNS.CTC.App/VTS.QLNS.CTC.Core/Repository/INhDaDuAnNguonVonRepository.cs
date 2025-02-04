using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDaDuAnNguonVonRepository : IRepository<NhDaDuAnNguonVon>
    {
        void DeleteByDuAnId(Guid duAnId);
        void AddOrUpdate(Guid duAnId, IEnumerable<NhDaDuAnNguonVon> entities);
    }
}
