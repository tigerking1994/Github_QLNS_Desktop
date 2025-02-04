using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDaQdDauTuChiPhiService : INhDaQdDauTuChiPhiService
    {
        private readonly INhDaQdDauTuChiPhiRepository _repository;

        public NhDaQdDauTuChiPhiService(INhDaQdDauTuChiPhiRepository repository)
        {
            _repository = repository;
        }

        public void Add(NhDaQdDauTuChiPhi entity)
        {
            _repository.Add(entity);
        }

        public void Update(NhDaQdDauTuChiPhi entity)
        {
            _repository.Update(entity);
        }

        public void Remove(NhDaQdDauTuChiPhi entity)
        {
            _repository.Delete(entity);
        }

        public void AddRange(IEnumerable<NhDaQdDauTuChiPhi> entities)
        {
            _repository.AddRange(entities);
        }

        public void UpdateRange(IEnumerable<NhDaQdDauTuChiPhi> entities)
        {
            _repository.UpdateRange(entities);
        }

        public void RemoveRange(IEnumerable<NhDaQdDauTuChiPhi> entities)
        {
            _repository.RemoveRange(entities);
        }

        public IEnumerable<NhDaQdDauTuChiPhi> FindByQdDauTuId(Guid qdDauTuId)
        {
            return _repository.FindAll(x => x.IIdQdDauTuId == qdDauTuId).OrderBy(x => x.SMaOrder);
        }

        public NhDaQdDauTuChiPhi FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<NhDaDetailChiPhiQuery> GetChiPhiByQdDauTuId(Guid iIdQdDauTuId)
        {
            return _repository.GetChiPhiByQdDauTuId(iIdQdDauTuId);
        }

        public IEnumerable<NhDaQdDauTuChiPhi> FindByNguonVonId(Guid idNguonVon)
        {
            return _repository.FindAll(x => x.IIdQDDauTuNguonVonId == idNguonVon).OrderBy(x => x.SMaOrder);
        }
        public IEnumerable<NhDaQdDauTuChiPhi> FindByQdDauTuByDuAnId(Guid idDuAn)
        {
            return _repository.FindByQdDauTuByDuAnId(idDuAn);
        }

    }
}
