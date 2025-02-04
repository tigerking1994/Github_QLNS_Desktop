using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NsBaoCaoGhiChuService : IService<NsCauHinhBaoCao>, INsBaoCaoGhiChuService
    {
        private readonly INsBaoCaoGhiChuRepository _repository;

        public NsBaoCaoGhiChuService(INsBaoCaoGhiChuRepository repository)
        {
            _repository = repository;
        }
        public void Add(NsCauHinhBaoCao dmGhiChu)
        {
            _repository.Add(dmGhiChu);
        }

        public IEnumerable<NsCauHinhBaoCao> FindByCondition(Expression<Func<NsCauHinhBaoCao, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public NsCauHinhBaoCao FindById(Guid id)
        {
            return _repository.SingleOrDefault(t => t.Id.Equals(id));
        }

        public void Save(NsCauHinhBaoCao dmGhiChu)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(IEnumerable<NsCauHinhBaoCao> dmGhiChus)
        {
            _repository.UpdateRange(dmGhiChus);
        }

        public void AddOrUpdateRange(IEnumerable<NsCauHinhBaoCao> listEntities)
        {
            _repository.AddOrUpdateRange(listEntities);
        }

        public void RemoveRange(IEnumerable<NsCauHinhBaoCao> listEntities)
        {
            _repository.RemoveRange(listEntities);
        }

        public override IEnumerable<NsCauHinhBaoCao> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _repository.FindAll();
        }

    }
}
