using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INsQsMucLucRepository : IRepository<NsQsMucLuc>
    {
        IEnumerable<NsQsMucLuc> FindByCondition(int yearOfWork);
        int countMLQSByNamLamViec(int yearOfWork);
        NsQsMucLuc FindMaMLNS(Guid MLNS);
        NsQsMucLuc FindByMaTangGiam(string maTangGiam);
    }

}
