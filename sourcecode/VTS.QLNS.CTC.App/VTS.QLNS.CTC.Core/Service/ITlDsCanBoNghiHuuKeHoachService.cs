using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDsCanBoNghiHuuKeHoachService
    {
        IEnumerable<TlDsCanBoNghiHuuKeHoachQuery> FinAllCanBoNghiHuuKeHoach();
        int Delete(Guid id);
    }
}
