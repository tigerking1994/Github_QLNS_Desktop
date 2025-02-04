using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhQtChungTuTaiSanService : INhQtChungTuTaiSanService
    {
        private INhQtChungTuTaiSanRepository _nhQtChungTuTaiSanRepository;
        public NhQtChungTuTaiSanService(INhQtChungTuTaiSanRepository nhQtChungTuTaiSanRepository)
        {
            _nhQtChungTuTaiSanRepository = nhQtChungTuTaiSanRepository;
        }
        public void Add(NhQtChungTuTaiSan nhQtChungTuTaiSan)
        {
            _nhQtChungTuTaiSanRepository.Add(nhQtChungTuTaiSan);
        }

        public void Delete(Guid id)
        {
            _nhQtChungTuTaiSanRepository.Delete(id);
        }

        public IEnumerable<NhQtChungTuTaiSan> FindAll()
        {
            return _nhQtChungTuTaiSanRepository.FindAll();
        }

        public NhQtChungTuTaiSan FindById(Guid? id)
        {
            return _nhQtChungTuTaiSanRepository.Find(id);
        }

        public void Update(NhQtChungTuTaiSan nhQtChungTuTaiSan)
        {
            _nhQtChungTuTaiSanRepository.Update(nhQtChungTuTaiSan);
        }
    }
}
