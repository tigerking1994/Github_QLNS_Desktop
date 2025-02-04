using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDaQdDauTuHangMucService : INhDaQdDauTuHangMucService
    {
        private readonly INhDaQdDauTuHangMucRepository _repository;

        public NhDaQdDauTuHangMucService(INhDaQdDauTuHangMucRepository repository)
        {
            _repository = repository;
        }

        public void AddRange(IEnumerable<NhDaQdDauTuHangMuc> entities)
        {
            _repository.AddRange(entities);
        }

        public void UpdateRange(IEnumerable<NhDaQdDauTuHangMuc> entities)
        {
            _repository.UpdateRange(entities);
        }

        public void RemoveRange(IEnumerable<NhDaQdDauTuHangMuc> entities)
        {
            _repository.RemoveRange(entities);
        }

        public IEnumerable<NhDaQdDauTuHangMuc> FindByQdDauTuChiPhiId(Guid qdDauTuChiPhiId)
        {
            return _repository.FindAll(x => x.IIdQdDauTuChiPhiId == qdDauTuChiPhiId).OrderBy(x => x.SMaOrder);
        }

        public IEnumerable<NhDaQdDauTuHangMuc> FindByQdDauTuChiPhiIds(IEnumerable<Guid> qdDauTuChiPhiIds)
        {
            return _repository.FindAll(x => qdDauTuChiPhiIds.Contains(x.Id)).OrderBy(x => x.SMaOrder);
        }

        public IEnumerable<NhDaDetailHangMucQuery> GetHangMucByQdDauTuId(Guid iIdQdDauTuId)
        {
            return _repository.GetHangMucByQdDauTuId(iIdQdDauTuId);
        }
        public NhDaQdDauTuHangMuc FindById(Guid id)
        {
            return _repository.Find(id);
        }
    }
}
