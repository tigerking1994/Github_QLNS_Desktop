using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using System.Linq;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhHdnkCacQuyetDinhChiPhiHangMucService : INhHdnkCacQuyetDinhChiPhiHangMucService
    {
        private INhHdnkCacQuyetDinhChiPhiHangMucRepository _repository;

        public NhHdnkCacQuyetDinhChiPhiHangMucService(INhHdnkCacQuyetDinhChiPhiHangMucRepository repository)
        {
            _repository = repository;
        }

        public void AddRange(List<NhHdnkCacQuyetDinhChiPhiHangMuc> listQuyetDinhChiPhiHangMuc)
        {
            _repository.AddRange(listQuyetDinhChiPhiHangMuc);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public void DeleteByQuyetDinhChiPhi(Guid IdQuyetDinhChiPhi)
        {
            var lstHangMuc = FindByIdQuyetDinhChiPhi(IdQuyetDinhChiPhi);
            _repository.RemoveRange(lstHangMuc);
        }

        public NhHdnkCacQuyetDinhChiPhiHangMuc FindById(Guid Id)
        {
            return _repository.Find(Id);
        }

        public IEnumerable<NhHdnkCacQuyetDinhChiPhiHangMuc> FindByIdQuyetDinhChiPhi(Guid IdQuyetDinhChiPhi)
        {
            return _repository.FindByIdQuyetDinhChiPhi(IdQuyetDinhChiPhi);
        }

        public void Update(NhHdnkCacQuyetDinhChiPhiHangMuc QuyetDinhChiPhiHangMuc)
        {
            _repository.Update(QuyetDinhChiPhiHangMuc);
        }
    }
}
