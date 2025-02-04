using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class SktChungTuChiTietCanCuChungTuService : ISktChungTuChiTietCanCuChungTuService
    {
        private readonly ISktChungTuChiTietCanCuChungTuRepository _iSktChungTuChiTietCanCuChungTuRepository;

        public SktChungTuChiTietCanCuChungTuService(ISktChungTuChiTietCanCuChungTuRepository iSktChungTuChiTietCanCuChungTuRepository)
        {
            _iSktChungTuChiTietCanCuChungTuRepository = iSktChungTuChiTietCanCuChungTuRepository;
        }

        public IEnumerable<NsSktChungTuChungTuCanCu> FindByCondition(Expression<Func<NsSktChungTuChungTuCanCu, bool>> predicate)
        {
            return _iSktChungTuChiTietCanCuChungTuRepository.FindAll(predicate);
        }

        public void AddRange(List<NsSktChungTuChungTuCanCu> chiTietCanCus)
        {
            _iSktChungTuChiTietCanCuChungTuRepository.AddRange(chiTietCanCus);
        }
        public int RemoveRange(IEnumerable<NsSktChungTuChungTuCanCu> sktChungTuChiTiets)
        {
            return _iSktChungTuChiTietCanCuChungTuRepository.RemoveRange(sktChungTuChiTiets);
        }
    }
}

