using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtKtKhoiTaoService : IVdtKtKhoiTaoService
    {
        private readonly IVdtKtKhoiTaoRepository _vdtKhoiTaoRepository;

        public VdtKtKhoiTaoService(IVdtKtKhoiTaoRepository vdtKhoiTaoRepository)
        {
            _vdtKhoiTaoRepository = vdtKhoiTaoRepository;
        }

        public IEnumerable<KhoiTaoQuery> FindByCondition(int namLamViec)
        {
            return _vdtKhoiTaoRepository.FindByCondition(namLamViec);
        }

        public int Delete(Guid Id)
        {
            VdtKtKhoiTao itemDelete = _vdtKhoiTaoRepository.Find(Id);
            if (itemDelete != null)
            {
                return _vdtKhoiTaoRepository.Delete(itemDelete);
            }
            return 0;
        }

        public int Add(VdtKtKhoiTao entity)
        {
            return _vdtKhoiTaoRepository.Add(entity);
        }

        public VdtKtKhoiTao Find(params object[] keyValues)
        {
            return _vdtKhoiTaoRepository.Find(keyValues);
        }

        public int Update(VdtKtKhoiTao entity)
        {
            return _vdtKhoiTaoRepository.Update(entity);
        }
    }
}
