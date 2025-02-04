using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class SktSoLieuChiTietCanCuService : ISktSoLieuChiTietCanCuService
    {
        private readonly ISktSoLieuChiTietCanCuRepository _iSktSoLieuChiTietCanCuRepository;

        public SktSoLieuChiTietCanCuService(ISktSoLieuChiTietCanCuRepository iSktSoLieuChiTietCanCuRepository)
        {
            _iSktSoLieuChiTietCanCuRepository = iSktSoLieuChiTietCanCuRepository;
        }

        public void AddRange(List<NsDtdauNamChungTuChungTuCanCu> entitys)
        {
            _iSktSoLieuChiTietCanCuRepository.AddRange(entitys);
        }

        public int Delete(Guid id)
        {
            return _iSktSoLieuChiTietCanCuRepository.Delete(id);
        }

        public IEnumerable<NsDtdauNamChungTuChungTuCanCu> FindByCondition(Expression<Func<NsDtdauNamChungTuChungTuCanCu, bool>> predicate)
        {
            return _iSktSoLieuChiTietCanCuRepository.FindAll(predicate);
        }

        public int RemoveRange(IEnumerable<NsDtdauNamChungTuChungTuCanCu> sktChungTuChiTiets)
        {
            return _iSktSoLieuChiTietCanCuRepository.RemoveRange(sktChungTuChiTiets);
        }
    }
}
