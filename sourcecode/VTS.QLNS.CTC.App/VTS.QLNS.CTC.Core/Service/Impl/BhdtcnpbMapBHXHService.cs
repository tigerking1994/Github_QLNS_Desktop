using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhdtcnpbMapBHXHService : IBhdtcnpbMapBHXHService
    {
        private readonly IBhdtcnpbMapBHXHRepository _bhdtcnpbMapBHXHRepository;
        public BhdtcnpbMapBHXHService(IBhdtcnpbMapBHXHRepository bhdtcnpbMapBHXHRepository)
        {
            _bhdtcnpbMapBHXHRepository = bhdtcnpbMapBHXHRepository;
        }

        public IEnumerable<BhdtcnpbMapBHXH> FindByIdNhanDuToan(Guid idNhanDuToan)
        {
            return _bhdtcnpbMapBHXHRepository.FindByIdNhanDuToan(idNhanDuToan);
        }
    }
}
