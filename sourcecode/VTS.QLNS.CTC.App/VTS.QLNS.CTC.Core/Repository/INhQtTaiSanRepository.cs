using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using System.Linq.Expressions;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhQtTaiSanRepository : IRepository<NhQtTaiSan>
    {
        IEnumerable<NhQtTaiSanQuery> GetTaiSanByIdChungTuTaiSan(Guid idChungTuTaiSan);
        IEnumerable<NhQtTaiSanQuery> FindAllThongKeTaiSan();
    }
}