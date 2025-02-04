using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtKtKhoiTaoService
    {
        IEnumerable<KhoiTaoQuery> FindByCondition(int namLamViec);
        int Delete(Guid Id);
        int Add(VdtKtKhoiTao entity);
        VdtKtKhoiTao Find(params object[] keyValues);
        int Update(VdtKtKhoiTao entity);
    }
}
