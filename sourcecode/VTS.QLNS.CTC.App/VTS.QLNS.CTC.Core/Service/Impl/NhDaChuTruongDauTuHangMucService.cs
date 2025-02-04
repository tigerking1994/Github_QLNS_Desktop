using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDaChuTruongDauTuHangMucService : INhDaChuTruongDauTuHangMucService
    {
        private readonly INhDaChuTruongDauTuHangMucRepository _repository;

        public NhDaChuTruongDauTuHangMucService(INhDaChuTruongDauTuHangMucRepository repository)
        {
            _repository = repository;
        }

        public void AddRange(IEnumerable<NhDaChuTruongDauTuHangMuc> entities)
        {
            _repository.AddRange(entities);
        }

        public void UpdateRange(IEnumerable<NhDaChuTruongDauTuHangMuc> entities)
        {
            _repository.UpdateRange(entities);
        }

        public void RemoveRange(IEnumerable<NhDaChuTruongDauTuHangMuc> entities)
        {
            _repository.RemoveRange(entities);
        }

        public IEnumerable<NhDaChuTruongDauTuHangMuc> FindByChuTruongDauTuId(Guid chuTruongDauTuId)
        {
            return _repository.FindAll(x => x.IIdChuTruongDauTuId == chuTruongDauTuId).OrderBy(x => x.SMaOrder);
        }
    }
}
