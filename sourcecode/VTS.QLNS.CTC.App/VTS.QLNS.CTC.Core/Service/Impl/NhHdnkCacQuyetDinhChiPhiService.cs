using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using System.Linq;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhHdnkCacQuyetDinhChiPhiService : INhHdnkCacQuyetDinhChiPhiService
    {
        private INhHdnkCacQuyetDinhChiPhiRepository _repository;

        public NhHdnkCacQuyetDinhChiPhiService(INhHdnkCacQuyetDinhChiPhiRepository nhHdnkCacQuyetDinhChiPhiRepository)
        {
            _repository = nhHdnkCacQuyetDinhChiPhiRepository;
        }

        public void AddRange(List<NhHdnkCacQuyetDinhChiPhi> listQuyetDinhNguonVon)
        {
            _repository.AddRange(listQuyetDinhNguonVon);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public NhHdnkCacQuyetDinhChiPhi FindById(Guid Id)
        {
            return _repository.Find(Id);
        }

        public IEnumerable<NhHdnkCacQuyetDinhChiPhi> FindByIdQuyetDinh(Guid? IdQuyetDinh)
        {
            return _repository.FindByIdQuyetDinh(IdQuyetDinh);
        }

        public IEnumerable<NhHdnkCacQuyetDinhChiPhiQuery> FindByIdKhttNhiemVuChi(Guid IdKhttNhiemVuChi)
        {
            return _repository.FindByIdKhttNhiemVuChi(IdKhttNhiemVuChi);
        }

        public IEnumerable<NhHdnkCacQuyetDinhChiPhi> FindAll()
        {
            return _repository.FindAll();
        }

        public void Update(NhHdnkCacQuyetDinhChiPhi quyetDinhNguonVon)
        {
            _repository.Update(quyetDinhNguonVon);
        }

        public IEnumerable<NhHdnkCacQuyetDinhChiPhiDmChiPhiQuery> FindByIdQuyetDinhGoiThau(Guid? idQuyetDinh)
        {
            return _repository.FindByIdQuyetDinhGoiThau(idQuyetDinh);
        }
    }
}
