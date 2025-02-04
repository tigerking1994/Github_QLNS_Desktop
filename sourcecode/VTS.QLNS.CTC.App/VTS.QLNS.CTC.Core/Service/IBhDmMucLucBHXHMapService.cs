using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhDmMucLucBHXHMapService
    {
        void Update(BhDmMucLucNganSach entity);
        void UpdateRange(IEnumerable<BhDmMucLucNganSach> entities);
        IEnumerable<BhDmMucLucNganSach> FindAll();
        BhDmMucLucNganSach FindById(Guid id);
        string GetTenPhuCap(string maPCs);
        string GetMoTaMLNS(string xauMLNSs, int YearOfWork);
    }
}
