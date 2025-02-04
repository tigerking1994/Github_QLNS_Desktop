using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;
using System.Linq;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDmTiGiaChiTietService : IService<NhDmTiGiaChiTiet>, INhDmTiGiaChiTietService
    {
        private readonly INhDmTiGiaChiTietRepository _repository;

        public NhDmTiGiaChiTietService(INhDmTiGiaChiTietRepository repository)
        {
            _repository = repository;
        }

        public void Add(NhDmTiGiaChiTiet nhDmTiGiaChiTiet)
        {
            _repository.Add(nhDmTiGiaChiTiet);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public NhDmTiGiaChiTiet FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<NhDmTiGiaChiTiet> FindByTiGiaId(Guid idTiGia)
        {
            return _repository.FindByTiGia(idTiGia);
        }

        public IEnumerable<NhDmTiGiaChiTiet> FindAll()
        {
            return _repository.FindAll();
        }

        public void Update(NhDmTiGiaChiTiet nhDmTiGiaChiTiet)
        {
            _repository.Update(nhDmTiGiaChiTiet);
        }
    }
}
