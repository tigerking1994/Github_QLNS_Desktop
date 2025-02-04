using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhDmKinhPhiService
    {
        IEnumerable<BhDmKinhPhi> FindAll();
    }
}
