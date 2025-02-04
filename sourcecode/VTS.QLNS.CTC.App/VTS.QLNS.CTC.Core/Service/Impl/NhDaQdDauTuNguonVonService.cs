using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDaQdDauTuNguonVonService : INhDaQdDauTuNguonVonService
    {
        private readonly INhDaQdDauTuNguonVonRepository _repository;

        public NhDaQdDauTuNguonVonService(INhDaQdDauTuNguonVonRepository repository)
        {
            _repository = repository;
        }

        public void AddRange(IEnumerable<NhDaQdDauTuNguonVon> entities)
        {
            _repository.AddRange(entities);
        }

        public void UpdateRange(IEnumerable<NhDaQdDauTuNguonVon> entities)
        {
            _repository.UpdateRange(entities);
        }

        public void RemoveRange(IEnumerable<NhDaQdDauTuNguonVon> entities)
        {
            _repository.RemoveRange(entities);
        }

        public NhDaQdDauTuNguonVon FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<NhDaQdDauTuNguonVon> FindByQdDauTuId(Guid qdDauTuId)
        {
            return _repository.FindAll(x => x.IIdQdDauTuId == qdDauTuId);
        }

        public IEnumerable<NhDaDetailNguonVonQuery> GetNguonVonByQdDauTuId(Guid iIdQdDauTuId)
        {
            return _repository.GetNguonVonByQdDauTuId(iIdQdDauTuId);
        }

        public List<NHDAQDDauTuNguonVonQuery> FindByDuAnId(Guid id)
        {
            return _repository.FindByDuAnId(id);
        }
    }
}
