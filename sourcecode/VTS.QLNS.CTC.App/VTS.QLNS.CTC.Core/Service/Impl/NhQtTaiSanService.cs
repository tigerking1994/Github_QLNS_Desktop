using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhQtTaiSanService : INhQtTaiSanService
    {
        private readonly INhQtTaiSanRepository _nhQtTaiSanRepository;

        public NhQtTaiSanService(INhQtTaiSanRepository nhQtTaiSanRepository)
        {
            _nhQtTaiSanRepository = nhQtTaiSanRepository;
        }
        public IEnumerable<NhQtTaiSan> FindAll()
        {
            return _nhQtTaiSanRepository.FindAll();
        }
        public NhQtTaiSan FindById(Guid? id) => _nhQtTaiSanRepository.Find(id);
        public void Add(NhQtTaiSan nhQtTaiSan)
        {
            _nhQtTaiSanRepository.Add(nhQtTaiSan);
        }
        public void Delete(Guid id)
        {
            _nhQtTaiSanRepository.Delete(id);
        }
        public void Update(NhQtTaiSan nhQtTaiSan)
        {
            _nhQtTaiSanRepository.Update(nhQtTaiSan);
        }
        public IEnumerable<NhQtTaiSanQuery> FindByTaiSanByIdChungTu(Guid idChungTuTaiSan)
        {
            return _nhQtTaiSanRepository.GetTaiSanByIdChungTuTaiSan(idChungTuTaiSan);
        }

        public IEnumerable<NhQtTaiSanQuery> FindAllThongKeTaiSan()
        {
            return _nhQtTaiSanRepository.FindAllThongKeTaiSan();
        }
    }
}
