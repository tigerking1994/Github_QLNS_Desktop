using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlMapColumnConfigService : ITlMapColumnConfigService
    {
        private readonly ITlMapColumnConfigRepository _tlMapColumnConfigRepository;

        public TlMapColumnConfigService(ITlMapColumnConfigRepository tlMapColumnConfigRepository)
        {
            _tlMapColumnConfigRepository = tlMapColumnConfigRepository;
        }

        public int AddRange(IEnumerable<TlMapColumnConfig> tlMapColumnConfigs)
        {
            return _tlMapColumnConfigRepository.AddRange(tlMapColumnConfigs);
        }

        public int Delete(Guid id)
        {
            return _tlMapColumnConfigRepository.Delete(id);
        }

        public IEnumerable<TlMapColumnConfig> FindAll()
        {
            return _tlMapColumnConfigRepository.FindAll();
        }

        public int Update(TlMapColumnConfig tlMapColumnConfig)
        {
            return _tlMapColumnConfigRepository.Update(tlMapColumnConfig);
        }
    }
}
