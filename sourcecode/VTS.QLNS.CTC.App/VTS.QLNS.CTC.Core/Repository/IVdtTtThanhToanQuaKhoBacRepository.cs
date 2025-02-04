using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtTtThanhToanQuaKhoBacRepository : IRepository<VdtTtThanhToanQuaKhoBac>
    {
        IEnumerable<VdtTtThanhToanQuaKhoBacQuery> GetDataIndex();
    }
}
