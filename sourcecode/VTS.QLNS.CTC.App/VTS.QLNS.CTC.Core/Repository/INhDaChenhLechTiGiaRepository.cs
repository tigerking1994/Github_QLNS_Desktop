using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDaChenhLechTiGiaRepository
    {
        IEnumerable<NhDaChenhLechTiGiaQuery> FindAllExchangeRateDifference(ExchangeRateDifferenceCriteria condition);
    }
}
