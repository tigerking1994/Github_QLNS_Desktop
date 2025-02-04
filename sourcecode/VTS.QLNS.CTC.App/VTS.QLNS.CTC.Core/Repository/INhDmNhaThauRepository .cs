using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDmNhaThauRepository : IRepository<NhDmNhaThau>
    {
            int AddOrUpdateRange(IEnumerable<NhDmNhaThau> entities, int iNamLamViec);
    }
}
