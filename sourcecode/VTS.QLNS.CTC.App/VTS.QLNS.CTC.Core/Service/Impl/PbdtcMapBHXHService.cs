using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;


namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class PbdtcMapBHXHService : IPbdtcMapBHXHService
    {
        private readonly IPbdtcMapBHXHRepository _ipbdtcMapBHXHRepository;
        public PbdtcMapBHXHService(IPbdtcMapBHXHRepository ipbdtcMapBHXHRepository)
        {
            _ipbdtcMapBHXHRepository = ipbdtcMapBHXHRepository;
        }
        public IEnumerable<BhdtcnpbMapBHXH> FindByCondition(Expression<Func<BhdtcnpbMapBHXH, bool>> predicate)
        {
            return _ipbdtcMapBHXHRepository.FindByCondition(predicate);
        }

        public int Add(BhdtcnpbMapBHXH item)
        {
            return _ipbdtcMapBHXHRepository.Add(item);
        }

        public int Update(BhdtcnpbMapBHXH item)
        {
            return _ipbdtcMapBHXHRepository.Update(item);
        }

        public IEnumerable<BhdtcnpbMapBHXH> Save(IEnumerable<BhdtcnpbMapBHXH> dtChungTuMaps)
        {
            if (dtChungTuMaps == null || !dtChungTuMaps.Any())
            {
                return Enumerable.Empty<BhdtcnpbMapBHXH>();
            }

            _ipbdtcMapBHXHRepository.AddRange(dtChungTuMaps);
            return dtChungTuMaps;
        }

        public int RemoveRange(IEnumerable<BhdtcnpbMapBHXH> dtChungTuMaps)
        {
            return _ipbdtcMapBHXHRepository.RemoveRange(dtChungTuMaps);
        }

        public bool IsExistEstimate(Guid idNhanPhanBo, Guid idPhanBo)
        {
            return _ipbdtcMapBHXHRepository.IsExistEstimate(idNhanPhanBo, idPhanBo);
        }

        public IEnumerable<BhdtcnpbMapBHXH> FindByListIdNhanDuToan(IEnumerable<string> listIdNhanDuToan)
        {
            return _ipbdtcMapBHXHRepository.FindByListIdNhanDuToan(listIdNhanDuToan);
        }
    }
}
