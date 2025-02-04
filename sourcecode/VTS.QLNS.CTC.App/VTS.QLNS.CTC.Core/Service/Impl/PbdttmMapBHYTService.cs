using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;


namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class PbdttmMapBHYTService : IPbdttmMapBHYTService
    {
        private readonly IPbdttmMapBHYTRepository _iPbdttmMapBHYTRepository;
        public PbdttmMapBHYTService(IPbdttmMapBHYTRepository iPbdttmMapBHYTRepository)
        {
            _iPbdttmMapBHYTRepository = iPbdttmMapBHYTRepository;
        }

        public IEnumerable<BhPbdttmMapBHYT> FindByCondition(Expression<Func<BhPbdttmMapBHYT, bool>> predicate)
        {
            return _iPbdttmMapBHYTRepository.FindByCondition(predicate);
        }

        public bool IsExistEstimate(Guid idNhanPhanBo, Guid idPhanBo)
        {
            return _iPbdttmMapBHYTRepository.IsExistEstimate(idNhanPhanBo, idPhanBo);
        }
        public int Add(BhPbdttmMapBHYT item)
        {
            return _iPbdttmMapBHYTRepository.Add(item);
        }

        public int AddRange(List<BhPbdttmMapBHYT> items)
        {
            return _iPbdttmMapBHYTRepository.AddRange(items);
        }

        public int Update(BhPbdttmMapBHYT item)
        {
            return _iPbdttmMapBHYTRepository.Update(item);
        }

        public int Delete(BhPbdttmMapBHYT item)
        {
            return _iPbdttmMapBHYTRepository.Delete(item);
        }
        public int RemoveRange(List<BhPbdttmMapBHYT> items)
        {
            return _iPbdttmMapBHYTRepository.RemoveRange(items);
        }

        public IEnumerable<BhPbdttmMapBHYT> FindByListIdNhanDuToan(IEnumerable<string> listIdNhanDuToan)
        {
            return _iPbdttmMapBHYTRepository.FindByListIdNhanDuToan(listIdNhanDuToan);
        }
    }
}
