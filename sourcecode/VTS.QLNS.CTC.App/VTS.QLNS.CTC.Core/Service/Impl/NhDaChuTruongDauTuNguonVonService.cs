using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDaChuTruongDauTuNguonVonService : INhDaChuTruongDauTuNguonVonService
    {
        private readonly INhDaChuTruongDauTuNguonVonRepository _repository;

        public NhDaChuTruongDauTuNguonVonService(INhDaChuTruongDauTuNguonVonRepository repository)
        {
            _repository = repository;
        }

        public void AddRange(IEnumerable<NhDaChuTruongDauTuNguonVon> entities)
        {
            _repository.AddRange(entities);
        }

        public void UpdateRange(IEnumerable<NhDaChuTruongDauTuNguonVon> entities)
        {
            _repository.UpdateRange(entities);
        }

        public void RemoveRange(IEnumerable<NhDaChuTruongDauTuNguonVon> entities)
        {
            _repository.RemoveRange(entities);
        }

        public IEnumerable<NhDaChuTruongDauTuNguonVon> FindByChuTruongDauTuId(Guid chuTruongDauTuId)
        {
            return _repository.FindAll(x => x.IIdChuTruongDauTuId == chuTruongDauTuId);
        }

        public NhDaChuTruongDauTuNguonVon FindById(Guid id)
        {
            return _repository.Find(id);
        }
    }
}
