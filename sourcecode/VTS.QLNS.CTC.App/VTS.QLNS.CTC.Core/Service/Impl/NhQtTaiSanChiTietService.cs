using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhQtTaiSanChiTietService : INhQtTaiSanChiTietService
    {
        private readonly INhQtTaiSanChiTietRepository _nhQtTaiSanChiTietRepository;

        public NhQtTaiSanChiTietService(INhQtTaiSanChiTietRepository nhQtTaiSanChiTietRepository)
        {
            _nhQtTaiSanChiTietRepository = nhQtTaiSanChiTietRepository;
        }
        public IEnumerable<NhQtTaiSanChiTiet> FindAll()
        {
            return _nhQtTaiSanChiTietRepository.FindAll();
        }
        public NhQtTaiSanChiTiet FindById(Guid? id) => _nhQtTaiSanChiTietRepository.Find(id);
        public void Add(NhQtTaiSanChiTiet nhQtTaiSanChiTiet)
        {
            _nhQtTaiSanChiTietRepository.Add(nhQtTaiSanChiTiet);
        }
        public void Update(NhQtTaiSanChiTiet nhQtTaiSanChiTiet)
        {
            _nhQtTaiSanChiTietRepository.Update(nhQtTaiSanChiTiet);
        }

        public void Delete(Guid id)
        {
            _nhQtTaiSanChiTietRepository.Delete(id);
        }

        public IEnumerable<NhQtTaiSanChiTiet> FindByTaiSanChiTietId(Guid idTaiSan)
        {
            return _nhQtTaiSanChiTietRepository.FindAll(x => x.IIdTaiSanId == idTaiSan);
        }
    }
}
