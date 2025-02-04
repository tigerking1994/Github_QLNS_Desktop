using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class SktChungTuChiTietCanCuService : ISktChungTuChiTietCanCuService
    {
        private readonly ISktChungTuChiTietCanCuRepository _iSktChungTuChiTietCanCuRepository;

        public SktChungTuChiTietCanCuService(ISktChungTuChiTietCanCuRepository iSktChungTuChiTietCanCuRepository)
        {
            _iSktChungTuChiTietCanCuRepository = iSktChungTuChiTietCanCuRepository;
        }

        public IEnumerable<NsSktChungTuChiTietCanCu> FindByCondition(Expression<Func<NsSktChungTuChiTietCanCu, bool>> predicate)
        {
            return _iSktChungTuChiTietCanCuRepository.FindAll(predicate);
        }

        public void AddRange(List<NsSktChungTuChiTietCanCu> chiTietCanCus)
        {
            _iSktChungTuChiTietCanCuRepository.AddRange(chiTietCanCus);
        }

        public int RemoveRange(IEnumerable<NsSktChungTuChiTietCanCu> sktChungTuChiTiets)
        {
            return _iSktChungTuChiTietCanCuRepository.RemoveRange(sktChungTuChiTiets);
        }
    }
}
