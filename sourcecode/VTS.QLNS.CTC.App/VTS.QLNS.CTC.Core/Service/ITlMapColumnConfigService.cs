using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlMapColumnConfigService
    {
        int AddRange(IEnumerable<TlMapColumnConfig> tlMapColumnConfigs);
        int Update(TlMapColumnConfig tlMapColumnConfig);
        int Delete(Guid id);
        IEnumerable<TlMapColumnConfig> FindAll();
    }
}
