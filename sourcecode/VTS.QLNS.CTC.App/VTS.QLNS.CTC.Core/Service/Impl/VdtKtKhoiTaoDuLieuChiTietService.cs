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
    public class VdtKtKhoiTaoDuLieuChiTietService : IVdtKtKhoiTaoDuLieuChiTietService
    {
        private readonly IVdtKtKhoiTaoDuLieuChiTietRepository _cpChungTuChiTietRepository;

        public VdtKtKhoiTaoDuLieuChiTietService(IVdtKtKhoiTaoDuLieuChiTietRepository chungTuChiTietRepository)
        {
            _cpChungTuChiTietRepository = chungTuChiTietRepository;
        }

        public VdtKtKhoiTaoDuLieuChiTiet Add(VdtKtKhoiTaoDuLieuChiTiet entity)
        {
            _cpChungTuChiTietRepository.Add(entity);
            return entity;
        }

        public int Delete(Guid id)
        {
            VdtKtKhoiTaoDuLieuChiTiet entity = _cpChungTuChiTietRepository.Find(id);
            if (entity != null)
            {
                return _cpChungTuChiTietRepository.Delete(entity);
            }
            return 0;
        }

        public VdtKtKhoiTaoDuLieuChiTiet Find(params object[] keyValues)
        {
            return _cpChungTuChiTietRepository.Find(keyValues);
        }

        public int Update(VdtKtKhoiTaoDuLieuChiTiet entity)
        {
            return _cpChungTuChiTietRepository.Update(entity);
        }

        public IEnumerable<VdtKtKhoiTaoDuLieuChiTiet> FindByCondition(Expression<Func<VdtKtKhoiTaoDuLieuChiTiet, bool>> predicate)
        {
            return _cpChungTuChiTietRepository.FindAll(predicate);
        }

        public int AddRange(IEnumerable<VdtKtKhoiTaoDuLieuChiTiet> entities)
        {
            return _cpChungTuChiTietRepository.AddRange(entities);
        }

        public IEnumerable<KhoiTaoDuLieuChiTietQuery> FindDataKhoiTaoChiTiet(string idKhoiTao)
        {
            return _cpChungTuChiTietRepository.FindDataKhoiTaoChiTiet(idKhoiTao);
        }
    }
}
