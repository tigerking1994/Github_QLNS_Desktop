using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtKtKhoiTaoChiTietService : IVdtKtKhoiTaoChiTietService
    {
        private readonly IVdtKtKhoiTaoChiTietRepository _vdtKhoiTaoChiTietRepository;

        public VdtKtKhoiTaoChiTietService(IVdtKtKhoiTaoChiTietRepository vdtKhoiTaoChiTietRepository)
        {
            _vdtKhoiTaoChiTietRepository = vdtKhoiTaoChiTietRepository;
        }

        public int Add(VdtKtKhoiTaoChiTiet entity)
        {
            return _vdtKhoiTaoChiTietRepository.Add(entity);
        }

        public int AddRange(IEnumerable<VdtKtKhoiTaoChiTiet> entities)
        {
            return _vdtKhoiTaoChiTietRepository.AddRange(entities);
        }

        public int Delete(Guid id)
        {
            return _vdtKhoiTaoChiTietRepository.Delete(id);
        }

        public VdtKtKhoiTaoChiTiet Find(params object[] keyValues)
        {
            return _vdtKhoiTaoChiTietRepository.Find(keyValues);
        }

        public IEnumerable<KhoiTaoChiTietQuery> FindDataKhoiTaoChiTiet(string idKhoiTao, string idDuAn)
        {
            return _vdtKhoiTaoChiTietRepository.FindDataKhoiTaoChiTiet(idKhoiTao, idDuAn).ToList();
        }

        public int Update(VdtKtKhoiTaoChiTiet entity)
        {
            return _vdtKhoiTaoChiTietRepository.Update(entity);
        }
    }
}
