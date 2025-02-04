using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using System.Linq;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhHdnkCacQuyetDinhNguonVonService : INhHdnkCacQuyetDinhNguonVonService
    {
        private INhHdnkCacQuyetDinhNguonVonRepository _repository;

        public NhHdnkCacQuyetDinhNguonVonService(INhHdnkCacQuyetDinhNguonVonRepository nhHdnkCacQuyetDinhNguonVonRepository)
        {
            _repository = nhHdnkCacQuyetDinhNguonVonRepository;
        }

        public void AddRange(List<NhHdnkCacQuyetDinhNguonVon> listQuyetDinhNguonVon)
        {
            _repository.AddRange(listQuyetDinhNguonVon);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public NhHdnkCacQuyetDinhNguonVon FindById(Guid Id)
        {
            return _repository.Find(Id); 
        }

        public IEnumerable<NhHdnkCacQuyetDinhNguonVon> FindByIdQuyetDinh(Guid? IdQuyetDinh)
        {
            return _repository.FindByIdQuyetDinh(IdQuyetDinh);
        }

        public IEnumerable<NhThongTinNGuonVonQuery> FindByIdKhttNhiemVuChi(Guid idKhttNhiemVuChi)
        {
            return _repository.FindByIdKhttNhiemVuChi(idKhttNhiemVuChi);
        }

        public IEnumerable<NhThongTinNGuonVonQuery> FindByThongTinNguonVon(Guid idQuyetDinh)
        {
            return _repository.FindByThongTinNguonVon(idQuyetDinh);
        }

        public void Update(NhHdnkCacQuyetDinhNguonVon quyetDinhNguonVon)
        {
            _repository.Update(quyetDinhNguonVon);
        }
        public IEnumerable<NhHdnkCacQuyetDinhNguonVon> FindAll()
        {
            return _repository.FindAll();
        }
    }
}
