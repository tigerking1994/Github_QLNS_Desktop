using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtKtKhoiTaoDuLieuService : IVdtKtKhoiTaoDuLieuService
    {
        private readonly IVdtKtKhoiTaoDuLieuRepository _cpChungTuChiTietRepository;

        public VdtKtKhoiTaoDuLieuService(IVdtKtKhoiTaoDuLieuRepository chungTuChiTietRepository)
        {
            _cpChungTuChiTietRepository = chungTuChiTietRepository;
        }

        public VdtKtKhoiTaoDuLieu Add(VdtKtKhoiTaoDuLieu entity)
        {
            _cpChungTuChiTietRepository.Add(entity);
            return entity;
        }

        public int Delete(Guid id)
        {
            VdtKtKhoiTaoDuLieu entity = _cpChungTuChiTietRepository.Find(id);
            if (entity != null)
            {
                return _cpChungTuChiTietRepository.Delete(entity);
            }
            return 0;
        }

        public VdtKtKhoiTaoDuLieu Find(params object[] keyValues)
        {
            return _cpChungTuChiTietRepository.Find(keyValues);
        }

        public int Update(VdtKtKhoiTaoDuLieu entity)
        {
            return _cpChungTuChiTietRepository.Update(entity);
        }

        public IEnumerable<VdtKtKhoiTaoDuLieu> FindByCondition(Expression<Func<VdtKtKhoiTaoDuLieu, bool>> predicate)
        {
            return _cpChungTuChiTietRepository.FindAll(predicate);
        }

        public IEnumerable<KhoiTaoDuLieuQuery> FindByCondition(int namLamViec)
        {
            return _cpChungTuChiTietRepository.FindByCondition(namLamViec);
        }
    }
}
