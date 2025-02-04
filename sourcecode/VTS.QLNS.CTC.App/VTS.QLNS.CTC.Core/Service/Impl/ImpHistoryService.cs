using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class ImpHistoryService : IImpHistoryService
    {
        private readonly IImpHistoryRepository _impHistoryRepository;

        public ImpHistoryService(IImpHistoryRepository impHistoryRepository)
        {
            _impHistoryRepository = impHistoryRepository;
        }

        public void Add(ImpHistory history)
        {
            _impHistoryRepository.Add(history);
        }
    }
}
